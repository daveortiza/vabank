﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaBank.Services.Contracts.Accounting.Models;
using VaBank.Services.Contracts.Common;
using VaBank.Services.Contracts.Common.Events;

namespace VaBank.Services.Contracts.Accounting.Events
{
    public class UserCardBlocked: ApplicationEvent, IAuditedEvent
    {
        public UserCardBlocked(CardAccountModel cardAccountModel)
        {
            if (cardAccountModel == null)
            {
                throw new ArgumentNullException("user card");
            }
            Code = "USER_CARD_BLOCKED";
            Description = string.Format("User card [{0}] blocked.", cardAccountModel.CardNo);
            Data = null;
        }

        [JsonConstructor]
        protected UserCardBlocked() { }

        [JsonProperty]
        public Guid OperationId { get; private set; }

        [JsonProperty]
        public string Code { get; private set; }

        [JsonProperty]
        public string Description { get; private set; }

        [JsonProperty]
        public object Data { get; private set; }
    }
}
