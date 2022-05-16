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
        public int ID { get; set; }

        public String ToString()
        {
            String getCompletionStatus = isDone ? (owner + "'s task ID: " +ID+" "+descript +" has been completed") : (owner +
                "'s task ID: " + ID + " " + descript + " has NOT been completed.");
            return getCompletionStatus;
        }

    }
}
