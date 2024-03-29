﻿using System.Collections.Generic;

namespace NickX.PasswordSafe.WebAPI.Domain.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Password> Passwords { get; set; }
    }
}
