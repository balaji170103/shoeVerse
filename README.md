shoeVerse

Project Overview
shoeVerse is an ASP.NET Core web application designed for managing and showcasing a collection of shoes. It includes features such as user authentication, product management, and an about page. The application uses Entity Framework Core for database management and ASP.NET Core Identity for authentication.

Features
User Authentication: Register, login, and manage user accounts.

Product Management: Add, view, and manage shoe products.

About Page: Learn more about the shoeVerse project and team.

Database Integration: Uses Azure SQL Database for storing product and user data.

Technologies Used
Backend: ASP.NET Core, Entity Framework Core

Frontend: Razor Pages, HTML, CSS

Database: Azure SQL Database

Authentication: ASP.NET Core Identity

Hosting: Azure App Service

Getting Started
Prerequisites
.NET 8 SDK

Visual Studio 2022 (or any code editor with .NET support)

Azure Account (for deploying to Azure)

Azure SQL Database (for database hosting)

Setup Instructions
1. Clone the Repository
bash
Copy
git clone https://github.com/your-username/shoeVerse.git
cd shoeVerse
2. Configure the Database
Create an Azure SQL Database in the Azure Portal.

Update the connection string in appsettings.json:

json
Copy
{
  "ConnectionStrings": {
    "shoeVerseContext": "Server=tcp:<your-server-name>.database.windows.net,1433;Initial Catalog=shoeVerse;User ID=<your-username>;Password=<your-password>;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  }
}
Apply database migrations:

bash
Copy
dotnet ef database update --context shoeVerseContext
3. Run the Application
Build and run the application:

bash
Copy
dotnet build
dotnet run
Open your browser and navigate to https://localhost:5001.

Seed Data
The application includes a SeedData.cs file to populate the database with initial product data. Ensure the seed method is called in Program.cs:

csharp
Copy
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        SeedData.Initialize(services);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding data: {ex.Message}");
    }
}
Publishing to Azure
1. Publish the Application
Right-click the project in Visual Studio and select Publish.

Choose Azure as the target and create a new Azure App Service.

Follow the prompts to configure the App Service and deploy the application.

2. Configure Connection Strings in Azure
Go to the Azure Portal and navigate to your App Service.

Under Settings, click Configuration.

Add the connection string for your Azure SQL Database.

3. Test the Application
Navigate to the URL of your Azure App Service.

Verify that the application is working and the database is seeded correctly.

Project Structure
Copy
shoeVerse/
├── Controllers/          # MVC Controllers
├── Data/                # Database context and migrations
├── Models/              # Entity models (e.g., Product, User)
├── Views/               # Razor views
├── wwwroot/             # Static files (CSS, JS, images)
├── Program.cs           # Application entry point
├── appsettings.json     # Configuration file
└── README.md            # Project documentation
Contributing
We welcome contributions! If you'd like to contribute to shoeVerse, please follow these steps:

Fork the repository.

Create a new branch for your feature or bugfix.

Submit a pull request with a detailed description of your changes.

License
This project is licensed under the MIT License. See the LICENSE file for details.

Acknowledgments
Thanks to the ASP.NET Core team for the amazing framework.

Special thanks to our team members: Kenechi, Balaji, and Siva.
