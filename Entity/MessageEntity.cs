using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecretMessage.Entity
{
    public class MessageEntity
    {
        public string Message { get; set; }
        public string SenderUID { get; set; }
        public string ReceiverUID { get; set; }
        public DateTime Time { get; set; }
    }
}
