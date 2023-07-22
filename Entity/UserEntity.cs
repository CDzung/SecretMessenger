using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretMessage.Entity
{
    public class UserEntity
    {
        public string Uid { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Provider { get; set; }
        public string PhotoUrl { get; set; }
    }
}
