using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBankungCustomer.Lib.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string email { get; set; }

        public string login { get; set; }
        public string Password { get; set; }
    }
}
