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

# Uso da API

## Autenticação

### Login

Realiza a autenticação do usuário e retorna um token JWT.

**Requisição:**
```bash
curl -X 'POST' \
  'https://localhost:7001/login' \
  -H 'accept: application/json' \
  -H 'Content-Type: application/json' \
  -d '{
    "email": "fabio",
    "password": "teste"
}'
```
**Resposta:**
```json
{
  "isSuccess": true,
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "message": "Login realizado com sucesso"
}
```

## Logs

### Consultar Logs

Retorna os registros de eventos da aplicação.

**Requisição:**
```bash
curl -X 'GET'
'https://localhost:7001/logs'
-H 'accept: application/json'
```
**Resposta:**
```json
{
"logs": [
"2025-05-31 01:33:30.4347|INFO|PID=27072|Login >>> Houve um erro na validação do token...",
"2025-06-02 15:24:12.7379|INFO|PID=10660|EfetuarEstorno >>>Pagamento não localizado no banco de dados >>> "97"",
...
]
}
```

## Pagamentos

### Criar Pagamento

Registra um novo pagamento no sistema, e efetua a requisição para um servidor terceiro. Havendo instabilidade em um, outro servidor sofrerá a requisição para criação de pagamentos. Gera um log em casos de falha.

**Requisição:**
```bash
curl -X 'POST' \
'https://localhost:7001/payments' \
-H 'accept: application/json' \
-H 'Authorization: Bearer <seu-token-aqui>' \
-H 'Content-Type: application/json' \
-d '{ 
"amount": 0, 
"currency": "string", 
"description": "string",
"type": "string",
"number": "string",
"holderName": "string",
"cvv": "string",
"expirationDate": "string",
"installments": 0
}'
```
**Resposta:**
```json
{
"id": "98",
"status": "status 98",
"originalAmount": "39",
"currency": "string",
"cardId": "cardId 98"
}
```

---

### Consultar Pagamento por ID

Retorna os detalhes de um pagamento específico. Usa banco de dados para identificar o fornecedor correto, e consulta em seu devido servidor. Gera um log em casos de falha.

**Requisição:**
```bash
curl -X 'GET'
'https://localhost:7001/payments/98' \
-H 'accept: application/json' \
-H 'Authorization: Bearer <seu-token-aqui>' 
```
**Resposta:**
```json
{
"id": "98",
"status": "status 98",
"originalAmount": "39",
"currency": "string",
"cardId": "cardId 98"
}
```

---

### Atualizar Pagamento

Atualiza dados de um pagamento existente. Usa banco de dados para identificar o fornecedor correto, e efetua a alteração de estado em seu devido servidor, e em banco de dados. Gera um log em casos de falha.

**Requisição:**
```bash
curl -X 'PUT' \
'https://localhost:7001/payments/98' \
-H 'accept: application/json' \
-H 'Authorization: Bearer <seu-token-aqui>' \
-H 'Content-Type: application/json' \
-d '{
"amount": 1000
}'
```
**Resposta:**
```json
{
"id": "98",
"status": "status 98",
"originalAmount": "39",
"currency": "string",
"cardId": "cardId 98"
}
```
