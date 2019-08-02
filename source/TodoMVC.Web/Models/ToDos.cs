using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TodoMVC.Web.Models
{
    public class ToDos
    {
        public int Id { get; set; }
        public string ToDoDescription { get; set; }
        public int Status { get; set; }
    }
}