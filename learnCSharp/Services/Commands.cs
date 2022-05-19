using learnCSharp.Models;
using learnCSharp.TodoRepository;
using learnCSharp.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.Services
{
    public class Commands
    {
        private ITodoRepository _todoRepository;
        private IUserRepository _userRepository;
        private CurrentUserProvider _currentUserProvider;

        public Commands(ITodoRepository todoRepository, IUserRepository userRepository, CurrentUserProvider currentUser)
        {
            _todoRepository = todoRepository;
            _userRepository = userRepository;
            _currentUserProvider = currentUser;
        }

        public void ListAllCommands()
        {
            Console.WriteLine("new: create a new todo task.(can enter in order: \'new\' description");
            Console.WriteLine("delete: remove chosen user. (can enter in order: \'delete\' User");
            Console.WriteLine("task: enter task description. (can enter in order: \'task\' idNum,NewDescription");
            Console.WriteLine("status: Set isDone (true or false). (can enter in order: \'status\' idNum,NewStatus");
            Console.WriteLine("print: print selected task description, status, and ID. (can enter as: \'print\' idNum");
            Console.WriteLine("remove: remove chosen task. (can enter as: \'remove\' idNum");
            Console.WriteLine("list: print all tasks.\nuser: change current user. (can enter as \'user\' User" +
                "\nexit: exit loop.");
        }

        
        public async Task PrintAllTodos()
        {
            var allUsers = await _userRepository.GetAllUsers();
            var userLookUp = allUsers.ToDictionary(user => user.ID, user => user.Name);
            foreach (Todo todo in await _todoRepository.GetAll())
            {
                PrintToConsole(todo,userLookUp[todo.UserID]);
            }
            return;
        }


        public async Task DeleteUser(String[] input)
        {
            if(input.Length > 1)
            {
                User user = await _userRepository.GetUserByName(input[1]);
                if (user != null)
                {
                    Console.WriteLine("Enter Password:");
                    if(user.Password == Console.ReadLine())
                    {
                        await _userRepository.DeleteUser(user.ID);
                    }
                    else
                    {
                        Console.WriteLine("wrong password");
                        return;
                    }
                }
            }
            else
            {
                Console.WriteLine("ENTER USER NAME");
                string secondaryInput = Console.ReadLine();
                User user = await _userRepository.GetUserByName(secondaryInput);
                if(user != null)
                {
                    await _userRepository.DeleteUser(user.ID);
                }
            }
            return;
        }

        
        public async Task RemoveTodo(String[] input)
        {
            var listIndex = -1;
            var indexFound = false;
            if (input.Length > 1)
            {
                await _todoRepository.DeleteTodo(int.Parse(input[1]));
            }
            else
            {
                Console.WriteLine("ENTER ID NUMBER OF TASK");
                String secondaryInput = Console.ReadLine();
                await _todoRepository.DeleteTodo(int.Parse(secondaryInput));
            }
            return;
        }


        public async Task ChangeUser(String[] input)
        {
            User user;
            String uName;
            if (input.Length > 1)
            {
                uName = input[1];
                user = await _userRepository.GetUserByName(input[1]);
            }
            else
            {
                Console.WriteLine("ENTER TASK OWNER");
                uName = Console.ReadLine();
                user = await _userRepository.GetUserByName(uName);
            }
            if (user == null)
            {
                user = new User() { Name = uName };
                Console.WriteLine("New User Detected.\nPlease Enter Password for New User.");
                user.Password = Console.ReadLine();
                _currentUserProvider.CurrentUser = await _userRepository.NewUser(user);
            }
            else
            {
                Console.WriteLine("Please Enter Your Password:");
                if(user.Password == Console.ReadLine())
                {
                    Console.WriteLine("accepted");
                    _currentUserProvider.CurrentUser = user;
                }
                else
                {
                    Console.WriteLine("Password Not Accepted");
                    Console.WriteLine("Enter User Name");
                    if(input.Length > 1)
                    {
                        input[1] = Console.ReadLine();
                    }
                    else
                    {
                        input.Append<String>(Console.ReadLine());
                    }
                    await ChangeUser(input);
                }
            }
        }


        public async Task CreateTask(String[] input)
        {
            Todo newTask;
            if (input.Length > 1)
            {
                newTask = new Todo { Description = input[1], UserID = _currentUserProvider.CurrentUser.ID };
            }
            else
            {
                newTask = new Todo { Description = "notset", UserID = _currentUserProvider.CurrentUser.ID };
            }
            await _todoRepository.NewTodo(newTask);
        }


        public async Task ChangeTask(String[] input)
        {
            Todo newTodo = new Todo();
            if (input.Length > 1)
            {
                String[] doubleParseIn = input[1].Split(',');
                if (doubleParseIn.Length > 1)
                {
                    newTodo = await _todoRepository.GetTodo(int.Parse(doubleParseIn[0]));
                    newTodo.Description = doubleParseIn[1];
                }
            }
            else
            {
                Console.WriteLine("ENTER TASK ID NUMBER:");
                String secondaryInput = Console.ReadLine();
                newTodo = await _todoRepository.GetTodo(int.Parse(secondaryInput));
                Console.WriteLine("ENTER NEW DESCRIPTION:");
                newTodo.Description = Console.ReadLine();
            }
            await _todoRepository.UpdateTodo(newTodo);
        }
        

        public async Task UpdateStatus(String[] input)
        {
            Todo newTodo = new Todo();
            if (input.Length > 1)
            {
                String[] doubleParseIn = input[1].Split(',');
                if (doubleParseIn.Length > 1)
                {
                    newTodo = await _todoRepository.GetTodo(int.Parse(doubleParseIn[0]));
                    newTodo.Completed = bool.Parse(doubleParseIn[1]);
                }
            }
            else
            {
                Console.WriteLine("ENTER ID NUMBER OF TASK");
                String secondaryInput = Console.ReadLine();
                newTodo = await _todoRepository.GetTodo(int.Parse(secondaryInput));
                Console.WriteLine("ENTER STATUS(true or false):");
                secondaryInput = Console.ReadLine();
                newTodo.Completed = bool.Parse(secondaryInput);
            }
            await _todoRepository.UpdateTodo(newTodo);
        }


        public async Task PrintSpecific(String[] input)
        {
            var allUsers = await _userRepository.GetAllUsers();
            var userLookUp = allUsers.ToDictionary(user => user.ID, user => user.Name);
            if (input.Length > 1)
            {
                var todo = await _todoRepository.GetTodo(int.Parse(input[1]));
                PrintToConsole(todo, userLookUp[todo.UserID]);
            }
            else
            {
                Console.WriteLine("ENTER ID NUMBER OF TASK");
                String secondaryInput = Console.ReadLine();
                var todo = await _todoRepository.GetTodo(int.Parse(secondaryInput));
                PrintToConsole(todo, userLookUp[todo.UserID]);
            }
        }

        public static void PrintToConsole(Todo todo, String name)
        {
            String getCompletionStatus = todo.Completed ? (name + "'s task ID: " + todo.ID + " " + todo.Description + " has been completed") : (name+
                "'s task ID: " + todo.ID + " " + todo.Description + " has NOT been completed.");
            Console.WriteLine(getCompletionStatus);
        }

    }
}
