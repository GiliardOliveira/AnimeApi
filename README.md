# AnimeApi

## Descrição

A **AnimeApi** é uma API RESTful construída com ASP.NET Core, projetada para gerenciar informações sobre animes.  
Ela permite realizar operações CRUD (Criar, Ler, Atualizar, Deletar) em dados relacionados a animes, utilizando **Entity Framework Core** para interação com o banco de dados SQL Server.

O projeto segue princípios de **Clean Architecture** e **Clean Code**, garantindo separação de responsabilidades, manutenção facilitada e código legível.  
Além disso, utiliza **MediatR** para orquestrar os handlers de forma desacoplada, implementando o padrão CQRS de forma simples.

---

## Funcionalidades

- CRUD completo de animes (título, descrição, gênero, número de episódios, imagem).  
- Estrutura baseada em **Clean Architecture / Clean Code**.  
- Uso de **MediatR** para mediar requisições e organizar handlers.  
- Logs com **Serilog**.  
- Suporte a migrations para gerenciamento de banco de dados.  
- Testes unitários com xUnit.

---

## Tecnologias Utilizadas

- **Backend:** ASP.NET Core 8.0  
- **Banco de Dados:** SQL Server  
- **ORM:** Entity Framework Core  
- **Mediator:** MediatR  
- **Logs:** Serilog  
- **Testes:** xUnit  
- **Docker:** Contêinerização da aplicação e banco de dados  
