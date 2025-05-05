# Teste Técnico - Backend

Bem-vindo ao projeto de backend para o teste técnico! Este repositório contém a implementação de uma API para gerenciar vendas, desenvolvida com foco em boas práticas de programação e organização de código.

## Tecnologias Utilizadas

- **C#** com **.NET 6/7/8** (dependendo da versão utilizada no projeto)
- **Entity Framework Core** para manipulação de banco de dados
- **MediatR** para implementação de padrões como CQRS
- **xUnit** para testes unitários
- **Docker** para containerização

## Configuração e Execução

### Pré-requisitos

Certifique-se de ter as seguintes ferramentas instaladas:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [Git](https://git-scm.com/)

### Passos para Configuração

1. Clone o repositório:
   ``` bash
       git clone https://github.com/vinive/Test-Ambev
       cd Test-Ambev
   ```

2. Restaure as dependências do projeto:
    ```dotnet restore .\Ambev.DeveloperEvaluation.sln```

3. Configure o ambiente Docker (se aplicável):
    ```docker-compose up --build```

4. Execute a migration:
    ``` cd .\src\Ambev.DeveloperEvaluation.WebApi 
        dotnet ef database update
    ```

5. Execute o projeto:
    ```dotnet run --project src/Ambev.DeveloperEvaluation.WebApi```

6. Acessar o Swagger 
    ```http://localhost:5119/swagger```

7. Acessando os testes
    ```
        cd .\tests\Ambev.DeveloperEvaluation.Unit
        dotnet test        
    ```

