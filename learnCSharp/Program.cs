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
Console.WriteLine("Enter help for command list.");
while (contLoop)
{
    Console.WriteLine("Please enter a command: ");
    String input = Console.ReadLine();
    String[] parsedInput = input.Split(' ', 2);
    try
    {
        switch (parsedInput[0])
        {
            case "exit":
                contLoop = false;
                break;
            case "help":
                Commands.ListAllCommands();
                break;
            case "list":
                Commands.PrintAllTodos(todoList);
                break;
            case "remove":
                Commands.RemoveTodo(todoList, parsedInput);
                break;
            case "owner":
                curOwner = Commands.ChangeUser(parsedInput);
                break;
            case "new":
                todoList.Add(Commands.CreateTask(parsedInput, curOwner));
                break;
            case "task":
                todoList = Commands.ChangeTask(todoList, parsedInput);
                break;
            case "status":
                todoList = Commands.UpdateStatus(todoList, parsedInput);
                break;
            case "print":
                Commands.PrintSpecific(todoList, parsedInput);
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

