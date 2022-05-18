using learnCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.TodoRepository
{
    public class FileTodoRepository : ITodoRepository
    {
        String fileName = "savefile.csv";
        int nextID;

        private async Task<List<Todo>> Read()
        {
            if(!File.Exists(fileName)) {
                nextID = 0;
                return new List<Todo>();
            }
            var lines = await File.ReadAllLinesAsync(fileName);
            nextID = int.Parse(lines[0]);
            return lines.Skip(1).Select(line =>
            {
                var splitLine = line.Split(',');
                return new Todo 
                { 
                    ID = int.Parse(splitLine[0]), 
                    UserID = int.Parse(splitLine[1]),
                    //owner = splitLine[1], 
                    Description = splitLine[2], 
                    Completed = bool.Parse(splitLine[3])  
                };
            }).ToList();
        }

        private async Task Write(List<Todo> todos)
        {
            var lines = todos.Select(line =>
            {
                var temp = $"{line.ID},{line.UserID},{line.Description},{line.Completed}";
                return temp;
            });
            lines = lines.Prepend(nextID.ToString());
            await File.WriteAllLinesAsync(fileName, lines);
        }

        public async Task Initialize()
        {
            
        }

        public async Task DeleteTodo(int ID)
        {
            List<Todo> todos = await Read();
            todos = todos.Where(todo => todo.ID != ID).ToList();
            await Write(todos);
        }

        public async Task<List<Todo>> GetAll()
        {
            return await Read();
        }

        public async Task<Todo> GetTodo(int ID)
        {
            List<Todo> todos = await Read();
            Todo todo = todos.FirstOrDefault(todo => todo.ID == ID);
            return todo;
        }

        public async Task<Todo> NewTodo(Todo todo)
        {
            List<Todo> todos = await Read();
            todo.ID = nextID++;
            todos.Add(todo);
            await Write(todos);
            return todo;
        }

        public async Task UpdateTodo(Todo todo)
        {
            //throw new NotImplementedException();
            List<Todo> todos = await Read();
            var foundTodo = todos.FirstOrDefault(t => t.ID == todo.ID);
            foundTodo.Completed = todo.Completed;
            foundTodo.UserID = todo.UserID;
            foundTodo.Description = todo.Description;
            await Write(todos);
        }
    }
}
