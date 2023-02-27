using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class UserLogin
    {
        public string EmailId { get; set; }
        public long UserId { get; set; }
        public string token { get; set; }
    }
}
