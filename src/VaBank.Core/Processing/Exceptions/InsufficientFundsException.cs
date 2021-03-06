﻿using VaBank.Common.Validation;
using VaBank.Core.Common;

namespace VaBank.Core.Processing.Exceptions
{
    public class InsufficientFundsException : DomainException
    {
        private readonly decimal _balance;
        private readonly decimal _requestedAmount;

        public InsufficientFundsException(decimal balance, decimal requestedAmount)
            :base(string.Format(Messages.InsufficientFundsErrorMessage, balance, requestedAmount))
        {
            Argument.Satisfies(requestedAmount, x => x > balance, "requestedAmount");
            _balance = balance;
            _requestedAmount = requestedAmount;
        }

        public decimal Balance
        {
            get { return _balance; }
        }

        public decimal RequestedAmount
        {
            get { return _requestedAmount; }
        }
    }
}
