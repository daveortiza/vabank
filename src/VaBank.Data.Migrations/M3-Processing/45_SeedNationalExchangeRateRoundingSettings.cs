﻿using System.Collections.Generic;
using FluentMigrator;
using VaBank.Common.Util.Math;

namespace VaBank.Data.Migrations
{
    [Tags("Development", "Test", "Production")]
    [Migration(45, "Seed NationalExchangeRateRoundingSettings.")]
    public class SeedNationalExchangeRateRoundingSettings : Migration
    {
        public override void Up()
        {
            
        }

        public override void Down()
        {
            //Do nothing
        }

        private object GetSettings()
        {
            var usd =
                new
                {
                    CurrencyISOName = "USD",
                    Rounding = new Rounding(new IntegerRounding(IntegerRoundingMode.Ceiling, 10))
                };
            
            var eur =
                new
                {
                    CurrencyISOName = "EUR",
                    Rounding = new Rounding(new IntegerRounding(IntegerRoundingMode.Ceiling, 10))
                };

            var rub =
                new
                {
                    CurrencyISOName = "RUB",
                    Rounding = new Rounding(new IntegerRounding(IntegerRoundingMode.Ceiling, 1))
                };

            var list = new List<object> {usd, eur, rub};

            return new
            {
                NationalExchangeRateRounding = list
            };
        } 
    }
}
