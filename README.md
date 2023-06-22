## WarehouseWebApi
This Web API project serves as a warehouse system for managing the inventory of a warehouse. It allows users to perform CRUD (create, read, update, delete) operations on pallets with materials and creating orders.

### Prerequisites
To run this project, you'll need the following:

.NET 6 SDK installed on your machine, an IDE such as Visual Studio or Visual Studio Code installing and SQL Server.

Clone this repository to your local machine:<br/>
````git clone https://github.com/IhorZhylchuk/WarehouseWebApi.git````
<br/>
<br/>
Navigate to the project directory and restore dependencies:
<br/>
```cd YOUR_REPOSITORY```<br/>
```dotnet restore```<br/><br/>

Open project in IDE, in ```appsettings.json``` entere you server name, run the following commands:<br/>
```add-migration MIGRATION_NAME``` then run <br/>
```update-database```<br/>

You can run the app by pressing the "Play" button in your IDE or by running the following command in the terminal:
<br/>
```dotnet run```
<br/><br/>
To test the API, open browser and navigate to http://localhost:5000/swagger/index.html (the port number may be changed, when you run ```dotnet run``` command it will show which port is listening). This will bring the Swagger UI where you can expore the API endpoints


