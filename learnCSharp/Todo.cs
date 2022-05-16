﻿using System;
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

        public void ToString()
        {
            String getCompletionStatus = isDone ? (owner +"'s " + ID + " task " + descript + " has been completed") : (owner + 
                "'s " + ID + " task " + descript + " has NOT been completed.");
            Console.WriteLine(getCompletionStatus);
        }

    }
}
