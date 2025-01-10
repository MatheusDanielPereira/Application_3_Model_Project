using System;

namespace YourProjectName_Domain.Entity
{
    public class LoginEO
    {
        public string UserName { get; set; }
        public int Attempts { get; set; }
        public DateTime LastBlock { get; set; }
    }
}
