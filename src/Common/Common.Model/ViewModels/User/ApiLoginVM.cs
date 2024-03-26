using System;
namespace Common.Model
{
    public class ApiLoginVM
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime DateRegister { get; set; }
    }
}