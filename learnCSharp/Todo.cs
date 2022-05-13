using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp
{
    public class Todo
    {
        public String owner { get; set; }
        public String descript { get; set; }
        public bool isDone { get; set; }


        public void ToString()
        {
            if(isDone)
            {
                Console.WriteLine(owner + "'s task " + descript + " has been completed.");
            }
            else
            {
                Console.WriteLine(owner + "'s task " + descript + " has NOT been completed.");
            }
        }

    }
}
