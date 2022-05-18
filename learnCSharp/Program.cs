// See https://aka.ms/new-console-template for more information

using learnCSharp;
using learnCSharp.Models;
using learnCSharp.TodoRepository;
using learnCSharp.UserRepository;
using System.Threading.Tasks;


ITodoRepository todoRepository = new SQLiteTodoRepository();
IUserRepository userRepository = new SQLiteUserRepository();
await todoRepository.Initialize();
await userRepository.Initialize();
Console.WriteLine("Enter current user: ");
var name = Console.ReadLine();
String[] startUser = { "user", name};
User curUser = await Commands.ChangeUser(startUser, userRepository);
//User curUser = await userRepository.GetUserByName(name);
//if (curUser != null)
//{
//    Console.WriteLine("Enter user password: ");
//    curUser.Password = Console.ReadLine();
//}
//else
//{
//    curUser = new User() { Name = name };
//    Console.WriteLine("Enter user password: ");
//    curUser.Password = Console.ReadLine();
//}

//await userRepository.UpdateUser(curUser);

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
                await Commands.PrintAllTodos(todoRepository, userRepository);
                break;
            case "remove":
                await Commands.RemoveTodo(todoRepository, parsedInput);
                break;
            case "user":
                curUser = await Commands.ChangeUser(parsedInput, userRepository);
                break;
            case "new":
                await todoRepository.NewTodo(Commands.CreateTask(parsedInput, curUser));
                break;
            case "task":
                await todoRepository.UpdateTodo(await Commands.ChangeTask(todoRepository, parsedInput));
                break;
            case "status":
                await todoRepository.UpdateTodo(await Commands.UpdateStatus(todoRepository, parsedInput));
                break;
            case "print":
                await Commands.PrintSpecific(todoRepository, parsedInput, userRepository);
                break;
            case "delete":
                await Commands.DeleteUser(userRepository, parsedInput);
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
        Console.WriteLine("Sorry, but the input did not match");
    }
    catch (Exception e)
    {
        Console.WriteLine("you broke something. lets see what, " + e.ToString());
    }
}

