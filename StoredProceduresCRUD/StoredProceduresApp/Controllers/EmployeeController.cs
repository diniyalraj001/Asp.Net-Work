using Microsoft.AspNetCore.Mvc;
using StoredProceduresApp.Models;
using StoredProceduresApp.Services;


namespace StoredProceduresApp.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeService _employeeServices;
        public EmployeeController(IConfiguration configuration, IEmployeeService employeeServices)
        {
            _configuration = configuration;
            _employeeServices = employeeServices;
        }
        public IActionResult Index()
        {
            return View();
        }

        //Get
        [HttpGet]
        public IActionResult EmployeeList()
        {
            AllModel model = new AllModel();
            model.employeesList = _employeeServices.GetEmployeeList().ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddEmployee(employee formdata)
        {
            AllModel model = new AllModel();
            string result = _employeeServices.InsertEmployee(formdata);
            if (result == "Saved Successfully")
            {
                TempData["SuccessMsg"] = "Saved Successfully";
                return RedirectToAction("EmployeeList");
            }
            else
            {
                TempData["ErrorMsg"] = result;
                return View(model);
            }
        }

        public IActionResult DeleteEmployee(int id)
        {
            string result = _employeeServices.DeleteEmployee(id);
            if (result == "Deleted Successfully")
            {
                TempData["SuccessMsg"] = "Deleted Successfully";
                return RedirectToAction("EmployeeList");
            }
            else
            {
                TempData["ErrorMsg"] = result;
                return View();
            }
        }

        [HttpGet]
        public IActionResult EditEmployee(int id)
        {
            AllModel model = new AllModel();
            if (id == 0)
            {
                return NotFound();
            }
            model.employees = _employeeServices.GetEmployee(id).ToList();
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult EditEmployee(employee formdata)
        {
            AllModel model = new AllModel();
            string result = "";
            if (formdata.EmployeeID > 0)
            {
                result = _employeeServices.UpdateEmployee(formdata);
            }
            else
            {
                result = _employeeServices.InsertEmployee(formdata);
            }
            return RedirectToAction("EmployeeList");
        }
    }
}
