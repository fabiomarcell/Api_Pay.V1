# Api_Pay.V1

Uma API desenvolvida em .NET que utiliza MongoDB como banco de dados. A aplicação é configurada para autenticação via JWT e também pode ser executada em containers Docker.
A Api tem finalidade de simular integração com meios de pagamentos de forma resiliente, onde caso uma delas caia, outra possa assumir a responsabilidade.
A solução aplica padrões de clean code, e padrões de projeto(como strategy, factory, e repository).
A Aplicação escreve logs estruturados, usando NLog, em arquivos de texto. Também é possível listar esses logs em um endpoint da api.
Existem espaços para melhorias, como migrar o trecho responsável por efetuar requests em filas, evitando uso do webserver, mas não se adequaria ao desafio proposto.

## Tecnologias Utilizadas

- **Backend:** .NET (ASP.NET Core)
- **Banco de Dados:** MongoDB
- **Autenticação:** JWT (JSON Web Token)
- **Containerização:** Docker

## Estrutura do Projeto

```plaintext
/Api_Pay.V1
├── /ApiPay
├── /Application
├── /Domain
├── /Infrastructure
├── /Shared
├── .dockerignore
├── .gitignore
├── Dockerfile
└── docker-compose.yml
```

- **ApiPay:** Contém a configuração da API e inicialização do servidor.
- **Application:** Lógica de negócios e serviços da aplicação.
- **Domain:** Modelos de domínio e interfaces, e lógicas pesadas futuramente(desacoplando assim a infra da application).
- **Infrastructure:** Implementações específicas, como acesso ao banco de dados.
- **Shared:** Componentes reutilizáveis e utilitários.

## Configuração

### Ambiente de Desenvolvimento

No arquivo `appsettings.Development.json`, configure as variáveis de ambiente:

```json
{
  "Variaveis": {
    "JWT": "hash para o token jwt"
  },
  "MongoDbSettings": {
    "ConnectionString": "HOST DO BD",
    "DatabaseName": "NOME DO BD"
  }
}
```

Configuração não necessária para o deploy via docker compose

## Executando com Docker

### Para construir e rodar a aplicação em um container Docker:

Será necessário Existir Mongo db configurado no ambiente.

```bash
docker build -t apipay .
docker run -d -p 7001:7001 --name apipay apipay
```

### Para subir a aplicação junto com o banco de dados utilizando Docker Compose:

```bash
docker-compose up --build
```
