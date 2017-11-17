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

        [HttpPost]
        public ActionResult OTPModule(string mobNo)
        {
            try
            {
                //For generating OTP
                Random r = new Random();
                string OTP = r.Next(1000, 9999).ToString();

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

                //String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;

                //StreamWriter myWriter = null;
                //HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                //objRequest.Method = "POST";
                //objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                //objRequest.ContentType = "application/x-www-form-urlencoded";
                //try
                //{
                //    myWriter = new StreamWriter(objRequest.GetRequestStream());
                //    myWriter.Write(url);
                //}
                //catch (Exception e)
                //{
                //    return View("OTPModule");
                //}
                //finally
                //{
                //    myWriter.Close();
                //}

                //HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                //using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                //{
                //    result = sr.ReadToEnd();
                //    sr.Close();
                //}

                //Store the OTP in session to verify in next page.
                //If you want to verify from DB store the OTP in DB for verification. But it will take space
                Session["OTP"] = OTP;
                return View();
            }
            catch (Exception ex)
            {
                return View("OTPModule");
            }
        }

        public ActionResult VerifyOTP(string otp) {
            if (otp == (String)Session["OTP"])
            {
                return View("PatientDetails");
            }
            else {
                return null;
            }
        }

    }
}