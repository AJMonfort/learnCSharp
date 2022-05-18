using learnCSharp.Models;
using learnCSharp.TodoRepository;
using learnCSharp.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp
{
    public static class Commands
    {
        
        public static void ListAllCommands()
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

        
        public async static Task PrintAllTodos(ITodoRepository todoRepository, IUserRepository userRepository)
        {
            var allUsers = await userRepository.GetAllUsers();
            var userLookUp = allUsers.ToDictionary(user => user.ID, user => user.Name);
            foreach (Todo todo in await todoRepository.GetAll())
            {
                PrintToConsole(todo,userLookUp[todo.UserID]);
            }
            return;
        }


        public async static Task DeleteUser(IUserRepository userRepository, String[] input)
        {
            if(input.Length > 1)
            {
                User user = await userRepository.GetUserByName(input[1]);
                if (user != null)
                {
                    Console.WriteLine("Enter Password:");
                    if(user.Password == Console.ReadLine())
                    {
                        await userRepository.DeleteUser(user.ID);
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
                User user = await userRepository.GetUserByName(secondaryInput);
                if(user != null)
                {
                    await userRepository.DeleteUser(user.ID);
                }
            }
            return;
        }

        
        public async static Task RemoveTodo(ITodoRepository todoRepository, String[] input)
        {
            var listIndex = -1;
            var indexFound = false;
            if (input.Length > 1)
            {
                await todoRepository.DeleteTodo(int.Parse(input[1]));
            }
            else
            {
                Console.WriteLine("ENTER ID NUMBER OF TASK");
                String secondaryInput = Console.ReadLine();
                await todoRepository.DeleteTodo(int.Parse(secondaryInput));
            }
            return;
        }


        public async static Task<User> ChangeUser(String[] input, IUserRepository userRepository)
        {
            User user;
            String uName;
            if (input.Length > 1)
            {
                uName = input[1];
                user = await userRepository.GetUserByName(input[1]);
            }
            else
            {
                Console.WriteLine("ENTER TASK OWNER");
                uName = Console.ReadLine();
                user = await userRepository.GetUserByName(uName);
            }
            if (user == null)
            {
                user = new User() { Name = uName };
                Console.WriteLine("New User Detected.\nPlease Enter Password for New User.");
                user.Password = Console.ReadLine();
                return await userRepository.NewUser(user);
            }
            else
            {
                Console.WriteLine("Please Enter Your Password:");
                if(user.Password == Console.ReadLine())
                {
                    Console.WriteLine("accepted");
                    return user;
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
                    return await ChangeUser(input, userRepository);
                }
            }
        }


        public static Todo CreateTask(String[] input, User curUser)
        {
            Todo newTask;
            if (input.Length > 1)
            {
                newTask = new Todo { Description = input[1], UserID = curUser.ID };
            }
            else
            {
                newTask = new Todo { Description = "notset", UserID = curUser.ID };
            }
            return newTask;
        }


        public async static Task<Todo> ChangeTask(ITodoRepository todoRepository, String[] input)
        {
            Todo newTodo = new Todo();
            if (input.Length > 1)
            {
                String[] doubleParseIn = input[1].Split(',');
                if (doubleParseIn.Length > 1)
                {
                    newTodo = await todoRepository.GetTodo(int.Parse(doubleParseIn[0]));
                    newTodo.Description = doubleParseIn[1];
                }
            }
            else
            {
                Console.WriteLine("ENTER TASK ID NUMBER:");
                String secondaryInput = Console.ReadLine();
                newTodo = await todoRepository.GetTodo(int.Parse(secondaryInput));
                Console.WriteLine("ENTER NEW DESCRIPTION:");
                newTodo.Description = Console.ReadLine();
            }
            return newTodo;
        }
        

        public async static Task<Todo> UpdateStatus(ITodoRepository todoRepository, String[] input)
        {
            Todo newTodo = new Todo();
            if (input.Length > 1)
            {
                String[] doubleParseIn = input[1].Split(',');
                if (doubleParseIn.Length > 1)
                {
                    newTodo = await todoRepository.GetTodo(int.Parse(doubleParseIn[0]));
                    newTodo.Completed = bool.Parse(doubleParseIn[1]);
                }
            }
            else
            {
                Console.WriteLine("ENTER ID NUMBER OF TASK");
                String secondaryInput = Console.ReadLine();
                newTodo = await todoRepository.GetTodo(int.Parse(secondaryInput));
                Console.WriteLine("ENTER STATUS(true or false):");
                secondaryInput = Console.ReadLine();
                newTodo.Completed = bool.Parse(secondaryInput);
            }
            return newTodo;
        }


        public static async Task PrintSpecific(ITodoRepository todoRepository, String[] input, IUserRepository userRepository)
        {
            var allUsers = await userRepository.GetAllUsers();
            var userLookUp = allUsers.ToDictionary(user => user.ID, user => user.Name);
            if (input.Length > 1)
            {
                var todo = await todoRepository.GetTodo(int.Parse(input[1]));
                PrintToConsole(todo, userLookUp[todo.UserID]);
            }
            else
            {
                Console.WriteLine("ENTER ID NUMBER OF TASK");
                String secondaryInput = Console.ReadLine();
                var todo = await todoRepository.GetTodo(int.Parse(secondaryInput));
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
