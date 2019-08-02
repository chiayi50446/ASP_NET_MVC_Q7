using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoMVC.Web.Models.Interface
{
    interface IToDoRepository
    {
        List<ToDos> GetAll();
        ToDos GetById(int Id);
        List<ToDos> GetByStatus(int Status);
        int Add(ToDos data);
        int Update(ToDos data);
        int Delete(int Id);
        int ClearComplete();
        int GetUniqueId();
        int GetActiveCount();
    }
}
