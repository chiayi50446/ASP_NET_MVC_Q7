using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoMVC.Web.Models;

namespace TodoMVC.Web.ViewModels
{
    public class ToDoMvcViewModel
    {
        public string Description { get; set; }
        public List<ToDos> ToDoList { get; set; }
        public int ActiveCount { get; set; }
    }
}