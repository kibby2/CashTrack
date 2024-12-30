using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashTrack.DataModel.Model
{
    public class User
    {
        public Guid UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Currency PreferredCurrency { get; set; }
    }
}
