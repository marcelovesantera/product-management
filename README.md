# WebAPI de Controle de Produtos

Este projeto é uma API web construída com .Net Core que permite aos usuários fazer operações básicas de CRUD (Create, Read, Update, Delete).

## Tecnologias Utilizadas

- **.Net Core**: Framework para C#.
- **Dapper**: MicroORM utilizado para persistir dados.
- **SQLite**: Usado para salvar tabela em memória.

## Como Começar

Para rodar este projeto localmente, siga estes passos:

1. Clone este repositório.
2. Navegue até o diretório do projeto 'product-management'.
3. Instale as dependências com `dotnet restore`.
4. Inicie o servidor de desenvolvimento com `dotnet run --launch-profile https`.
5. Abra [https://localhost:7075/swagger](https://localhost:7075/swagger) no navegador para visualizar o aplicativo.
