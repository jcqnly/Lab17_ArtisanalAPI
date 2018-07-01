using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Model;

//this is the controller class for the todo ITEMS
namespace ToDoApi.Controllers
{
    /// <summary>
    /// sets the api route to api/todo
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;
        /// <summary>
        /// sets the connection to the Db
        /// </summary>
        /// <param name="context"></param>
        public TodoController(TodoContext context)
        {
            _context = context;

            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all the items and returns the data as a list
        /// </summary>
        /// <returns>JSON list of the items</returns>
        [HttpGet]
        public IEnumerable<TodoItem> GetAll()
        {
            return _context.TodoItems;
        }

        /// <summary>
        /// Returns the item by the specific id requested
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the requested item</returns>
        [HttpGet("{id}:long", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById([FromRoute]long id)
        {
            //sets the item return from the Db as a var
            var item = _context.TodoItems.Find(id);
            //if that item doesn't exist, a 404 will be called
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        /// <summary>
        /// Value of the todo item is grabbed from the http request
        /// </summary>
        /// <param name="item"></param>
        /// <returns>returns a status code</returns>
        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            //returns a 201 for a successful post
            //adds a location header to the response, which specifies the URI
            //of the newly created to-do item
            //the "GetTodo" named route is used to create the URL
            //"GetTodo" is defined in GetById method
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates the name of the item and if it's complete
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>status code 204 for "no content"</returns>
        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {   //gets the item, by its id, in order to update the info
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deletes the specified item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code 204 for "no content"</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }
            //if the id is found, call the remove method on it
            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
