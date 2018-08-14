using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Admin.Lib.Model
{
   public class Operator
    {
        /// <summary>
        /// наименование оператора
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// процент за обслуживание
        /// </summary>
        public  double percent { get; set; }
        /// <summary>
        /// Логотип компании
        /// </summary>
        public string Logo { get; set; }

        public List<Prefix> prefixes = new List<Prefix>();
    }
}

public class Prefix
{
    public int Pref { get; set; }
}