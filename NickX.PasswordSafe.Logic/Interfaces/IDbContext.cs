using NickX.InventoryManagement.Models.Classes;
using System;
using System.Collections.Generic;

namespace NickX.InventoryManagement.Logic.Interfaces
{
    public interface IDbContext
    {
        // TODO: <IDataContext> - Implement category handling
        string DisplayName { get; set; }
        ICollection<Password> Passwords { get; }
        void CreatePassword(Password password);
        Password GetPassword(Guid guid);
        Password UpdatePassword(Password password);
        void DeletePassword(Password password);
        void ReloadPasswords();
    }
}
