using System;

namespace Domain.Model
{
    public class ApiUser
    {
        public int ApiUserId { get; set; }
        public string Role { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Project { get; set; }
        public string Applicant { get; set; }
        public DateTime Expires { get; set; }
    }
}