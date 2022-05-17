using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.TodoRepository
{
    public class InMemoryTodoRepositroy : ITodoRepository
    {
        private int _nextID = 0;
        private Dictionary<int, Todo> _todoDictionary = new Dictionary<int, Todo>();

        public async Task DeleteTodo(int ID)
        {
            var succeeded = _todoDictionary.TryGetValue(ID, out var todo);
            if(succeeded)
            {
                _todoDictionary.Remove(ID);
            }
            else
            {
                Console.WriteLine("Not Found.");
            }
        }

        public async Task<Todo> GetTodo(int ID)
        {
            var succeeded = _todoDictionary.TryGetValue(ID, out var todo);
            if(succeeded) 
            { 
                return todo; 
            }
            else 
            { 
                return null; 
            }
        }

        public async Task<List<Todo>> GetAll()
        {
            return _todoDictionary.Values.ToList();
        }

        public async Task<Todo> NewTodo(Todo todo)
        {
            todo.ID = _nextID++;
            _todoDictionary.Add(todo.ID, todo);
            return todo;
        }

        public async Task UpdateTodo(Todo todo)
        {
            var succeeded = _todoDictionary.TryGetValue(todo.ID, out var oldtodo);
            if(succeeded)
            {
                _todoDictionary[todo.ID] = todo;
            }
            else
            {
                Console.WriteLine("Not Found.");
            }
        }
    }
}
