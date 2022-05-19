using learnCSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.Services
{
    internal class ApplicationLoop
    {
        private Commands _commands;

        public ApplicationLoop(Commands commands)
        {
            _commands = commands;
        }

        public async Task Run()
        {
            Console.WriteLine("Enter current user: ");
            var name = Console.ReadLine();
            String[] startUser = { "user", name };
            await _commands.ChangeUser(startUser);

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
                            _commands.ListAllCommands();
                            break;
                        case "list":
                            await _commands.PrintAllTodos();
                            break;
                        case "remove":
                            await _commands.RemoveTodo(parsedInput);
                            break;
                        case "user":
                            await _commands.ChangeUser(parsedInput);
                            break;
                        case "new":
                            await _commands.CreateTask(parsedInput);
                            break;
                        case "task":
                            await _commands.ChangeTask(parsedInput);
                            break;
                        case "status":
                            await _commands.UpdateStatus(parsedInput);
                            break;
                        case "print":
                            await _commands.PrintSpecific(parsedInput);
                            break;
                        case "delete":
                            await _commands.DeleteUser(parsedInput);
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

        }

    }
}
