﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using VaBank.Common.Validation;
using VaBank.Core.Membership.Entities;

namespace VaBank.Core.Accounting.Entities
{
    public class CardAccount : UserAccount
    {
        internal CardAccount(string accountNo, Currency currency, User owner, Bank bank)
            :base(currency, owner, bank)
        {
            Argument.EnsureIsValid<AccountNumberValidator, string>(accountNo, "accountNo");
            AccountNo = accountNo;
            Type = "CardAccount";
            Cards = new Collection<UserCard>();            
        }

        protected CardAccount()
        {
            Type = "CardAccount";
        }

        public virtual ICollection<UserCard> Cards { get; protected set; }
    }
}
