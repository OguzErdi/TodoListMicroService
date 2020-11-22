using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace User.Core.Entities
{
    public class UserEntity
    {
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Type => typeof(UserEntity).Name;
    }
}
