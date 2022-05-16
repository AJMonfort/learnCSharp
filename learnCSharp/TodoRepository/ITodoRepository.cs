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
        Task DeleteTodo(Todo todo);
        Task<Todo> GetTodo(int ID);
        Task UpdateTodo(Todo todo);
    }
}
