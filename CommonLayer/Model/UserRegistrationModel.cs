using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;


namespace CommonLayer.Model
{
    public class UserRegistrationModel
    {
        [Key]
        public long UserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public long PhoneNum { get; set; }
    }
}
