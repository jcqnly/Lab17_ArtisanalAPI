using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDoApi.Controllers;
using ToDoApi.Model;
using Xunit;

namespace TestToDoApi
{
    public class TestTodoController
    {
        [Fact]
        public void CanCreateItem()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanCreateNewItem")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoItem item = new TodoItem
                {
                    Name = "Crank it up to Level 11",
                    IsComplete = true,
                    ListId = 1
                };

                TodoController tc = new TodoController(context);

                //act
                context.Add(item);
                context.SaveChanges();

                var addedItem = tc.Create(item);

                var results = context.TodoItems.Where(x => x.Name == "Crank it up to Level 11");

                //assert
                Assert.Equal(1, results.Count());
            }
        }

        [Fact]
        public async void CanGetItemById()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanGetItemById")
                .Options;
            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoItem item1 = new TodoItem();
                item1.Id = 1;
                item1.Name = "power wash car";
                TodoItem item2 = new TodoItem();
                item2.Id = 2;
                item2.Name = "mow lawn";
                TodoItem item3 = new TodoItem();
                item3.Id = 3;
                item3.Name = "throw banquet";

                //act
                await context.TodoItems.AddAsync(item1);
                await context.TodoItems.AddAsync(item2);
                await context.TodoItems.AddAsync(item3);
                await context.SaveChangesAsync();

                TodoController tc = new TodoController(context);
                var findItem = tc.GetById(1);

                //assert
                Assert.Equal(item1.Name, findItem.Result.Value.Name);
            }
        }

        [Fact]
        public async void CanUpdateItemById()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanUpdateItemById")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoItem item = new TodoItem();
                item.Id = 1;
                item.Name = "clean car";
                item.IsComplete = false;

                TodoController tc = new TodoController(context);

                //act
                await context.TodoItems.AddAsync(item);
                await context.SaveChangesAsync();

                item.IsComplete = true;

                OkResult updateItem = (OkResult)tc.Update(1, item).Result;

                //assert
                Assert.Equal("200", updateItem.StatusCode.ToString());
            }
        }

        [Fact]
        public async void CanDeleteItemById()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanDeleteItemById")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoItem item1 = new TodoItem();
                item1.Id = 1;
                item1.Name = "power wash car";
                TodoItem item2 = new TodoItem();
                item2.Id = 2;
                item2.Name = "mow lawn";
                TodoItem item3 = new TodoItem();
                item3.Id = 3;
                item3.Name = "throw banquet";

                //act
                await context.TodoItems.AddAsync(item1);
                await context.TodoItems.AddAsync(item2);
                await context.TodoItems.AddAsync(item3);
                await context.SaveChangesAsync();

                var findItem = context.TodoItems.Find(1);

                TodoController tc = new TodoController(context);

                var deletedItem = tc.Delete(findItem.Id);

                //assert
                Assert.Equal(2, context.TodoItems.Count());
            }
        }

        [Fact]
        public async void CanAddItemsToAList()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanDeleteItemById")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoItem item1 = new TodoItem();
                item1.Id = 1;
                item1.Name = "power wash car";
                item1.ListId = 1;
                TodoItem item2 = new TodoItem();
                item2.Id = 2;
                item2.Name = "mow lawn";
                item2.ListId = 2;
                TodoItem item3 = new TodoItem();
                item3.Id = 3;
                item3.Name = "throw banquet";
                item3.ListId = 1;

                //act
                await context.TodoItems.AddAsync(item1);
                await context.TodoItems.AddAsync(item2);
                await context.TodoItems.AddAsync(item3);
                await context.SaveChangesAsync();

                var findItem = context.TodoItems.Find(1);

                TodoController tc = new TodoController(context);

                var totalLists = context.TodoItems.Where(x => x.ListId == 1);

                //assert
                Assert.Equal(2, totalLists.Count());
            }
        }

        [Fact]
        public async void CanRemoveItemsFromList()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanDeleteItemById")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoItem item1 = new TodoItem();
                item1.Id = 1;
                item1.Name = "power wash car";
                item1.ListId = 1;
                TodoItem item2 = new TodoItem();
                item2.Id = 2;
                item2.Name = "mow lawn";
                item2.ListId = 2;
                TodoItem item3 = new TodoItem();
                item3.Id = 3;
                item3.Name = "throw banquet";
                item3.ListId = 1;

                //act
                await context.TodoItems.AddAsync(item1);
                await context.TodoItems.AddAsync(item2);
                await context.TodoItems.AddAsync(item3);
                await context.SaveChangesAsync();


                TodoController tc = new TodoController(context);

                item1.Id = 1;
                item1.Name = "power wash car";
                item1.ListId = 4;

                item2.Id = 2;
                item2.Name = "mow lawn";
                item2.ListId = 4;

                item3.Id = 3;
                item3.Name = "throw banquet";
                item3.ListId = 4;

                await context.SaveChangesAsync();
                var totalLists = context.TodoItems.Where(x => x.ListId == 1);

                //assert
                Assert.Equal(0, totalLists.Count());
            }
        }
    }
}
