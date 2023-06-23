## WarehouseWebApi
This Web API project serves as a warehouse system for managing the inventory of a warehouse. It allows users to perform CRUD (create, read, update, delete) operations on pallets with materials and creating orders.

### Prerequisites
To run this project, you'll need the following:

.NET 6 SDK installed on your machine, an IDE such as Visual Studio or Visual Studio Code installing and SQL Server.

Clone this repository to your local machine:<br/>
<b>````git clone https://github.com/IhorZhylchuk/WarehouseWebApi.git````</b>
<br/>
<br/>
Navigate to the project directory and restore dependencies:
<br/>
<b>```cd YOUR_REPOSITORY```</b><br/>
<b>```dotnet restore```</b><br/>

Open project in IDE, in <b>```appsettings.json```</b> enter your server name, run the following commands:<br/>
<b>```add-migration MIGRATION_NAME```</b> then run <br/>
<b>```update-database```</b><br/>

You can run Api by pressing the "Play" button in your IDE or by running the following command in the terminal:
<br/>
<b>```dotnet run```</b>
<br/><br/>
To test the API, open browser and navigate to <b>```http://localhost:5000/swagger/index.html```</b> (the port number may be changed, when you run <b>```dotnet run```</b> command it will show which port is listening). This will bring the Swagger UI where you can expore the API endpoints.


