using System;

namespace NickX.InventoryManagement.Models.Interfaces
{
    public interface IModel
    {
        Guid Guid { get; set; }
        DateTime DateCreate { get; set; }
        string DisplayName { get; set; }
    }
}
