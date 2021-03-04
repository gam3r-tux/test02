using DevTest02.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace DevTest02.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return RedirectToAction("Employees", "Home");
        }

        public ActionResult Employees()
        {
            string empPath = Server.MapPath("~/employees");
            DataAccess da = new DataAccess(empPath);
            List<Employee> listEmp = da.GetDataEmployees();

            return Json(listEmp, JsonRequestBehavior.AllowGet);
        }     
    }
}