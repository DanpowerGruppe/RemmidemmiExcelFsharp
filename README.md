"# RemmidemmiObjRepro" 

Install Docker
Run: docker pull microsoft/mssql-server-linux:2017-latest
then

PS docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<Passw0rd>" -p 1433:1433 --name sql1 -d microsoft/mssql-server-linux:2017-latest