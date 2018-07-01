using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Model;

//this is the controller class for the todo LIST
namespace ToDoApi.Controllers
{
    /// <summary>
    /// sets the api route to api/todolist
    /// </summary>
    [Route("api/[controller]")]
    public class TodoListController : Controller
    {
        private readonly TodoContext _context;
        /// <summary>
        /// sets the connection to the Db
        /// </summary>
        /// <param name="context"></param>
        public TodoListController(TodoContext context)
        {
            _context = context;

            if (_context.TodoList.Count() == 0)
            {
                _context.TodoList.Add(new TodoList { Name = "List1" });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all the lists of todos and returns the list as a list
        /// </summary>
        /// <returns>JSON list of the lists</returns>
        [HttpGet]
        public ActionResult<List<TodoList>> GetAll()
        {
            return _context.TodoList.ToList();
        }

        /// <summary>
        /// Returns the list by the specific id along with its todo items
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the requested list</returns>
        [HttpGet("{id}", Name = "GetTodoList")]
        public ActionResult<TodoList> GetById(long id)
        {
            //sets the list return from the Db as a var
            var list = _context.TodoList.Find(id);
            //finds the list of todo items that matches the requested list id
            var todoItem = _context.TodoItems.Where(x => x.ListId == id).ToList();
            //if that item doesn't exist, a 404 will be called
            if (list == null)
            {
                return NotFound();
            }
            //set the matching list of todo items to the property of each list
            list.ItemList = todoItem;
            //return that list along with its matching list of todo items
            return list;
        }

        /// <summary>
        /// Similar to create, except the http request is a put
        /// </summary>
        /// <param name="list"></param>
        /// <returns>returns a status code</returns>
        [HttpPost]
        public IActionResult Create(TodoList list)
        {
            _context.TodoList.Add(list);
            _context.SaveChanges();
            //returns a 201 for a successful post
            //adds a location header to the response, which specifies the URI
            //of the newly created to-do item
            //the "GetTodo" named route is used to create the URL
            //"GetTodo" is defined in GetById method
            return CreatedAtRoute("GetTodo", new { id = list.Id }, list);
        }

        /// <summary>
        /// Updates the list name and the list of items
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns>status code 204 for "no content" or not found</returns>
        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoList list)
        {
            //gets the item, by its id, in order to update the info
            var todoList = _context.TodoList.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }

            todoList.Name = list.Name;
            todoList.ItemList = list.ItemList;

            _context.TodoList.Update(todoList);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// deletes the specified list
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code 204 for "no content"</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todoList = _context.TodoList.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }
            _context.TodoList.Remove(todoList);

            //finds the list of todo items that matches the requested list id
            var todoItem = _context.TodoItems.Where(x => x.ListId == id).ToList();
            //remove all the items in that list
            foreach (var item in todoItem)
            {
                _context.TodoItems.Remove(item);
            }

            _context.SaveChanges();
            return NoContent();
        }
    }
}
