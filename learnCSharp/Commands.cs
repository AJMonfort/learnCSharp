using learnCSharp.TodoRepository;
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
            Console.WriteLine("task: enter task description. (can enter in order: \'task\' idNum,NewDescription");
            Console.WriteLine("status: Set isDone (true or false). (can enter in order: \'status\' idNum,NewStatus");
            Console.WriteLine("print: print selected task description, status, and ID. (can enter as: \'print\' idNum");
            Console.WriteLine("remove: remove chosen task. (can enter as: \'remove\' idNum");
            Console.WriteLine("list: print all tasks.\nowner: change current user. (can enter as \'owner\' User" +
                "\nexit: exit loop.");
        }

        
        public async static void PrintAllTodos(ITodoRepository todoRepository)
        {
            foreach (Todo a in await todoRepository.GetAll())
            {
                Console.WriteLine(a.ToString());
            }
        }

        
        public async static void RemoveTodo(ITodoRepository todoRepository, String[] input)
        {
            var listIndex = -1;
            var indexFound = false;
            if (input.Length > 1)
            {
                await todoRepository.DeleteTodo(int.Parse(input[1]));
            }
            else
            {
                Console.WriteLine("ENTER CURRENT DESCRIPTION OF TASK");
                String secondaryInput = Console.ReadLine();
                await todoRepository.DeleteTodo(int.Parse(secondaryInput));
            }
        }


        public static String ChangeUser(String[] input)
        {
            String user;
            if (input.Length > 1)
            {
                user = input[1];
            }
            else
            {
                Console.WriteLine("ENTER TASK OWNER");
                user = Console.ReadLine();
            }
            return user;
        }


        public static Todo CreateTask(String[] input, String owner)
        {
            Todo newTask;
            if (input.Length > 1)
            {
                newTask = new Todo { descript = input[1], owner = owner };
            }
            else
            {
                newTask = new Todo { descript = "notset", owner = owner };
            }
            return newTask;
        }


        public async static Task<Todo> ChangeTask(ITodoRepository todoRepository, String[] input)
        {
            Todo temp = new Todo();
            if (input.Length > 1)
            {
                String[] doubleParseIn = input[1].Split(',');
                if (doubleParseIn.Length > 1)
                {
                    temp = await todoRepository.GetTodo(int.Parse(doubleParseIn[0]));
                    temp.descript = doubleParseIn[1];
                }
            }
            else
            {
                Console.WriteLine("ENTER TASK ID NUMBER:");
                String secondaryInput = Console.ReadLine();
                temp = await todoRepository.GetTodo(int.Parse(secondaryInput));
                Console.WriteLine("ENTER NEW DESCRIPTION:");
                temp.descript = Console.ReadLine();
            }
            return temp;
        }


        public async static Task<Todo> UpdateStatus(ITodoRepository todoRepository, String[] input)
        {
            Todo temp = new Todo();
            if (input.Length > 1)
            {
                String[] doubleParseIn = input[1].Split(',');
                if (doubleParseIn.Length > 1)
                {
                    temp = await todoRepository.GetTodo(int.Parse(doubleParseIn[0]));
                    if(doubleParseIn[1].Equals("true") || doubleParseIn[1].Equals("True") || doubleParseIn[1].Equals("t") || doubleParseIn[1].Equals("T"))
                    {
                        temp.isDone = true;
                    }
                    else
                    {
                        temp.isDone = false;
                    }
                }
            }
            else
            {
                Console.WriteLine("ENTER ID NUMBER OF TASK");
                String secondaryInput = Console.ReadLine();
                temp = await todoRepository.GetTodo(int.Parse(secondaryInput));
                Console.WriteLine("ENTER STATUS(true or false):");
                secondaryInput = Console.ReadLine();
                if (secondaryInput.Equals("true") || secondaryInput.Equals("True") || secondaryInput.Equals("t") || secondaryInput.Equals("T"))
                {
                    temp.isDone = true;
                }
                else
                {
                    temp.isDone = false;
                }
            }
            return temp;
        }


        public static async Task PrintSpecific(ITodoRepository todoRepository, String[] input)
        {
            if (input.Length > 1)
            {
                Console.WriteLine((await todoRepository.GetTodo(int.Parse(input[1]))).ToString());
            }
            else
            {
                Console.WriteLine("ENTER ID NUMBER OF TASK");
                String secondaryInput = Console.ReadLine();
                Console.WriteLine((await todoRepository.GetTodo(int.Parse(secondaryInput))).ToString());
            }
        }


    }
}
