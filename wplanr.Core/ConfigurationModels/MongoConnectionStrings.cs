using System;
using System.Collections.Generic;
using System.Text;

namespace wplanr.Core.ConfigurationModels
{
    public class MongoConnectionStrings
    {
        public string MongoDbName { get; set; }
        public string MongoAgentDbName { get; set; }
        public string MongoConnectionString { get; set; }
    }
}
