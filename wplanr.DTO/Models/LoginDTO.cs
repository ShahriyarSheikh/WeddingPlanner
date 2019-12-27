using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace wplanr.DTO.Models
{
    public class LoginDTO
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string Token { get; set; }
        public bool IsLoggedIn { get; set; }
    }
}
