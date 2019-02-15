using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cadastrarClienteSolution.Models
{
    public class ConnectionDb
    {
        public static string Conexao
        {
            get
            {
                return "server=localhost;user id=fabigpn;password=N1kolaTesla;persistsecurityinfo=True;database=testecrmall";
            }
        }
    }
}