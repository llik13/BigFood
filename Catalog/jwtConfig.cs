﻿namespace Catalog.WebApi
{
    public class jwtConfig
    {
        public string ValidIssuer { get; set; }
        public string ValidAudience {  get; set; }
        public string Secret { get; set; }
    }
}