using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace learnCSharp.Models
{
    public class Todo
    {
        //User user;
        public int UserID { get; set; }
        public String Description { get; set; }
        public bool Completed { get; set; }
        public int ID { get; set; }

        //public String ToString()
        //{
        //    String getCompletionStatus = Completed ? (owner + "'s task ID: " +ID+" "+Description +" has been completed") : (owner +
        //        "'s task ID: " + ID + " " + Description + " has NOT been completed.");
        //    return getCompletionStatus;
        //}

    }
}
