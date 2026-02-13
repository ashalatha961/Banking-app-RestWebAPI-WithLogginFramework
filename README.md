# Banking-app--WebAPI---WithLogginFramework
*** Backing application built using C#, .NETcore 8. *** It is a banking web api where we can use swagger and hit end point and apply CRUD operation on factory data to persist data across application to find deposit withdraw and transaction.

Programming language & Framworks:

	C#, .NET core 8, Entity Framework, XUNit framework, Fluent Validation, ILogging framework
Design pattrens:

	I have used design patterns like repository pattern and dependency injection making code more modular and testable.
Database base:

	- To Generating Migration and Syncing with Database, open the NuGet Package Manager Console in Visual Studio by selecting Tools => NuGet Package Manager => Package Manager Console. Then, execute the command.
	- "Add-Migration BankingAppDB"
	- Once build is succeded. Which means after creating the migration file, we need to update the database using the "Update-Database" command. you can use–verbose option to view the generated SQL statements executed in the target database.
	- "Update-Database -Verbose"
	- Once the above command is executed successfully, it will generate and execute the required SQL Statements in the defined database.
	- Now sign in to you SSMS and can find the BankingDB under your DB and can see all the tables created matching with Entity classes / database tables that we specified as the DbSet property. Ex: address, Categories, Customers, Memborship Tiers etc., 
Testing framework:

	Used Xunit framework with NSubstitute and Moq. NSubstitute and Moq are popular, feature-rich .NET mocking frameworks used to simulate dependencies (interfaces/classes) in unit tests, with NSubstitute often praised for its cleaner, more concise syntax, while Moq is widely used with extensive options for verifying behavior. 
Testable both in swagger and postman
