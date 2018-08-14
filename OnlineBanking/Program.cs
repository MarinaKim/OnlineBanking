

using OnlineBankungCustomer.Lib;
using OnlineBankungCustomer.Lib.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking
{
    class Program
    {
        static void Main(string[] args)
        {

            //ServiceXmlDoument service = new ServiceXmlDoument(@"\\dc\Студенты\ПКО\SMP-172.1\MK.xml");
            //Operator oper = new Operator();
            //oper.name = "Beeline5";
            //oper.percent = 0.1;
            //oper.prefixes.Add(new Prefix() { Pref = 778 });
            //oper.Logo = "bee";
            //try
            //{
            //    service.CreateOperator(oper);
            //    Console.WriteLine("Оператор успешно добавлен");
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}



            ServiceXmlDocument service = new ServiceXmlDocument(@"\\dc\Студенты\ПКО\SMP-172.1\МК");
            
            try
            {
                User user = new User();

                user.email = "jhsgd@jshd/ru";
                user.login = "jsagd";
                user.Password = "6454kjfk";

                service.CreateUser(user);
                Console.WriteLine("пользователь успешно добавлен");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
