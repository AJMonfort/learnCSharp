using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp
{
    public class Todo
    {
        private static int nextID = 0;
        public String owner { get; set; }
        public String descript { get; set; }
        public bool isDone { get; set; }
        public int ID { get; private set; } = nextID++;

        public void ToString()
        {
            String getCompletionStatus = isDone ? (owner +"'s " + ID + " task " + descript + " has been completed") : (owner + 
                "'s " + ID + " task " + descript + " has NOT been completed.");
            Console.WriteLine(getCompletionStatus);
        }

    }
}
