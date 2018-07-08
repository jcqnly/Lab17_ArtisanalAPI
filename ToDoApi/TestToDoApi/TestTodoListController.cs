using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ToDoApi.Controllers;
using ToDoApi.Model;
using Xunit;

namespace TestToDoApi
{
    public class TestTodoListController
    {
        [Fact]
        public async void CanCreateList()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanCreateList")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoList list = new TodoList
                {
                    Id = 1,
                    Name = "Learn to Surf"
                };

                TodoListController tlc = new TodoListController(context);

                //act
                await context.AddAsync(list);
                await context.SaveChangesAsync();

                var addedList = tlc.Create(list);

                var result = context.TodoLists.Where(x => x.Name == "Learn to Surf");

                //assert
                Assert.Equal(1, result.Count());
            }
        }

        [Fact]
        public async void CanGetListById()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanGetListById")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoList list1 = new TodoList();
                list1.Id = 1;
                list1.Name = "Knit an octopus";

                TodoList list2 = new TodoList();
                list2.Id = 2;
                list2.Name = "Catch an octopus";

                TodoList list3 = new TodoList();
                list3.Id = 3;
                list3.Name = "Cook an octopus";

                //act
                await context.TodoLists.AddAsync(list1);
                await context.TodoLists.AddAsync(list2);
                await context.TodoLists.AddAsync(list3);

                await context.SaveChangesAsync();

                TodoListController tlc = new TodoListController(context);
                var findList = tlc.GetById(2);

                //assert
                Assert.Equal(list2.Name, findList.Result.Value.Name);
            }
        }

        [Fact]
        public async void CanUpdateListById()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanUpdateList")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoList list = new TodoList();
                list.Id = 1;
                list.Name = "Go Crabbing";

                TodoListController tlc = new TodoListController(context);

                //act
                await context.TodoLists.AddAsync(list);
                await context.SaveChangesAsync();

                list.Name = "Go duck hunting";

                OkResult updateList = (OkResult)tlc.Update(1, list).Result;

                //assert
                Assert.Equal("200", updateList.StatusCode.ToString());
            }
        }

        [Fact]
        public async void CanDeleteListById()
        {
            DbContextOptions<TodoContext> options =
                new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase("CanDeleteItemById")
                .Options;

            using (TodoContext context = new TodoContext(options))
            {
                //arrange
                TodoList list1 = new TodoList();
                list1.Id = 1;
                list1.Name = "power wash car";
                TodoList list2 = new TodoList();
                list2.Id = 2;
                list2.Name = "mow lawn";
                TodoList list3 = new TodoList();
                list3.Id = 3;
                list3.Name = "throw banquet";

                //act
                await context.TodoLists.AddAsync(list1);
                await context.TodoLists.AddAsync(list2);
                await context.TodoLists.AddAsync(list3);
                await context.SaveChangesAsync();

                var findList = context.TodoLists.Find(1);

                TodoListController tlc = new TodoListController(context);

                var deletedList = tlc.Delete(findList.Id);

                //assert
                Assert.Equal(2, context.TodoLists.Count());
            }
        }
    }
}
