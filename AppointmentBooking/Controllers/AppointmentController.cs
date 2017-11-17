using System;
using System.Web.Mvc;

namespace AppointmentBooking.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Book(string pName, int pAge, int pDept, int pDoc, DateTime pDate) {
                return View("PatientDetails");
        }
    }
}