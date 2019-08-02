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
    public class ToDoMvcController : Controller
    {
        private IToDoRepository _toDoRepository;
        private static int _status;
        public ToDoMvcController ()
        {
            _toDoRepository = new ToDoRepository();
        }
        // GET: ToDoMvc
        public ActionResult Index(int status = -1)
        {
            _status = status;
            ToDoMvcViewModel item = new ToDoMvcViewModel();
            if (status != -1)
            {
                item.ToDoList = _toDoRepository.GetByStatus(status);
            } else
            {
                item.ToDoList = _toDoRepository.GetAll();
            }
            item.ActiveCount = _toDoRepository.GetActiveCount();
            ViewBag.Status = _status;
            return View(item);
        }
        [HttpPost]
        public ActionResult Add(ToDoMvcViewModel item)
        {
            ToDos data = new ToDos()
            {
                Id = _toDoRepository.GetUniqueId(),
                ToDoDescription = item.Description,
                Status = 0
            };
            _toDoRepository.Add(data);
            return RedirectToAction("Index", new { status = _status});
        }

        public ActionResult Delete(int Id)
        {
            _toDoRepository.Delete(Id);
            return RedirectToAction("Index", new { status = _status });
        }

        public ActionResult Update(int Id)
        {
            ToDos data = _toDoRepository.GetById(Id);
            if (data.Status == 1)
            {
                data.Status = 0;
            } else
            {
                data.Status = 1;
            }
            _toDoRepository.Update(data);
            return RedirectToAction("Index", new { status = _status });
        }

        public ActionResult ClearComplete()
        {
            _toDoRepository.ClearComplete();
            return RedirectToAction("Index", new { status = _status });
        }

        public JsonResult GetActiveCount()
        {
            int data = _toDoRepository.GetActiveCount();
            return Json(data);
        }
    }
}