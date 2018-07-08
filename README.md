# Lab17 ArtisanalAPI
Hand crafted, artisanal API

## Objective
Create an API that allows a user to create individual todo tasks,
and put them in a todo list

---
## Overview
This is an API built on .NET Core 2.1 that conducts HTTP GET,
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

[Swagger](https://docs.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-2.1) 
has been implemented and the deployed site will take you directly to the Swagger UI. 
From there follow the walk through. 

---
## API Endpoint Routes and Explanation
After following the build directions, run the API and navigate 
to the following endpoints:

#### GET all Lists:

**localhost:yourPortNumber/api/todolist** or

**https://artisanalapi.azurewebsites.net/api/todolist**

Click "Try It Out" and then "Execute" in order to see all the lists:

![See All Lists](/assets/getAllListDetails.png)


#### GET a List by ID:

**localhost:yourPortNumber/api/todolist/`{id}`**

**https://artisanalapi.azurewebsites.net/api/todolist/{id}**

where id is the id of a list

Click "Try It Out", enter in 2, for this example, and then click "Execute":

![Get List By ID](/assets/getListById.png)

Swagger will show the lists and its contents:

![Get List By ID Details](/assets/getListByIdDetails.png)


#### POST a new list:

**localhost:yourPortNumber/api/todolist** or

**https://artisanalapi.azurewebsites.net/api/todolist**

Click "Try It Out" and let's begin.
Swagger will display "Id", but we don't need it because the database
will assign it an ID number.  Delete the Id portion.  Type in 
"Dancing Queen" because most of us want to be dancing queens, yes???

![Post a new list](/assets/postList.png)

Navigate back to the directions to get all lists and we can see the
new list is there:

![Show Post List](/assets/postListGetReq.png)


#### PUT/Update a list by ID:

**localhost:yourPortNumber/api/todolist/`{id}`**

**https://artisanalapi.azurewebsites.net/api/todolist/{id}**

where id is the id of a list

Let's update the we created earlier.  Becoming a dancing queen is work, let's aim 
to be an amateur dancer instead, type the following in:

```
{
  "id": 3,
  "name": "amateur dancer",
  "todoItems": null
}
```

![Update a list](/assets/updateList2.png)

Go back to the "GET a list by ID" direction to check the changes:

![Check Update](/assets/updateListDetails.png)


#### DELETE a list by ID:

**localhost:yourPortNumber/api/todolist/`{id}`**

**https://artisanalapi.azurewebsites.net/api/todolist/{id}**

where id is the id of a brand

If we're trying to become an amateur dancer, we have to delete another todo 
list because there is only so much time in the world.  Let's forget about 
todo list #1:

![Delete List By Id](/assets/deleteListById.png)

See if it's still there by following the directions for 
"GET all lists" directions:

![Check Delete Work](/assets/getBrand.png)

SAMYANG is no longer in the list of brands:

![List After Deletion](/assets/deleteListIsDeleted.png)

---
## API Items Endpoint Routes and Walk Through
#### GET all items:

**localhost:yourPortNumber/api/todo** or

**https://artisanalapi.azurewebsites.net/api/todo**

Click "Try It Out":

![Get All Items](/assets/getAllitems.png)

Then click "Execute" in order to see all the items:

![See All Items](/assets/getAllItemsList.png)

#### GET an item by ID:

**localhost:yourPortNumber/api/todo/`{id}`**

**https://artisanalapi.azurewebsites.net/api/todo/{id}**

where id is the id of an item

Click "Try It Out", enter 2 for this example and click "Execute":

![Get Item By Id](/assets/getAnItemById.png)

The information for item #2 will be displayed in a JSON format:

![See Item Info](/assets/getAnItemByIdDetail.png)

#### POST a new item

**localhost:yourPortNumber/api/todo** or

**https://artisanalapi.azurewebsites.net/api/todo**

Click "Try It Out":

![Post An Item](/assets/postAnItem.png)

We'll be using "clean patio" for this example and we'll attach it to list #1.

![Post An Item Detail](/assets/postAnItemDetail.png)

See if it's there by following the "GET all lists" directions:

![See new item](/assets/postSeeItemDetail.png)


#### PUT/Update an item by ID:

**localhost:yourPortNumber/api/todo/`{id}`**

**https://artisanalapi.azurewebsites.net/api/todo/{id}**

where id is the id of an item


Let's update the example we created earlier.  Let's change the
"isComplete" variable to false.  The ID is needed for this
entry.  We can tell which ID it is from the POST request made in the previous
section:

![Update an Item](/assets/updateAnItem.png)

Go back to the "GET all times" direction to check up on the update. 
The response shows that "isComplete" for "clean patio" is false:

![Check Update Works](/assets/updateSeeItem.png)

#### Delete an item by ID:

**localhost:yourPortNumber/api/todo/`{id}`**

**https://artisanalapi.azurewebsites.net/api/todo/{id}**

where id is the id of an item

Click "Try It Out":

![Delete An Item](/assets/deleteAnItem.png)

Let's delete the example from the previous step.  
We know its ID is 6 because of our POST request earlier:

![Delete An Item Example](/assets/deleteAnItemById.png)

Go back to the "GET all items" direction to check the list of items. 
The response shows that "clean patio" is no longer there:

![Check Delete](/assets/deleteAnItemCheck.png)

---
## Acknowledgements
- [jaatay](https://github.com/jaatay), [IndigoShock](https://github.com/IndigoShock)
and I trudged through this together.

- Many thanks to [taylorjoshuaw](https://github.com/taylorjoshuaw) 
for this awesome README layout.