using NickX.PasswordSafe.WebAPI.Domain.Models;
using System;

namespace NickX.PasswordSafe.WebAPI.Resources
{
    public class PasswordResource
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }
        public Category Category { get; set; }
    }
}
