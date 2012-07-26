using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengR.Models
{
    public class Message
    {
        public bool IsMine { get; set; }
        public User From { get; set; }
        public string Value { get; set; }
        public DateTime DateReceived { get; set; }
    }
}
