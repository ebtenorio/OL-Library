using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrderLinc.DTOs
{
    public class DTOMailConfig
    {
        public string HostName { get; set; }

        public int Port { get; set; }

        public bool UseDefaultCredential { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string SenderEmail { get; set; }

        public string CCMail { get; set; }
    }
}
