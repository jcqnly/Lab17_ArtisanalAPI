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
    [Route("api/todo")]
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
        }

        /// <summary>
        /// Gets all the items and returns the data as a list
        /// </summary>
        /// <returns>JSON list of the items</returns>
        [HttpGet]
        public ActionResult<List<TodoItem>> GetAll()
        {
            return _context.TodoItems.ToList();
        }

        /// <summary>
        /// Returns the item by the specific id requested
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the requested item</returns>
        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<ActionResult<TodoItem>> GetById(int id)
        {
            //sets the item return from the Db as a var
            var item = await _context.TodoItems.FindAsync(id);
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
        public async Task<IActionResult> Create([FromBody]TodoItem item)
        {
            await _context.TodoItems.AddAsync(item);
            await _context.SaveChangesAsync();
            //returns a 201 for a successful post
            //adds a location header to the response, which specifies the URI
            //of the newly created to-do item
            //the "GetTodo" named route is used to create the URL
            //"GetTodo" is defined in GetById method
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        /// <summary>
        /// Updates the item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns>status code 204 for "no content"</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TodoItem item)
        {   //gets the item, by its id, in order to update the info
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;
            todo.ListId = item.ListId;

            _context.TodoItems.Update(todo);
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Deletes the specified item
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code 204 for "no content"</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var todo = await _context.TodoItems.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }
            //if the id is found, call the remove method on it
            _context.TodoItems.Remove(todo);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
