using Dapper;
using Microsoft.Data.SqlClient;
using StoredProceduresApp.Models;
using System.Data;

namespace StoredProceduresApp.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IConfiguration _configuration;
        public EmployeeService(IConfiguration configuration)
        {
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DBConnection");
            providerName = "System.Data.SqlClient";
        }
        public string ConnectionString { get; set; }
        public string providerName { get; set; }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }


        public List<employee> GetEmployeeList()
        {
            List<employee> result = new List<employee>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    result = dbConnection.Query<employee>("GetEmployeeList", commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return result;
            }
        }

        public string InsertEmployee(employee model)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var emp = dbConnection.Query<employee>("InsertEmployeeRecord", new { Name = model.Name, Position = model.Position, Salary = model.Salary, Gender = model.Gender }, commandType: CommandType.StoredProcedure).ToList();

                    if (emp != null && emp.FirstOrDefault()?.Response == "Saved Successfully")
                    {
                        result = "Saved Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }

        public string DeleteEmployee(int id)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var emp = dbConnection.Query<employee>("DeleteEmployeeRecord", new { EmployeeID = id }, commandType: CommandType.StoredProcedure).ToList();

                    if (emp != null && emp.FirstOrDefault()?.Response == "Deleted Successfully")
                    {
                        result = "Deleted Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }
        public List<employee> GetEmployee(int id)
        {
            List<employee> result = new List<employee>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    result = dbConnection.Query<employee>("FindEmployeeData", new { ID = id }, commandType: CommandType.StoredProcedure).ToList();
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return result;
            }
        }

        public string UpdateEmployee(employee model)
        {
            string result = "";
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var emp = dbConnection.Query<employee>("UpdateEmployeeRecord", new { Name = model.Name, Position = model.Position, Salary = model.Salary, Gender = model.Gender, EmployeeID = model.EmployeeID }, commandType: CommandType.StoredProcedure).ToList();

                    if (emp != null && emp.FirstOrDefault()?.Response == "Saved Successfully")
                    {
                        result = "Saved Successfully";
                    }
                    dbConnection.Close();
                    return result;
                }
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message;
                return errorMsg;
            }
        }
    }

    public interface IEmployeeService
    {
        public string InsertEmployee(employee model);
        public List<employee> GetEmployeeList();
        public string DeleteEmployee(int id);
        public string UpdateEmployee(employee model);
        public List<employee> GetEmployee(int id);
    }
}
