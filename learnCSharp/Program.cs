// See https://aka.ms/new-console-template for more information

using learnCSharp;
using learnCSharp.TodoRepository;
using System.Threading.Tasks;


Console.WriteLine("Enter current user: ");
var curOwner = Console.ReadLine();
var basic = new Todo { descript = "notset", owner = curOwner };
ITodoRepository todoRepository = new InMemoryTodoRepositroy();
todoRepository.NewTodo(basic);
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
                Commands.PrintAllTodos(todoRepository);
                break;
            case "remove":
                Commands.RemoveTodo(todoRepository, parsedInput);
                break;
            case "owner":
                curOwner = Commands.ChangeUser(parsedInput);
                break;
            case "new":
                await todoRepository.NewTodo(Commands.CreateTask(parsedInput, curOwner));
                break;
            case "task":
                await todoRepository.UpdateTodo(await Commands.ChangeTask(todoRepository, parsedInput));
                break;
            case "status":
                await todoRepository.UpdateTodo(await Commands.UpdateStatus(todoRepository, parsedInput));
                break;
            case "print":
                Commands.PrintSpecific(todoRepository, parsedInput);
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

