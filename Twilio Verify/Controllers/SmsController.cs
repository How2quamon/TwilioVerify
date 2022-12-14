using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Verify.V2;
using Twilio.Rest.Verify.V2.Service;

namespace Twilio_Verify.Controllers
{
    public class SmsController : Controller
    {
        public IActionResult Login()
        {
            return View("SignIn");
        }
        public IActionResult SMSOTP()
        {
            return View("SmsCheck");
        }

        public void CreateVeri()
        {
            string accountSid = "ACbd49423279a8f607d1b7e235e303629a";
            string authToken = "70143eee6ab73c32a1862eef3d088ce3";

            TwilioClient.Init(accountSid, authToken);

            var service = ServiceResource.Create(friendlyName: "CTQM");
            /*var message = MessageResource.Create(
                body: "HEY it's work!",
                from: new Twilio.Types.PhoneNumber("+13466995349"),
                to: new Twilio.Types.PhoneNumber("+840909161871")
            );*/

            Console.WriteLine(service.Sid);
        }
        public void SendVeri()
        {
            string accountSid = "ACbd49423279a8f607d1b7e235e303629a";
            string authToken = "70143eee6ab73c32a1862eef3d088ce3";

            TwilioClient.Init(accountSid, authToken);
            var verification = VerificationResource.Create(
            to: "+840909161871",
            channel: "sms",
            pathServiceSid: "VA26e179a04ed3bb7c3e14fccab91c873d"
        );

            Console.WriteLine(verification.Status);
        }

        public bool CheckVeri(string code)
        {
            string accountSid = "ACbd49423279a8f607d1b7e235e303629a";
            string authToken = "70143eee6ab73c32a1862eef3d088ce3";

            TwilioClient.Init(accountSid, authToken);
            var verificationCheck = VerificationCheckResource.Create(
            to: "+840909161871",
            code: code,
            pathServiceSid: "VA26e179a04ed3bb7c3e14fccab91c873d"
        );
            Console.WriteLine(verificationCheck.Status);
            if (verificationCheck.Status == "approved")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public IActionResult SendCode(string username, string password)
        {
            if (username == "thejohan39" || password == "123456")
            {
                CreateVeri();
                SendVeri();
                return Redirect("SMSOTP");
            }
            else return BadRequest(password);
        }

        [HttpPost]
        public IActionResult LogingIn(string smscode)
        {
            if (CheckVeri(smscode))
            {
                return RedirectToAction("Index", "Home");
            }
            else return Redirect("SMSOTP");
        }

    }
}
