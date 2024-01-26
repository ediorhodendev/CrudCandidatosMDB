# Projeto CRUD de Produtos

Este é um projeto de exemplo que demonstra como criar uma aplicação de CRUD de candidatos usando Angular 13 e .NET 6, com funcionalidades de criação, atualização, exclusão e listagem de candidatos. Além disso, a aplicação inclui validações de CPF e email e foi construída com o uso da biblioteca Po-UI para a interface do usuário.

## Estrutura da Solution

A Solution consiste nos seguintes projetos:

- **CandidatoApp**: O projeto principal que contém a API da aplicação.
- **Candidato.Application**: Camada de aplicação que lida com a lógica de negócios.
- **Candidato.Domain**: Camada de domínio que define os modelos de dados da aplicação.
- **Candidato.Infrastructure**: Camada de infraestrutura que lida com o acesso ao banco de dados e implementações concretas.
- **Candidato.Tests**: Projetos de testes unitários para testar a aplicação.

## Funcionalidades

- Cadastro de um novo candidato.
- Atualização de um candidato existente.
- Exclusão de um candidato.
- Listagem de todos os candidatos.
- Validação de CPF e email.
- Mensagens de erro detalhadas para validações de campos.

## Requisitos

- .NET 6 ou superior.
- Entity Framework Core.
- Docker (para o banco de dados mongo DB).
- Swagger/OpenAPI para documentação da API.
- Testes unitários para garantir a qualidade do código.


## Configuração

Certifique-se de configurar a string de conexão com o banco de dados MongoDB no arquivo `appsettings.json` do projeto `CrudProdutosApi`.
 },
 "ConnectionStrings": {
   "MongoDBConnection": "mongodb://root:totus123@localhost:27017",
   "MongoDBDatabaseName": "candidato-api-mdb"
   
Se necessário altera as portas. 
### Utilizando Docker e MongoDB

Este projeto utiliza Docker para executar um contêiner do MongoDB. Siga as instruções abaixo:

1. **Instale o Docker**: Se você ainda não tiver o Docker instalado, faça o download e instale-o a partir do [site oficial do Docker](https://www.docker.com/get-started).

2. **Execute o Contêiner do mongo db**:

   Execute o seguinte comando no terminal para baixar e iniciar um contêiner do MongoDB: na raiz do projeto execute o comando para criar as imagens no docker
   
   docker-compose up
   Obs: verifique se o arquivo docker-compose.yml está na raiz do projeto.
