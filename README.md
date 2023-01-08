# API - Movies API

<p align="center">
    <img src="https://skillicons.dev/icons?i=git,cs,dotnet,azure,visualstudio" />
</p>

🌎 Deploy -  

 - https://moviesapiapp.azurewebsites.net/api/welcome

 ⚒️ Installation - for run in local

 - You need to have SQL Server 2019
 - Go to "appsettings.json" and update the connection string 👇
    "Server=.\\SQLExpress;Database=moviesapi;TrustServerCertificate=True;Trusted_Connection=True;"
        remember that "\\SQLExpress" is the instance if you instance has a other name you need to change it.
 - On package manager console write :
   - Remove-Migration
   - Add-Migration newmigration
   - Update-Database

▶️ Usage 

 - Open Visual Studio and Start it.
 - or in your terminal run ---> dotnet run 
 - You need postman or insomnia to use OR CAN USE SWAGGER
 - user default 👇
````
{
  "userName": "admin",
  "password": "1234"
}
````

## YOU NEED TO USE TOKEN, ALL ENDPOINTS LIKE POST, PUT OR DELETE ARE RESTRICTED TO ANONYMOUS USER EXCEPT POST FOR REGISTER OR LOGIN

📍 Endpoints
````
Users { GET, POST }
    - POST - /api/users/register - register as a new user
    {
      "userName": "admin", 
	    "name": "test",
      "password": "1234",
	    "role": "admin"
    }
    - POST - /api/users/login - login
    {
      "userName": "admin",
      "password": "1234"
    }
    response: 
    {
	    "request_status": "successful",
	    "response": {
		  "user": {
			  "id": 2,
			  "userName": "admin",
			  "name": "test",
			  "password": "81dc9bdb52d04dc20036dbd8313ed055",
			  "role": "admin"
		  },
		  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImFkbWluIiwicm9sZSI6ImFkbWluIiwibmJmIjoxNjczMTIwMzc5LCJleHAiOjE2NzM3MjUxNzksImlhdCI6MTY3MzEyMDM3OX0.Om36PyCnj4Ky_kb5FfwbHc089ObEy331PcAsuTg64Sw" ##### YOU NEED TO COPY THE TOKEN AND PUT ON BEARER TOKEN #####
	}
}
````
````
Movies { GET, POST, PUT, DELETE }
    - GET /api/movies - get all movies on database.
    - GET /api/movies/{id} - you need put an id by params and this endpoint will return the specific movie.
    - GET /api/movies/{order} - you need put true or false by params for get all movies, if is true order by asc and false order by desc.
    - GET /api/movies/search - you need to send by query with var name the movie to be search .
    - GET /api/movies/GetMovieOnCategory/{categoryId} - you need put a category id by params and this endpoint will return the specific movie into category or categories.
    - POST /api/movies - create a new movie with json body like this example below
    {
      "name": "string",
      "img": "string",
      "description": "string",
      "length": 0,
      "classification": 0,
      "categoryId": 0
    }
    - PUT /api/movies - update movie with json body like this example below
    {
      "id": 0, here put the movie id that you want to update
      "name": "string",
      "img": "string",
      "description": "string",
      "length": 0,
      "classification": 0,
      "categoryId": 0
    }
    - DELETE /api/movies/{id} - you need put an id by params for delete specif movie.
````
````
    - GET /api/categories - get all categories on database.
    - GET /api/categories/{id} - you need put an id by params and this endpoint will return the specific category.
    - GET /api/categories/{order} - you need put true or false by params for get all categories, if is true order by asc and false order by desc.
    - POST /api/categories - create a new category with json body like this example below
    {
      "name": "string",
    }
    - PUT /api/categories - update movie with json body like this example below
    {
      "id": 0, --- here put the category id that you want to update
      "name": "string",
    }
    - DELETE /api/categories/{id} - you need put an id by params for delete specif category.
````

🧗🏽‍♂️ Contributing

 - Just me Aaron Fraga :bowtie:

🔖 License

 - It's free :smiley: :smiley: