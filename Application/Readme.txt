https://blog.georgekosmidis.net/2020/07/11/swagger-in-asp-net-core-tips-and-tricks/
https://ppolyzos.com/2017/10/30/add-jwt-bearer-authorization-to-swagger-and-asp-net-core/
https://didourebai.medium.com/add-swagger-to-asp-net-core-3-0-web-api-874cb265854c

https://ppolyzos.com/2017/10/30/add-jwt-bearer-authorization-to-swagger-and-asp-net-core/

https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-5.0&tabs=visual-studio

--------------------------------------------------
----- Swagger ------------------------------------
--------------------------------------------------

(1)

Install-Package Swashbuckle.AspNetCore

	Swashbuckle.AspNetCore.Swagger
	Swashbuckle.AspNetCore.SwaggerUI
	Swashbuckle.AspNetCore.SwaggerGen

(2)

Right click on Application -> Properties -> Build ->
Check: XML documentation file check box
Fill Text Box (File Name): Application.xml ([ApplicationName].xml)

Note: Never use path!

Note: After this settings, If we do not use xml documentation
(For classes and members), VS will display some warnings!

URL: https://localhost:[PORT]/swagger/index.html

(3)
Properties -> launchSettings.json -> "launchUrl" -> "swagger"

--------------------------------------------------
