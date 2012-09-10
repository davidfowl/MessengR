using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengR.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public string Value { get; set; }
        public DateTime DateReceived { get; set; }

        public virtual bool IsMine { get; set; }
        public virtual User Initiator { get; set; }
        public virtual User Contact { get; set; }
    }
}
