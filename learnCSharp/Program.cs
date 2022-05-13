// See https://aka.ms/new-console-template for more information

using learnCSharp;
using System.Threading.Tasks;


Console.WriteLine("Enter current user: ");
var curOwner = Console.ReadLine();
var basic = new Todo { descript = "notset", owner = curOwner };
Todo newTask;
List<Todo> todoList = new List<Todo>();
todoList.Add(basic);
bool contLoop = true;
var indexFound = false;
String input;
String secondaryInput;
var listIndex = -1;
String[] parsedInput;
String[] doubleParseIn;
Console.WriteLine("Enter help for command list.");
while (contLoop)
{
    listIndex = -1;
    Console.WriteLine("Please enter a command: ");
    input = Console.ReadLine();
    parsedInput = input.Split(' ', 2);
    try
    {
        switch (parsedInput[0])
        {
            case "exit":
                contLoop = false;
                break;
            case "help":
                Console.WriteLine("new: create a new todo task.(can enter in order: \'new\' Description");
                Console.WriteLine("task: enter task description. (can enter in order: \'task\' OldDescription,NewDescription");
                Console.WriteLine("status: Set isDone (true or false). (can enter in order: \'status\' Description,NewStatus");
                Console.WriteLine("print: print selected task description and status. (can enter as: \'print\' Description");
                Console.WriteLine("remove: remove chosen task. (can enter as: \'remove\' Description");
                Console.WriteLine("list: print all tasks.\nowner: change current user. (can enter as \'owner\' User" +
                    "\nexit: exit loop.");
                break;
            case "list":
                foreach (Todo a in todoList)
                {
                    a.ToString();
                }
                break;
            case "remove":
                if(parsedInput.Length > 1)
                {
                    foreach (Todo a in todoList)
                    {
                        if (a.descript.Equals(parsedInput[1]))
                        {
                            listIndex = todoList.IndexOf(a);
                            indexFound = true; break;
                        }
                    }
                    if (indexFound)
                    {
                        todoList.Remove(todoList[listIndex]);
                    }
                    else
                    {
                        Console.WriteLine("Task with input description not found.");
                    }
                }
                else
                {
                    Console.WriteLine("ENTER CURRENT DESCRIPTION OF TASK");
                    secondaryInput = Console.ReadLine();
                    foreach (Todo a in todoList)
                    {
                        if (a.descript.Equals(secondaryInput))
                        {
                            listIndex = todoList.IndexOf(a);
                            indexFound = true; break;
                        }
                    }
                    if (indexFound)
                    {
                        todoList.Remove(todoList[listIndex]);
                    }
                    else
                    {
                        Console.WriteLine("Task with input description not found.");
                    }
                }
                break;
            case "owner":
                if(parsedInput.Length >= 2)
                {
                    curOwner = parsedInput[1];
                }
                else
                {
                    Console.WriteLine("ENTER TASK OWNER");
                    curOwner = Console.ReadLine();
                }
                break;
            case "new":
                if(parsedInput.Length >= 3)
                {
                    newTask = new Todo { descript = parsedInput[1], owner = curOwner };
                    if(parsedInput[2].Equals("true"))
                    {
                        newTask.isDone = true;
                    }
                    else { newTask.isDone = false; }
                }
                else if(parsedInput.Length >=2)
                {
                    newTask = new Todo { descript = parsedInput[1], owner = curOwner };
                }
                else
                {
                    newTask = new Todo { descript = "unnamed", owner = curOwner };
                }
                todoList.Add(newTask);
                Console.WriteLine("new task at index " + todoList.Count);
                break;
            case "task":
                if(parsedInput.Length > 1)
                {
                    doubleParseIn = parsedInput[1].Split(',');
                    if(doubleParseIn.Length > 1)
                    {
                        foreach (Todo a in todoList)
                        {
                            if (a.descript.Equals(doubleParseIn[0]))
                            {
                                listIndex = todoList.IndexOf(a);
                                indexFound = true; break;
                            }
                        }
                        if (indexFound)
                        {
                            if(doubleParseIn.Length > 1)
                            {
                                todoList[listIndex].descript = doubleParseIn[1];
                            }
                            else
                            {
                                Console.WriteLine("ENTER NEW DESCRIPTION");
                                todoList[listIndex].descript = Console.ReadLine();
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
                    secondaryInput = Console.ReadLine();
                    foreach (Todo a in todoList)
                    {
                        if (a.descript.Equals(secondaryInput))
                        {
                            listIndex = todoList.IndexOf(a);
                            indexFound = true; break;
                        }
                    }
                    if (indexFound)
                    {
                        Console.WriteLine("ENTER NEW DESCRIPTION");
                        todoList[listIndex].descript = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Task with input description not found.");
                    }
                }
                break;
            case "status":
                if (parsedInput.Length > 1)
                {
                    doubleParseIn = parsedInput[1].Split(',');
                    if(doubleParseIn.Length > 1)
                    {
                        foreach (Todo a in todoList)
                        {
                            if (a.descript.Equals(doubleParseIn[0]))
                            {
                                listIndex = todoList.IndexOf(a);
                                indexFound = true; break;
                            }
                        }
                        if (indexFound)
                        {
                            if (doubleParseIn[1].Equals("true"))
                            {
                                todoList[listIndex].isDone = true;
                            }
                            else { todoList[listIndex].isDone = false; }
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
                    secondaryInput = Console.ReadLine();
                    foreach (Todo a in todoList)
                    {
                        if (a.descript.Equals(secondaryInput))
                        {
                            listIndex = todoList.IndexOf(a);
                            indexFound = true; break;
                        }
                    }
                    if (indexFound)
                    {
                        Console.WriteLine("\nENTER STATUS(true or false): ");
                        secondaryInput = Console.ReadLine();
                        if (secondaryInput.Equals("true"))
                        {
                            todoList[listIndex].isDone = true;
                            Console.WriteLine("Status set to true.");
                        }
                        else
                        {
                            Console.WriteLine("Status set to false.");
                            todoList[listIndex].isDone = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Task with input description not found.");
                    }
                }
                break;
            case "print":
                if (parsedInput.Length >= 2)
                {
                    foreach (Todo a in todoList)
                    {
                        if (a.descript.Equals(parsedInput[1]))
                        {
                            listIndex = todoList.IndexOf(a);
                            indexFound = true; break;
                        }
                    }
                    if (indexFound)
                    {
                        todoList[listIndex].ToString();
                    }
                    else
                    {
                        Console.WriteLine("Task with input description not found.");
                    }
                }
                else
                {
                    Console.WriteLine("ENTER CURRENT DESCRIPTION OF TASK");
                    secondaryInput = Console.ReadLine();
                    foreach (Todo a in todoList)
                    {
                        if (a.descript.Equals(secondaryInput))
                        {
                            listIndex = todoList.IndexOf(a);
                            indexFound = true; break;
                        }
                    }
                    if (indexFound)
                    {
                        todoList[listIndex].ToString();
                    }
                    else
                    {
                        Console.WriteLine("Task with input description not found.");
                    }
                }
                break;
            default:
                Console.WriteLine("not a command.");
                break;
        }
    }
    catch (ArgumentOutOfRangeException e)
    {
        Console.WriteLine("Please stay in your boundaries.");
    }
    catch (FormatException e)
    {
        Console.WriteLine("Sorry, but I need an integer (in the correct bounds please).");
    }
    catch (Exception e)
    {
        Console.WriteLine("you broke something. lets see what, " + e.ToString());
    }
}

