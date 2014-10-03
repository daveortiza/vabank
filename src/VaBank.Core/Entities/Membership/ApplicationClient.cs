﻿using System;

namespace VaBank.Core.Entities.Membership
{
    public class ApplicationClient : Entity<Guid>
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public ApplicationClientType ApplicationType { get; set; }
        public string AllowedOrigin { get; set; }        
    }
}