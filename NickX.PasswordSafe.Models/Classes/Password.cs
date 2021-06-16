using MongoDB.Bson.Serialization.Attributes;
using NickX.InventoryManagement.Models.Abstracts;
using System.Collections.Generic;

namespace NickX.InventoryManagement.Models.Classes
{
    [BsonIgnoreExtraElements]
    public class Password : ModelBase
    {
        public string Description { get; set; }
        public string Key { get; set; } // TODO: <Password> - This is bullshit, never keep passwords as plaintext in memory -> find alternative!
        public string Username { get; set; }
        public string Domain { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> AdditionalInformation { get; set; } 
        public string DisplayColor { get; set; }
    }
}
