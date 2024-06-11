# Documentação da Aplicação

## Descrição
Esta aplicação é um sistema de gerenciamento de gestão de estoque

## Tecnologias Utilizadas
- **Linguagem de Programação:** C#
- **Framework:** .NET Core
- **Banco de Dados:** PostgreSQL
- **ORM:** Entity Framework Core
- **API Framework:** ASP.NET Core Web API

## Funcionalidades Principais

### Clientes
- Adicionar, listar, atualizar e excluir clientes.
- Obter a quantidade total de clientes.

### Produtos
- Adicionar, listar, atualizar e excluir produtos.
- Obter a quantidade total de produtos.
- Obter os produtos com maior estoque.
- Obter os produtos com estoque zerado ou negativo.

### Autenticação
- Autenticar usuários através de login.

## Estrutura do Projeto
- **ms_spa.Api:** Projeto principal da aplicação, contendo a lógica da API e suas Controllers
- **ms_spa.Api.Contract:** Contratos de dados usados na API.
- **ms_spa.Api.Domain:** Lógica de negócio da aplicação.
- **ms_spa.Api.Data:** Configuração do banco de dados e acesso aos dados.
- **ms_spa.Api.Exceptions:** Exceções personalizadas da aplicação.
- **ms_spa.Api.Controllers:** Controladores da API para manipulação de recursos.
- **ms_spa.Api.Services:** Serviços de domínio para lidar com a lógica de negócios.

## Configuração do Ambiente de Desenvolvimento

1. Instale o .NET Core SDK.
2. Configure um banco de dados PostgreSQL e atualize a string de conexão no arquivo `appsettings.json`.
3. Execute o comando `dotnet ef database update` para aplicar as migrações e criar o banco de dados.

## Execução da Aplicação

1. Execute o comando `dotnet watch run` na raiz do projeto para iniciar a aplicação, caminho: `cd src && cd ms-spa.Api`.
2. A API estará disponível em `http://localhost:8080`.

## Execução dos Testes Unitários

1. Execute o comando `dotnet test` no caminho: `cd src && cd ms-spa.Test`.
2. Caso esteja no VsCode podera rodar na extensão `.NET Core Test Explorer`.
3. Sendo testado somente, `services`, `repositorys`, `controllers`.



