using learnCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.TodoRepository
{
    public interface ITodoRepository
    {
        Task<Todo> NewTodo(Todo todo);
        Task DeleteTodo(int ID);
        Task<Todo> GetTodo(int ID);
        Task<List<Todo>> GetAll();
        Task UpdateTodo(Todo todo);
        Task Initialize();
    }
}
