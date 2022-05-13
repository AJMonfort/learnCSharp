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
            Console.WriteLine("new: create a new todo task.(can enter in order: \'new\' Description");
            Console.WriteLine("task: enter task description. (can enter in order: \'task\' OldDescription,NewDescription");
            Console.WriteLine("status: Set isDone (true or false). (can enter in order: \'status\' Description,NewStatus");
            Console.WriteLine("print: print selected task description and status. (can enter as: \'print\' Description");
            Console.WriteLine("remove: remove chosen task. (can enter as: \'remove\' Description");
            Console.WriteLine("list: print all tasks.\nowner: change current user. (can enter as \'owner\' User" +
                "\nexit: exit loop.");
        }

        
        public static void PrintAllTodos(List<Todo> list)
        {
            foreach (Todo a in list)
            {
                a.ToString();
            }
        }

        
        public static void RemoveTodo(List<Todo> list, String[] input)
        {
            var listIndex = -1;
            var indexFound = false;
            if (input.Length > 1)
            {
                foreach (Todo a in list)
                {
                    if (a.descript.Equals(input[1]))
                    {
                        listIndex = list.IndexOf(a);
                        indexFound = true; break;
                    }
                }
                if (indexFound)
                {
                    list.Remove(list[listIndex]);
                }
                else
                {
                    Console.WriteLine("Task with input description not found.");
                }
            }
            else
            {
                Console.WriteLine("ENTER CURRENT DESCRIPTION OF TASK");
                String secondaryInput = Console.ReadLine();
                foreach (Todo a in list)
                {
                    if (a.descript.Equals(secondaryInput))
                    {
                        listIndex = list.IndexOf(a);
                        indexFound = true; break;
                    }
                }
                if (indexFound)
                {
                    list.Remove(list[listIndex]);
                }
                else
                {
                    Console.WriteLine("Task with input description not found.");
                }
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
                newTask = new Todo { descript = "unnamed", owner = owner };
            }
            return newTask;
        }


        public static List<Todo> ChangeTask(List<Todo> list, String[] input)
        {
            var listIndex = -1;
            var indexFound = false;
            if (input.Length > 1)
            {
                String[] doubleParseIn = input[1].Split(',');
                if (doubleParseIn.Length > 1)
                {
                    foreach (Todo a in list)
                    {
                        if (a.descript.Equals(doubleParseIn[0]))
                        {
                            listIndex = list.IndexOf(a);
                            indexFound = true; break;
                        }
                    }
                    if (indexFound)
                    {
                        if (doubleParseIn.Length > 1)
                        {
                            list[listIndex].descript = doubleParseIn[1];
                        }
                        else
                        {
                            Console.WriteLine("ENTER NEW DESCRIPTION");
                            list[listIndex].descript = Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Task with input description not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("ENTER CURRENT DESCRIPTION OF TASK");
                String secondaryInput = Console.ReadLine();
                foreach (Todo a in list)
                {
                    if (a.descript.Equals(secondaryInput))
                    {
                        listIndex = list.IndexOf(a);
                        indexFound = true; break;
                    }
                }
                if (indexFound)
                {
                    Console.WriteLine("ENTER NEW DESCRIPTION");
                    list[listIndex].descript = Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Task with input description not found.");
                }
            }
            return list;
        }


        public static List<Todo> UpdateStatus(List<Todo> list, String[] input)
        {
            var listIndex = -1;
            var indexFound = false;
            if (input.Length > 1)
            {
                String[] doubleParseIn = input[1].Split(',');
                if (doubleParseIn.Length > 1)
                {
                    foreach (Todo a in list)
                    {
                        if (a.descript.Equals(doubleParseIn[0]))
                        {
                            listIndex = list.IndexOf(a);
                            indexFound = true; break;
                        }
                    }
                    if (indexFound)
                    {
                        if (doubleParseIn[1].Equals("true"))
                        {
                            list[listIndex].isDone = true;
                        }
                        else { list[listIndex].isDone = false; }
                    }
                    else
                    {
                        Console.WriteLine("Task with input description not found.");
                    }
                }
            }
            else
            {
                Console.WriteLine("ENTER CURRENT DESCRIPTION OF TASK");
                String secondaryInput = Console.ReadLine();
                foreach (Todo a in list)
                {
                    if (a.descript.Equals(secondaryInput))
                    {
                        listIndex = list.IndexOf(a);
                        indexFound = true; break;
                    }
                }
                if (indexFound)
                {
                    Console.WriteLine("\nENTER STATUS(true or false): ");
                    secondaryInput = Console.ReadLine();
                    if (secondaryInput.Equals("true"))
                    {
                        list[listIndex].isDone = true;
                        Console.WriteLine("Status set to true.");
                    }
                    else
                    {
                        Console.WriteLine("Status set to false.");
                        list[listIndex].isDone = false;
                    }
                }
                else
                {
                    Console.WriteLine("Task with input description not found.");
                }
            }
            return list;
        }


        public static void PrintSpecific(List<Todo> list, String[] input)
        {
            var listIndex = -1;
            var indexFound = false;
            if (input.Length >= 2)
            {
                foreach (Todo a in list)
                {
                    if (a.descript.Equals(input[1]))
                    {
                        listIndex = list.IndexOf(a);
                        indexFound = true; break;
                    }
                }
                if (indexFound)
                {
                    list[listIndex].ToString();
                }
                else
                {
                    Console.WriteLine("Task with input description not found.");
                }
            }
            else
            {
                Console.WriteLine("ENTER CURRENT DESCRIPTION OF TASK");
                String secondaryInput = Console.ReadLine();
                foreach (Todo a in list)
                {
                    if (a.descript.Equals(secondaryInput))
                    {
                        listIndex = list.IndexOf(a);
                        indexFound = true; break;
                    }
                }
                if (indexFound)
                {
                    list[listIndex].ToString();
                }
                else
                {
                    Console.WriteLine("Task with input description not found.");
                }
            }
        }


    }
}
