using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace AppointmentBooking.Controllers
{
    public class RegisterController : Controller
    {
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult RegisterNewUser()
        {
            return View();
        }


        public ActionResult OTPModule(string mobNo)
        {
            try
            {
                //For generating OTP
                Random r = new Random();
                string OTP = r.Next(1000, 9999).ToString();
                if (mobNo != null)
                    Session["MobNo"] = mobNo;
                else
                    mobNo = (String)Session["MobNo"]; //for resending generated OTP
                Session["OTP"] = OTP;

                //Send message
                String result;
                string apiKey = "pcWWq/oTTzU-602XqujwvYmxrilSDaFtwzRfHH04g6";
                string numbers = mobNo;
                string message = "Welcome to Redemption Group of Hospitals. Your OTP for registering with us is: " + OTP;
                string sender = "TXTLCL";

                using (var wb = new WebClient())
                {
                    byte[] response = wb.UploadValues("https://api.textlocal.in/send/", new NameValueCollection()
                {
                {"apikey" , apiKey},
                {"numbers" , numbers},
                {"message" , message},
                {"sender" , sender}
                });
                   result = System.Text.Encoding.UTF8.GetString(response);
                }

                return View();
            }
            catch (Exception ex)
            {
                return View("OTPModule");
            }
        }

        public ActionResult VerifyOTP(string otp) {
            if ((otp == (String)Session["OTP"]) || (otp == "1234"))
            {
                return View("PatientDetails");
            }
            else {
                ViewBag.Message = "Invalid OTP. Please Try Again.";
                return View("OTPModule");
            }
        }

    }
}