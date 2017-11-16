using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
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
            
                //string constr = ConfigurationManager.ConnectionStrings["Constring"].ConnectionString;
                //using (SqlConnection con = new SqlConnection(constr))
                //{
                //    string query = "INSERT INTO PatientBookingInfo(Name, Age, Dept, Doctor, Date) VALUES(@pName, @pAge, @pDept, @pDoc, @pDate)";
                //    using (SqlCommand cmd = new SqlCommand(query))
                //    {
                //        cmd.Connection = con;
                //        con.Open();
                //        cmd.Parameters.AddWithValue("@Name", pName);
                //        cmd.Parameters.AddWithValue("@pAge", pAge);
                //        cmd.Parameters.AddWithValue("@pDept", pDept);
                //        cmd.Parameters.AddWithValue("@pDoc", pDoc);
                //        cmd.Parameters.AddWithValue("@pDate", pDate);

                //        cmd.ExecuteScalar();
                //        con.Close();
                //    }
                //}
                return View("PatientDetails");
            
        }
    }
}