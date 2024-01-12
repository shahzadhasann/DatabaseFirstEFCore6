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


What is Session?
1. A Session is a state-management technique.
2. Session state is a Asp.Net core scenerio for storage of user data while the user browses a Web App.
3. Session state uses a store maintain by the appto persist data accross request from a client.
4. The Session data is backed by a cache and considered ephemeral data (lasting for a very short time) or temporary data.
5. Critical applicatuion data should be stored in the user database & cached in session only as a performance optimization.
6. The Session is specific to the browser, Sessions are not shared accross browsers.
7. Sessions are deleted when the browser session ends.
8. Sessions are Server-Side and Cookies are Client-Side.
9. Sessions are also used to pass data within the Asp.Net Core MVC application and unlike Tempdata. 
10. It persist (maintain/retain) for its expiration time (default time is 20 min but it can increased or decreased).
11. Session is applicable for all request, not for a single redirect.
12. A session state stores application-specific data in key-value pairs.
13. When multiple users access an application simultaneously, then each of these users will have a different session state.
14. Every Session has a Unique Session Id.



/*********** Ways to Use Session and Retrieve Value from it. **********/

/***********************  A  ****************************/
1. First use namespace 
	using Microsoft.AspNetCore.Http;
2. Create Session in key value-pair
	HttpContext.Session.SetString("MyKey","MySessionValue");
3. Assign it on ViewBag to retrieve its value on View.
	if (HttpContext.Session.GetString("MyKey") != null)
    {
        ViewBag.Data = HttpContext.Session.GetString("MyKey").ToString();
    }

4. Get value on View using '@' symbol. 
	<h6>@ViewBag.Data</h6>
5. You can use it multiple Views or multiple Action methods or 
if you redirect accross whole Application then you get your session.

/***********************  B  ****************************/

1. If you want to access Session directly in View not in Action method then you have to use "HttpContextAccessor".
2. Register Services in Program.cs file
	builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

3. Use these lines on Top of the View page
	using Microsoft.AspNetCore.Http;
	inject Microsoft.AspNetCore.Http.IHttpContextAccessor accessor

4. And retrive like this
	<h6>@accessor.HttpContext.Session.GetString("MyKey")</h6>

Note: a. The lifespan of Session in Asp.Net Core is 20 minutes and after that it automatically destroys.
	  b. It automatically also destroy after closing the browser.
	  c. If you can destroy manually then you can use this line 
		 HttpContext.Session.Remove("MyKey");

