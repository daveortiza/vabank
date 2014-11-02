﻿namespace VaBank.Data.EntityFramework.Common
{
    internal static class Restrict
    {
        public static class Length
        {
            public const int Name = 100;
            public const int ShortName = 50;
            public const int Email = 50;
            public const int PhoneNumber = 30;
            public const int BigString = 1024;
            public const int SecurityString = 256;
            public const int Url = 256;
        }
    }
}