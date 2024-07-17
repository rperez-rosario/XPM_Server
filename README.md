8# Introduction 
An ASP.NET Core Web Api to access and manipulate project data using EF Core on a PostgreSQL or SQL Server database.

Data Entity Relationship Diagram
![XPM_ERD](https://github.com/user-attachments/assets/b8ab8c5f-e001-484b-8898-a196a9ce027b)

Endpoint Swagger View
<img width="922" alt="Screenshot 2024-07-17 171829" src="https://github.com/user-attachments/assets/8e0efa80-47ba-4bda-be41-e84f4d769b36">

# Getting Started
1.	Installation process:

(See "Build and Test" section of this document.)

2.	Software dependencies:

<ul>
<li>ASP.NET Core 8.0</li>
<li>Entity Framework Core</li>
<li>C#</li>
</ul>

3.	Web API, languages and technology stack references:

<ul>
<li>https://docs.microsoft.com/en-us/dotnet/csharp/</li>
<li>https://docs.microsoft.com/en-us/ef/</li>
<li>https://www.postgresql.org/docs/</li>
<li>https://www.microsoft.com/en-us/sql-server/developer-tools?msockid=02f21cdf3f0c66e81a900e5e3e256728</li>
<li>https://www.iis.net/</li>
<li>https://azure.microsoft.com/en-us/</li>
</ul>

# Build and Test
1. Create and configure a private application within the target Shopify store.
2. Configure additional external services (ShipEngine, SendGrid, TaxJar and Stripe.) 
3. Web or folder deploy to IIS or Azure Cloud App (.NET Core 8.0 application pool), 
configure appsettings.json as needed.
4. Build and execute Visual Studio solution.
