using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoMVC.Web.Models;
using TodoMVC.Web.Models.Interface;
using TodoMVC.Web.Models.Repository;
using TodoMVC.Web.ViewModels;

namespace TodoMVC.Web.Controllers
{
    public class ToDoAjaxController : Controller
    {
        // GET: ToDoAjax
        private IToDoRepository _toDoRepository;
        public ToDoAjaxController()
        {
            _toDoRepository = new ToDoRepository();
        }
        // GET: ToDoMvc
        public ActionResult Index()
        {
            ToDoMvcViewModel item = new ToDoMvcViewModel();
            item.ToDoList = _toDoRepository.GetAll();
            return View(item);
        }

        public JsonResult GetList(int status = -1)
        {
            ToDoMvcViewModel item = new ToDoMvcViewModel();
            if (status != -1)
            {
                item.ToDoList = _toDoRepository.GetByStatus(status);
            }
            else
            {
                item.ToDoList = _toDoRepository.GetAll();
            }
            return Json(item);
        }
        [HttpPost]
        public JsonResult Delete(int Id)
        {
            _toDoRepository.Delete(Id);
            return Json("");
        }
        [HttpPost]
        public JsonResult Update(int Id)
        {
            ToDos data = _toDoRepository.GetById(Id);
            if (data.Status == 1)
            {
                data.Status = 0;
            }
            else
            {
                data.Status = 1;
            }
            _toDoRepository.Update(data);
            return Json(data);
        }

        public JsonResult ClearComplete()
        {
            _toDoRepository.ClearComplete();
            return Json("");
        }

        public JsonResult GetActiveCount()
        {
            int data = _toDoRepository.GetActiveCount();
            return Json(data);
        }
        [HttpPost]
        public JsonResult Add(string description)
        {
            ToDos data = new ToDos()
            {
                Id = _toDoRepository.GetUniqueId(),
                ToDoDescription = description,
                Status = 0
            };
            _toDoRepository.Add(data);
            return Json(data);
        }
    }
}