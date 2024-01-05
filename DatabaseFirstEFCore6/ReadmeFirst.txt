Step 1 - Install 3 packages in your Asp.Net Core MVC Application
	Microsoft.EntityFrameworkCore.SqlServer
	Microsoft.EntityFrameworkCore.Tools
	Microsoft.EntityFrameworkCore.Design

Step 2 - Execute a command for Scaffold DbContext	
	Scaffold-DbContext "server=server_name;database=db_name;trusted_connection=true;"
	Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models

Above command will generate Model class and DbContext class automatically acc. to your database table.

Step 2.1 - If we update our database then how we can update our model and DbContext
By using this command...
	Scaffold-DbContext "server=server_name;database=db_name;trusted_connection=true;"
	Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -force

Step 3 - Go to Tools -> Nuget Package Manager -> Package Manager Console and paste above commands 
Note: Remove and add double quotes ("") after pasting commands in Package Manager Console
Info: null! - 'null forgiving operator' just like 'not null operator' in database.

Step 4 - Move Connection String from DbContext Class to appsetting.json before "var app = builder.Build();" line.
Step 5 - Registering Connection String in Program.cs File.
	var provider = builder.Services.BuildServiceProvider();
	var config = provider.GetRequiredService<IConfiguration>();
	builder.Services.AddDbContext<CodeFirstDBContext>(item => item.UseSqlServer(config.GetConnectionString("dbcs")));

