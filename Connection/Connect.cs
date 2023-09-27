using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiSituacaoContratante.Connection
{
    //Classe de conexão ao banco de dados:
    internal class Connect
    {
        private static string server = "192.168.17.196";
        private static string dataBase = "BancoD";
        private static string user = "sa";
        private static string password = "p@dr@o868";

        public static string StrCon
        {
            get
            {
                return "Data Source=" + server + "; Integrated Security=False;Initial Catalog=" + dataBase + "; User ID=" +
                    user + "; Password=" + password;
            }
        }

    }
}
