using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApi.Model
{
    public class TodoList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TodoItem> TodoItems { get; set; }
    }
}
