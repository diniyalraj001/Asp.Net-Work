using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LINQ.Controllers
{
    public class EmpCrudController : Controller
    {
         DataClasses1DataContext dc = new DataClasses1DataContext ();
        // GET: EmpCrud
        public ActionResult Index()
        {
            var empDetails = from x in dc.Tbl_EmployeeRegistrations select x;

            return View(empDetails);
        }

        // GET: EmpCrud/Details/5
        public ActionResult Details(int id)
        {
            var getEmpDetails = dc.Tbl_EmployeeRegistrations.Single(x=> x.EmployeeID==id);
            return View(getEmpDetails);
        }

        // GET: EmpCrud/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: EmpCrud/Create
        [HttpPost]
        public ActionResult Create(Tbl_EmployeeRegistration  collection)
        {
            try
            {
                
                // TODO: Add insert logic here
                dc.Tbl_EmployeeRegistrations.InsertOnSubmit(collection);
                dc.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpCrud/Edit/5
        public ActionResult Edit(int id)
        {
            var getEmpDetails = dc.Tbl_EmployeeRegistrations.Single(x => x.EmployeeID == id);

            return View(getEmpDetails);
        }

        // POST: EmpCrud/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Tbl_EmployeeRegistration collection)
        {
            try
            {
                // TODO: Add update logic here
                Tbl_EmployeeRegistration empUpdate = dc.Tbl_EmployeeRegistrations.Single(x=>x.EmployeeID==id);
                empUpdate.Name = collection.Name;
                empUpdate.Position = collection.Position;
                empUpdate.Gender = collection.Gender;
                empUpdate.Salary = collection.Salary;
                dc.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EmpCrud/Delete/5
        public ActionResult Delete(int id)
        {
            var getEmpDetails = dc.Tbl_EmployeeRegistrations.Single(x => x.EmployeeID == id);


            return View(getEmpDetails);
        }

        // POST: EmpCrud/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var empdel = dc.Tbl_EmployeeRegistrations.Single(x => x.EmployeeID == id);
                dc.Tbl_EmployeeRegistrations.DeleteOnSubmit(empdel);
                dc.SubmitChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
