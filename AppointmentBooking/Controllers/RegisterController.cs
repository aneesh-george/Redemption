using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Collections.Specialized;
using System.Text;
using AppointmentBooking.Models;

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
                string strUrl = "http://api.mVaayoo.com/mvaayooapi/MessageCompose?user=aneeshg777@gmail.com:123456&senderID=TEST SMS&receipientno=9447336152&msgtxt=This is a test from mVaayoo API&state=4";
                WebRequest request = HttpWebRequest.Create(strUrl);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream s = (Stream)response.GetResponseStream();
                StreamReader readStream = new StreamReader(s);
                string dataString = readStream.ReadToEnd();
                response.Close();
                s.Close();
                readStream.Close();

                String result;
                string apiKey = "pcWWq/oTTzU-602XqujwvYmxrilSDaFtwzRfHH04g6";
                string numbers = mobNo; // in a comma seperated list
                string message = "Welcome to Redemption Group of Hospitals! Your OTP for registering with us is: " + OTP;
                string sender = "TXTLCL";

                String url = "https://api.textlocal.in/send/?apikey=" + apiKey + "&numbers=" + numbers + "&message=" + message + "&sender=" + sender;
                //refer to parameters to complete correct url string

                StreamWriter myWriter = null;
                HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url);

                objRequest.Method = "POST";
                objRequest.ContentLength = Encoding.UTF8.GetByteCount(url);
                objRequest.ContentType = "application/x-www-form-urlencoded";
                try
                {
                    myWriter = new StreamWriter(objRequest.GetRequestStream());
                    myWriter.Write(url);
                }
                catch (Exception e)
                {
                    //return e.Message;
                }
                finally
                {
                    myWriter.Close();
                }

                HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
                    sr.Close();
                }

                //Store the OTP in session to verify in next page.
                //If you want to verify from DB store the OTP in DB for verification. But it will take space
                Session["OTP"] = OTP;
                return View();
            }
            catch (Exception ex)
            {
                return View();
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