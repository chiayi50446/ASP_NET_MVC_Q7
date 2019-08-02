using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TodoMVC.Web.Models.Interface;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using Dapper;

namespace TodoMVC.Web.Models.Repository
{
    public class ToDoRepository : IToDoRepository
    {
        private readonly static string _connStr = ConfigurationManager.ConnectionStrings["ToDoList"].ConnectionString;
        protected SqlConnection _sqlConnection;
        private static int _uniqueId;
        private static int _activeCount;
        public ToDoRepository()
        {
            GetSqlConnection();
        }

        public List<ToDos> GetAll()
        {
            List<ToDos> toDos = new List<ToDos>();
            string sqlStatement = "SELECT * FROM ToDoList";
            toDos = _sqlConnection.Query<ToDos>(sqlStatement).ToList();

            return toDos;
        }

        public ToDos GetById(int Id)
        {
            ToDos toDo = new ToDos();
            string sqlStatement = "SELECT * FROM ToDoList WHERE Id=@Id";
            toDo = _sqlConnection.QueryFirstOrDefault<ToDos>(sqlStatement, new { Id = Id });

            return toDo;
        }

        public List<ToDos> GetByStatus(int Status)
        {
            List<ToDos> toDos = new List<ToDos>();
            string sqlStatement = "SELECT * FROM ToDoList WHERE Status=@Status";
            toDos = _sqlConnection.Query<ToDos>(sqlStatement, new { Status = Status}).ToList();

            return toDos;
        }

        public int Add(ToDos data)
        {
            string sqlStatement = "INSERT INTO ToDoList (Id, ToDoDescription, Status)" +
                                    " values (@Id, @ToDoDescription, @Status)";
            return _sqlConnection.Execute(sqlStatement, data);
        }

        public int Update(ToDos data)
        {
            string sqlStatement = "UPDATE ToDoList SET Status=@Status WHERE Id=@Id";
            return _sqlConnection.Execute(sqlStatement, new { Status = data.Status, Id = data.Id });
        }

        public int Delete(int Id)
        {
            string sqlStatement = "DELETE ToDoList WHERE Id=@Id";
            return _sqlConnection.Execute(sqlStatement, new { Id = Id });
        }

        public int ClearComplete()
        {
            string sqlStatement = "DELETE ToDoList WHERE Status=@Status";
            return _sqlConnection.Execute(sqlStatement, new { Status = 1 });
        }

        public int GetUniqueId()
        {
            _uniqueId++;
            return _uniqueId;
        }

        public int GetActiveCount()
        {
            _activeCount = GetAll().Where(x => x.Status == 0).Count();
            return _activeCount;
        }

        public SqlConnection GetSqlConnection()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection(_connStr);
            }
            List<ToDos> toDos = GetAll();
            if (toDos.Any())
            {
                _uniqueId = toDos.Max(x => x.Id);
            }
            return _sqlConnection;
        }
    }
}