using learnCSharp.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.TodoRepository
{
    internal class SQLiteTodoRepository : ITodoRepository
    {
        private SQLiteDatabaseHelper _dbHelper;
        public SQLiteTodoRepository()
        {
            _dbHelper = new SQLiteDatabaseHelper();
        }
        public async Task Initialize()
        {
            if(File.Exists(_dbHelper.DatabaseName))
            {
                return;
            }
            
            await _dbHelper.CreateDatabase();
            
        }

        public async Task DeleteTodo(int ID)
        {
            using var connection = new SqliteConnection(_dbHelper.ConnectionString);
            await connection.ExecuteAsync(@"DELETE FROM Todo WHERE ID = @ID", new {ID});
        }

        public async Task<List<Todo>> GetAll()
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            var todos = await connection.QueryAsync<Todo>(@"SELECT * FROM Todo");
            return todos.ToList();
        }

        public async Task<Todo> GetTodo(int ID)
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            var todo = await connection.QuerySingleAsync<Todo>(@"SELECT * FROM Todo WHERE ID = @ID", new {ID});
            return todo;
        }

        public async Task<Todo> NewTodo(Todo todo)
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            var insertedID = await connection.QuerySingleAsync<int>(
@"INSERT INTO Todo(UserID, Description, Completed)
VALUES (@UserID, @Description, @Completed); 
SELECT last_insert_rowid() as ID;",
                new {UserID = todo.UserID, Description = todo.Description, Completed = todo.Completed});
            todo.ID = insertedID;
            return todo;
        }

        public async Task UpdateTodo(Todo todo)
        {
            var connection = new SqliteConnection(_dbHelper.ConnectionString);
            await connection.ExecuteAsync(
@"UPDATE Todo
SET UserID = @UserID, Description = @Description, Completed = @Completed
WHERE ID = @ID",
                new {UserID = todo.UserID, Description = todo.Description, Completed = todo.Completed, ID = todo.ID});
        }
    }
}
