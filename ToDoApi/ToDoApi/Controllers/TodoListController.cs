using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoApi.Model;

//this is the controller class for the todo LIST
namespace ToDoApi.Controllers
{
    /// <summary>
    /// sets the api route to api/todolist
    /// </summary>
    [Route("api/todolist")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly TodoContext _context;
        /// <summary>
        /// sets the connection to the Db
        /// </summary>
        /// <param name="context"></param>
        public TodoListController(TodoContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all the lists of todos and returns the list as a list
        /// </summary>
        /// <returns>JSON list of the lists</returns>
        [HttpGet]
        public ActionResult<List<TodoList>> GetAll()
        {
            return _context.TodoLists.ToList();
        }

        /// <summary>
        /// Returns the list by the specific id along with its todo items
        /// </summary>
        /// <param name="id"></param>
        /// <returns>the requested list</returns>
        [HttpGet("{id}", Name = "GetTodoList")]
        public ActionResult<TodoList> GetById([FromRoute]long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //sets the list return from the Db as a var
            TodoList list = _context.TodoLists.Find(id);
            //finds the list of todo items that matches the requested list id
            var todoItem = _context.TodoItems.Where(x => x.ListId == id).ToList();
            list.TodoItems = todoItem;
            //if that item doesn't exist, a 404 will be called
            if (list == null)
            {
                return NotFound();
            }
            return Ok(list);
        }

        /// <summary>
        /// Similar to create, except the http request is a put
        /// </summary>
        /// <param name="list"></param>
        /// <returns>returns a status code</returns>
        [HttpPost]
        public IActionResult Create([FromBody]TodoList list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.TodoLists.Add(list);
            _context.SaveChanges();
            //returns a 201 for a successful post
            //adds a location header to the response, which specifies the URI
            //of the newly created to-do item
            //the "GetTodo" named route is used to create the URL
            //"GetTodo" is defined in GetById method
            return CreatedAtRoute("GetTodoList", new { id = list.Id }, list);
        }

        /// <summary>
        /// Updates the list name and the list of items
        /// </summary>
        /// <param name="id"></param>
        /// <param name="list"></param>
        /// <returns>status code 204 for "no content" or not found</returns>
        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] TodoList list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //gets the item, by its id, in order to update the info
            var todoList = _context.TodoLists.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }

            todoList.Name = list.Name;
            todoList.TodoItems = list.TodoItems;  

            _context.TodoLists.Update(todoList);
            _context.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// deletes the specified list
        /// </summary>
        /// <param name="id"></param>
        /// <returns>status code 204 for "no content"</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ////finds the list of todo items that matches the requested list id
            //var todoItem = _context.TodoItems.Where(x => x.ListId == id).ToList();
            ////remove all the items in that list
            //foreach (var item in todoItem)
            //{
            //    _context.TodoItems.Remove(item);
            //}

            var todoList = _context.TodoLists.Find(id);
            if (todoList == null)
            {
                return NotFound();
            }
            _context.TodoLists.Remove(todoList);

            _context.SaveChanges();
            return NoContent();
        }
    }
}
