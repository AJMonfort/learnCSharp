// See https://aka.ms/new-console-template for more information

using learnCSharp;
using System.Threading.Tasks;

Console.WriteLine("Hello, World!");

var test = new AsyncTest();
Task testTask = test.wait10Sec();

Console.WriteLine("This happened while waiting.");
Console.WriteLine(test.return5());
Console.WriteLine(await test.waitReturn6());

await testTask;

