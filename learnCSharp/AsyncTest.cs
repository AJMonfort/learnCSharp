using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp
{
    public class AsyncTest
    {
        public async Task wait10Sec()
        {
            Console.WriteLine("Starting Task.");
            await Task.Delay(5000);
            Console.WriteLine("Finished Waiting!");
        }

        public int return5()
        {
            return 5;
        }

        public async Task<int> waitReturn6()
        {
            await Task.Delay(1000);
            return 6;
        }
    }
}
