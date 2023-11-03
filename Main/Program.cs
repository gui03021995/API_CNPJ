using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net.Http;
using System.Threading.Tasks;
using ApiSituacaoContratante.Connection;
using Newtonsoft.Json.Linq;

class Program
{
    //Tarefa assíncrona:
    static async Task Main(string[] args)
    {
        int validacao = 0;
        int i = 1;

        using System.Data.SqlClient.SqlConnection cn = new SqlConnection(Connect.StrCon);
        cn.Open();

        var validaScript = $@"select count(*) qtd from Situacao_Contratante
                               where Situacao_CNPJ is null";

        SqlCommand sqlCommand = new SqlCommand(validaScript, cn);
        validacao = Convert.ToInt32(sqlCommand.ExecuteScalar());

        cn.Close();

        //Conexão com o banco de dados:
        do
        {
            try
            {
                cn.Open();

                //Consulta no banco:
                var sqlQuery = $@"SELECT DISTINCT top 1 
	                            Nu_CNPJ_Contratante                           
                              FROM Situacao_Contratante
                              WHERE Situacao_CNPJ IS NULL
                              AND Nu_CNPJ_Contratante IS NOT NULL";

                sqlCommand = new SqlCommand(sqlQuery, cn);
                string cnpj = Convert.ToString(sqlCommand.ExecuteScalar());

                string url = "https://publica.cnpj.ws/cnpj/" + cnpj;

                //Conexão com a API pelo método Http:
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(url);

                    //Se receber o código 200:
                    if (response.IsSuccessStatusCode)
                    {
                        //Obtem todos os dados da resposta do json:
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        JObject data = JObject.Parse(jsonResponse);

                        //Atribui nas variáveis os dados que preciso:
                        string cnpjRaizValue = data["estabelecimento"]?["cnpj"]?.ToString();
                        string situacaoCadastralValue = data["estabelecimento"]?["situacao_cadastral"]?.ToString();

                        //Se os dados não forem vazios, mostro pro usuário se o CNPJ está ativo:
                        if (!string.IsNullOrEmpty(cnpjRaizValue) && !string.IsNullOrEmpty(situacaoCadastralValue))
                        {
                            Console.WriteLine("[{0}]", i + " | " + cnpjRaizValue + " | " + situacaoCadastralValue);

                            //Atualiza o status de "Ativo" no banco:
                            using (SqlCommand command = cn.CreateCommand())
                            {

                                command.CommandText = "UPDATE Situacao_Contratante SET Situacao_CNPJ = @sit WHERE Nu_CNPJ_Contratante = @cnpj";
                                command.Parameters.AddWithValue("@sit", situacaoCadastralValue);
                                command.Parameters.AddWithValue("@cnpj", cnpjRaizValue);

                                command.ExecuteNonQuery();
                            }

                            i++;
                            //Atraso de 20 segundos para pesquisar outro CNPJ (3 por min):
                            await Task.Delay(20000);
                        }
                        else
                        {
                            Console.WriteLine("Campos 'cnpj_raiz' e/ou 'situacao_cadastral' não encontrados no JSON.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Erro ao pesquisar pelo CNPJ informado: " + cnpj);
                    }

                    cn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

            }
            
        } while (validacao != 0);
    }
}
