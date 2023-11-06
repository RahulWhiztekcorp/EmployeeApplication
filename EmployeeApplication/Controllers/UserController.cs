using EmployeeApplication.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeApplication.Controllers
{
    public class UserController : Controller
    {
        EmployeeDBEntities employeeDBEntities;
        public UserController()
        {
            employeeDBEntities = new EmployeeDBEntities();
        }
        //List<Employee_tbl> employees = new List<Employee_tbl>
        //{
        //    new Employee_tbl()
        //    {
        //        EId=1,
        //        FirstName="Rahul",
        //        LastName="Kondi",
        //        Age=22,
        //        MobileNumber=9898989898,
        //        Email="rahul@gmail.com",
        //        Username="rahulkondi",
        //        DepartmentId=2,
        //        Salary=240000,
        //        IsFlag=true
        //    },
        //     new Employee_tbl()
        //    {
        //        EId=2,
        //        FirstName="Venu",
        //        LastName="Konda",
        //        Age=21,
        //        MobileNumber=6305848554,
        //        Email="venu@gmail.com",
        //        Username="venu123",
        //        DepartmentId=1,
        //        Salary=180000,
        //        IsFlag=false
        //    },new Employee_tbl()
        //    {
        //        EId=3,
        //        FirstName="Nithin",
        //        LastName="Rana",
        //        Age=24,
        //        MobileNumber=632525154,
        //        Email="Nithin@gmail.com",
        //        Username="Nithinken",
        //        DepartmentId=7,
        //        Salary=280000,
        //        IsFlag=true
        //    }
        //};
        public ActionResult Index()
        {
            return View(employeeDBEntities.Employees.Include(e => e.Department_tbl).ToList());
        }
        public JsonResult Details(int ID)
        {
            try
            {
                Employee_tbl emp = new Employee_tbl();
                employeeDBEntities.Configuration.ProxyCreationEnabled = false;
                //Employee_tbl data = employeeDBEntities.Employees.Where(e => e.EId == ID).FirstOrDefault();
                var data = employeeDBEntities.Employees.Where(e => e.EId == ID).Select(x => new
                {
                    EId = x.EId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Age = x.Age,
                    MobileNumber = x.MobileNumber,
                    Email = x.Email,
                    Username = x.Username,
                    Department_tbl = employeeDBEntities.Departments.Where(e => e.DId == x.DepartmentId).Select(y => new
                    {
                        DepartmentId = y.DId,
                        DepartmentName = y.DepartmentName,
                    }),
                    Salary = x.Salary,
                });
                //Employee_tbl data = employeeDBEntities.Employees.Where(e => e.EId == ID).Include(path => path.Department_tbl).FirstOrDefault();
                //JsonSerializerSettings jss = new JsonSerializerSettings();
                //jss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //var result = JsonConvert.SerializeObject(data, Formatting.None, jss);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public ActionResult Create()
        {
            ViewBag.Departments = employeeDBEntities.Departments.ToList();
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(Employee_tbl employee)
        {
            if (ModelState.IsValid)
            {
                if (employee == null)
                {
                    return RedirectToAction("Create");
                }
                else
                {
                    employee.IsFlag = true;
                    employeeDBEntities.Employees.Add(employee);
                    employeeDBEntities.SaveChanges();
                    return RedirectToAction("Index", "User");
                }
            }
            else
            {
                return View("Create");
            }
        }
        public ActionResult Delete(int ID)
        {
            var data = employeeDBEntities.Employees.Where(e => e.EId == ID).FirstOrDefault();
            data.IsFlag = false;
            employeeDBEntities.Entry(data).State = EntityState.Modified;
            employeeDBEntities.SaveChanges();
            return RedirectToAction("Index", "User");
        }

        public ActionResult Edit(int Id)
        {
            ViewBag.Departments = employeeDBEntities.Departments.ToList();
            employeeDBEntities.Configuration.ProxyCreationEnabled = false;
            Employee_tbl data = employeeDBEntities.Employees.Where(e => e.EId == Id).FirstOrDefault();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(int Id, Employee_tbl employee_Tbl)
        {

            try
            {
                if (employee_Tbl != null)
                {
                    var data = employeeDBEntities.Employees.Where(e => e.EId == Id).FirstOrDefault();
                    data.IsFlag = true;
                    data.FirstName = employee_Tbl.FirstName;
                    data.LastName = employee_Tbl.LastName;
                    data.Email = employee_Tbl.Email;
                    data.MobileNumber = employee_Tbl.MobileNumber;
                    data.Age = employee_Tbl.Age;
                    data.DepartmentId = employee_Tbl.DepartmentId;
                    data.Salary = employee_Tbl.Salary;
                    data.Username = employee_Tbl.Username;
                    employeeDBEntities.Entry(data).State = EntityState.Modified;
                    employeeDBEntities.SaveChangesAsync();
                    return RedirectToAction("Index", "User");

                }
                return RedirectToAction("Edit", "User", employee_Tbl);
            }
            catch (DbEntityValidationException ex)
            {
                // Loop through the validation errors and write them to the console
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: {0} Error: {1}",
                            validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return RedirectToAction("Index", "User");
        }
        public ActionResult Failure()
        {
            return View();
        }
    }
}