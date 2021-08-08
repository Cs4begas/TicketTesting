#!/bin/bash
echo "Start"
cd ./Liquibase/workspace

dbUrl="jdbc:postgresql://tickettesting_postgres:5432/tickettesting"
dbUserName="postgres"
dbPassword="password"


java -jar liquibase.jar \
	--driver=org.postgresql.Driver \
	--classpath=./lib/postgresql.jar \
	--url=$dbUrl \
	--changeLogFile=./changelog/changelog-initschema.xml \
	--username=$dbUserName\
	--password=$dbPassword\
	--defaultSchemaName=public\
	update;
java -jar liquibase.jar \
	--driver=org.postgresql.Driver \
	--classpath=./lib/postgresql.jar \
	--url=$dbUrl \
	--changeLogFile=./changelog/changelog-master.xml \
	--username=$dbUserName\
	--password=$dbPassword\
	--defaultSchemaName=tickettesting\
	update && cd /app/out \
	&& dotnet TicketTesting.dll