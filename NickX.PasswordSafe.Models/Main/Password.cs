using System;

namespace NickX.PasswordSafe.Models.Main
{
    public class Password
    {
        public int Id { get; set; }
        public DateTime DateCreate { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Username { get; set; }
        public string Key { get; set; }
        public string Url { get; set; }
        public string Domain { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
