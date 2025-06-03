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
Caso queira subir a imagem somente com a api(não recomendado por não conter arquivos de configuração)

``docker build -t apipay .``
``docker run -d -p 7001:7001 --name apipay apipay``

Subindo a aplicação com banco de dados
``docker-compose up --build``
