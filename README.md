# Atualizador de Situação de CNPJ

Este é um aplicativo de console que atualiza a situação cadastral de CNPJ em um banco de dados usando uma API pública.

## Requisitos

Certifique-se de ter os seguintes requisitos instalados:

- .NET Core SDK
- SQL Server (ou outro banco de dados compatível)

## Configuração

1. Clone este repositório em seu ambiente de desenvolvimento:
git clone https://github.com/gui03021995/API_CNPJ.git

2. Navegue até o diretório do projeto:
cd API_CNPJ


3. Abra o arquivo `Connect.cs` e configure a string de conexão com seu banco de dados SQL Server.

4. Execute o aplicativo usando o seguinte comando:
dotnet run


O aplicativo começará a atualizar a situação cadastral de CNPJ em seu banco de dados.

## Funcionalidades

- Conecta-se a um banco de dados SQL Server.
- Consulta registros sem situação cadastral de CNPJ.
- Faz solicitações HTTP para uma API pública.
- Atualiza registros no banco de dados com a situação cadastral recuperada da API.

## Contribuindo

Sinta-se à vontade para contribuir para este projeto criando pull requests e reportando problemas.




