using System;
using System.Collections.Generic;
using System.Text;

namespace wplanr.Core.ConfigurationModels
{
    public class JwtAuthenticationSettings
    {
        public int ExpiryInMinutes { get; set; }
        public int TempExpiryInMinutes { get; set; }
        public string SigningKey { get; set; }
        public string TempSigningKey { get; set; }
        public bool ValidateSigningKey { get; set; }
        public string Audience { get; set; }
        public bool ValidateAudience { get; set; }
        public string Issuer { get; set; }
        public bool ValidateIssuer { get; set; }
    }
}
