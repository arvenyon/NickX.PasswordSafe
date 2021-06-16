using NickX.InventoryManagement.Models.Interfaces;
using System;
using System.Text.Json;

namespace NickX.InventoryManagement.Models.Abstracts
{
    public abstract class ModelBase : IModel
    {
        public Guid Guid { get; set; }
        public DateTime DateCreate { get; set; }
        public string DisplayName { get; set; }
        
        public static ModelBase FromJson(string json)
        {
            return JsonSerializer.Deserialize<ModelBase>(json);
        }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
