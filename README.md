# Lab17 ArtisanalAPI
Hand crafted, artisanal API

## Objective
Create an API that allows a user to create individual todo tasks,
and put them in a todo list

---
## Overview
This is an API built on .NET Core that conducts HTTP GET,
POST, PUT, and DELETE actions.  This todo API consists of 
individual tasks that can be saved into a SQL database and 
be extracted as needed.  This API allows the user to view all
tasks, or all todo lists, in a JSON format with a GET request.

The deployed API can be found [here](http://artisanalapi.azurewebsites.net/api/).

---
## Dependencies
This application runs on .NET Core 2.1, which can be downloaded [here](https://www.microsoft.com/net/download/macos).
The API database relies on the Microsoft Entity Framework. 
Directions are below.

---
## Build
After installing the [.NET Core 2.1 SDK](https://www.microsoft.com/net/download/macos), clone this repo onto your machine. From a terminal interface, go to where this was cloned and type the following commands:

```
cd Lab17_ArtisanalAPI
dotnet restore
dotnet run
```

The database runs on the Entity Framework, which is installed
with a nuget package.  To run the API locally, run the following 
commands:

```
Install-Package Microsoft.EntityFrameworkCore.Tools -Version 2.1.1
Add-Migration initial
Update-Database
```

[Postman](https://www.getpostman.com/) was used to test the 
API end points.  Postman was also used to populate the deployed database.
The deployed site of todo lists can be found [here](http://artisanalapi.azurewebsites.net/api/todolist).
The deployed site of todo items can be found [here](http://artisanalapi.azurewebsites.net/api/todo).


---
## API Endpoint Routes and Explanation
After following the build directions, run the API and navigate 
to the following endpoints:

#### GET all todo items:

**localhost:yourPortNumber/api/todo**

This route will get all the todo items.

#### GET a specific todo item:

**localhost:yourPortNumber/api/todo/{number}**

This route will get a specific todo item, where "number" is 
associated with the task number.  If it exists, the item will 
be displayed, otherwise the user will be routed to another page.

#### GET all todo lists:

**localhost:yourPortNumber/api/todolist**

This route will get all the todo lists.

#### GET a specific todo lists and its items:

**localhost:yourPortNumber/api/todolist/{number}**

This route will get a specific todo list, where "number" is 
associated with the list number.  If it exists, the list and 
all its associated items will be displayed, 
otherwise the user will be routed to another page.

---
## Screenshots
Get all todo items:

![all todo items](/assets/getAllItems.png)

Get specific todo item:

![specific todo item](/assets/getSpecificItem.png)

Get all todo lists:

![all todo lists](/assets/getAllLists.png)

Get specific todo list:

![specific todo list](/assets/getSpecificList.png)


---
## Acknowledgements
- [jaatay](https://github.com/jaatay), [IndigoShock](https://github.com/IndigoShock)
and I trudged through this together.

- Many thanks to [taylorjoshuaw](https://github.com/taylorjoshuaw) 
for this awesome README layout.