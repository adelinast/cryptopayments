Dockerized Cryptopayments application

Programming Language: C#
Unit tests: NUnit
Entity Framework
SQL Server
Docker

#Build#

	docker build -t cryptopayments .

It takes some time to pull from microsoft/dotnet-framework and from microsoft/mssql-server-windows-developer(~4G)

Path to the transactions files can be changed from ENTRYPOINT, found in Dockerfile
If the files are not found, the strings are read from Application.cs

#Run#

	docker run cryptopayments


#Compose#

	docker-compose up

SQL Server needs to be started first and the service for the app needs to wait until init is done

1. Starting SQL server:
	docker-compose up sqldocker

2. Application:
	docker-compose up appcrypto

For building separately db
	https://github.com/Microsoft/mssql-docker/tree/master/windows/mssql-server-windows

	cd db; docker build -t db .;

Run sln outside the container using the SQL server started above:

1. Find available network

	docker network ls
	docker inspect cryptopaymentsdocker_dockermysql-net > network_inpect

	Ex IPv4->172.26.3.167
	netstat -an should show this IP

	telnet 172.26.3.167 1433 -> should connect

2. Needs to be added in App.config-> ConnectionString tag or in TransactionContext.cs
	private string scmConnectionString -> Data Source=172.26.3.167,1433 or DataSource=efmysqldocker\\SQLSERVER,1433
	

#Run in Container#

	docker ps -> get container ID/NAME 
	
	Data Source=CONTAINERID,1433 or CONTAINERNAME=efmysqldocker,1433

	
#Check SQL Server connection inside the container#

	docker exec -it efsqldocker sqlcmd
1> print Suser_Sname()
2> GO
User Manager\ContainerAdministrator

#Using App.config#

  <connectionStrings>
    <add name="ConnectionString" connectionString="Data Source=efmysqldocker\\SQLSERVER,1433;Initial Catalog = CryptoPayments.TransactionContext; User Id=sa; Password=Password1; Persist Security Info = True; Connection Timeout=10" providerName="System.Data.SqlClient" />
  </connectionStrings>

#Logger#
DB is in Logger.txt

The unit rests are in Transaction.Test.cs
tests_playlist represents the list that can be run from Visual Studio

