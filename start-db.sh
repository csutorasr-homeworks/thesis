docker run -d --name sql_server_demo -e 'ACCEPT_EULA=Y' -e 'SA_PASSWORD=Password1' -p 1433:1433 microsoft/mssql-server-linux
docker run -d -p 27017:27017 mongo
