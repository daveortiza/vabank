﻿using System;
using VaBank.Common.Data.Repositories;
using VaBank.Common.IoC;
using VaBank.Common.Util;
using VaBank.Common.Validation;
using VaBank.Core.Accounting.Entities;
using VaBank.Core.Processing.Entities;

namespace VaBank.Core.Processing
{    
    [Injectable]
    public class ExchangeRateCalculator
    {
        private readonly IRepository<Currency> _currencyRepository;

        private readonly ExchangeRateSettings _settings;

        public ExchangeRateCalculator(IRepository<Currency> currencyRepository)
        {
            Argument.NotNull(currencyRepository, "currencyRepository");

            _currencyRepository = currencyRepository;
            _settings = new ExchangeRateSettings();
        }

        public ExchangeRate CalculateFromNationalBankRate(string foreignCurrencyISOName, decimal rate, DateTime timestampUtc)
        {
            Argument.NotEmpty(foreignCurrencyISOName, "foreignCurrencyISOName");
            Argument.Satisfies(foreignCurrencyISOName, x => x != _settings.NationalCurrency);
            Argument.Satisfies(rate, x => x > 0, "rate");

            var conversionRate = new ConversionRate(new CurrencyConversion(_settings.NationalCurrency, foreignCurrencyISOName), rate);
            return CalculateFromConversionRate(conversionRate, timestampUtc, _settings.NationalExchangeRateRounding);
        }

        public ExchangeRate CalculateFromConversionRate(ConversionRate conversionRate, DateTime timstampUtc, MoneyRounding rounding)
        {
            Argument.NotNull(conversionRate, "conversionRate");

            var baseCurrency = _currencyRepository.Find(conversionRate.Conversion.From);
            if (baseCurrency == null)
            {
                var message = string.Format("Can't find currency [{0}] in the database.", conversionRate.Conversion.From);
                throw new InvalidOperationException(message);
            }
            var foreignCurrency = _currencyRepository.Find(conversionRate.Conversion.To);
            if (foreignCurrency == null)
            {
                var message = string.Format("Can't find currency [{0}] in the database.", conversionRate.Conversion.To);
                throw new InvalidOperationException(message);
            }
            var buyRate = MoneyMath.Round(conversionRate.Rate * (decimal) Randomizer.FromRange(_settings.BuyRateCoefficientRange), rounding);
            var sellRate = MoneyMath.Round(conversionRate.Rate * (decimal) Randomizer.FromRange(_settings.SellRateCoefficientRange), rounding);
            return ExchangeRate.Create(baseCurrency, foreignCurrency, buyRate, sellRate, timstampUtc);
        }

        public ExchangeRate CalculateCrossRate(ExchangeRate rate1, ExchangeRate rate2, DateTime timestampUtc)
        {
            Argument.NotNull(rate1, "rate1");
            Argument.NotNull(rate2, "rate2");

            if (rate1.Base.ISOName != rate2.Base.ISOName)
            {
                throw new ArgumentException("Can't calculate cross rate for rates with different base currencies.");
            }
            if (rate1.Foreign.ISOName == rate2.Foreign.ISOName)
            {
                throw new ArgumentException("Can't calculate cross rate for rates with same foreign currencies.");
            }
            var buyRate = rate1.BuyRate / rate2.SellRate;
            var sellRate = rate1.SellRate / rate2.BuyRate;

            return ExchangeRate.Create(rate1.Foreign, rate2.Foreign, buyRate, sellRate, timestampUtc);
        }
    }    
}