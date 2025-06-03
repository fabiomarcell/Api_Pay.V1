# Api_Pay.V1

em appsettings.Development, configurar 
``"Variaveis": {
  "JWT": "hash para o token jwt"
},
  "MongoDbSettings": {
    "ConnectionString": "HOST DO BD",
    "DatabaseName": "NOME DO BD"
}``

Para subir as imagens com ambiente .net, e mongodb

``docker build -t apipay .``
``docker run -d -p 7001:7001 --name apipay apipay``
``docker-compose up --build``
