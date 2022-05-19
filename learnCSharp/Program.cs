// See https://aka.ms/new-console-template for more information

using learnCSharp;
using learnCSharp.Models;
using learnCSharp.Services;
using learnCSharp.TodoRepository;
using learnCSharp.UserRepository;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;


var services = new ServiceCollection();

services.AddSingleton<ITodoRepository, SQLiteTodoRepository>();
services.AddSingleton<IUserRepository, SQLiteUserRepository>();
services.AddSingleton<Commands>();
services.AddSingleton<CurrentUserProvider>();
services.AddSingleton<ApplicationLoop>();

var ServiceProvider = services.BuildServiceProvider();
await ServiceProvider.GetRequiredService<ITodoRepository>().Initialize();
await ServiceProvider.GetRequiredService<IUserRepository>().Initialize();

await ServiceProvider.GetRequiredService<ApplicationLoop>().Run();
