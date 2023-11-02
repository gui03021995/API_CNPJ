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
        private static string server = "xxx.xxx.xxx";
        private static string dataBase = "Nome_Banco";
        private static string user = "sa";
        private static string password = "senha";

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
