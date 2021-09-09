# CQRS,DAPPER SAMPLE



Used CQRS approach and Domain Driven Design.

Read Model - executing raw SQL scripts on database views objects (using Dapper).

Write Model - Domain Driven Design approach (using Entity Framework Core).

Commands/Queries/Domain Events handling using MediatR library.

----------------------------------------------------

----------IMPORTANT-----------

- BEFORE USE, PLEASE CHANGE CONNECTION STRING "ORDERSCONNECTIONSTRING" ON APPSETTINGS AND ALBELLI_INTEGRATIONTESTS_CONNECTIONSTRING ENVIRONMENT VARIABLE FOR TESTS.

- WITH UPDATE-DATABASE COMMAND DATABASE WILL BE CREATED. 
  AFTER CREATION PLEASE EXECUTE "INITIALIZEDATABASE.SQL" SCRIPT FOR SEED DATA.
