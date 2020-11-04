using Job_Portal_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Job_Portal_MVC.Controllers
{
    public class EmployerController : Controller
    {
        JobPortalContext db = new JobPortalContext();
        // GET: Employer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Employer emp)
        {
            emp.password = encrypt(emp.password);
            var employer = db.Employerss.Where(a => a.employerId.Equals(emp.employerId) && a.password.Equals(emp.password)).FirstOrDefault();
            if (employer != null)
            {
                FormsAuthentication.SetAuthCookie(emp.employerId, false);
                Session["UserId"] = employer.employerId.ToString();
                Session["Username"] = (employer.firstName).ToString();
                Session["Role"] = "Employer";
                return RedirectToAction("AppliedJob");

            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Credentials");
            }
            return View(emp);
        }
        public string encrypt(string clearText)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "employerId,firstName,lastName,password,ConfirmPassword,mobileNo,Organisation")]Employer emp)
        {
            emp.password = encrypt(emp.password);
            emp.ConfirmPassword = encrypt(emp.ConfirmPassword);
            var check = db.Employerss.Find(emp.employerId);
            if (check == null)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Employerss.Add(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("", "User already Exists");
                return View();
            }
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Index");
        }
        public ActionResult AddJob()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult AddJob([Bind(Include = "jobId,designation,salary,experience,qualification,location,vacancy")]Openings open)
        {
           
                string empid = Session["UserId"].ToString();
                var org = db.Employerss.Where(o => o.employerId.Equals(empid)).FirstOrDefault();
                var check = db.Openings.Find(open.jobId);
                if (check == null)
                {
                    open.employerID = empid;
                    open.company = org.Organisation;
                    db.Openings.Add(open);
                    db.SaveChanges();
                    return RedirectToAction("AddJob");
                }
                return RedirectToAction("AddJob");

           
        }
        public ActionResult AppliedJob()
        {
            
            return View(db.Applications.ToList());
        }
        [Authorize]
        public ActionResult StatusEdit(int? applicationid)
        {
            if (Session["UserId"] != null && Session["Role"].ToString() == "Employer")
            {

                if (applicationid == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Application application = db.Applications.Find(applicationid);
                if (applicationid == null)
                {
                    return HttpNotFound();
                }
                return View(application);
            }
            else
            {
                return RedirectToAction("AppliedJob");

            }
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult StatusEdit([Bind(Include = "applicationId,status")] Application application)
        {
            if (Session["UserId"] != null && Session["Role"].ToString() == "Employer")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(application).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("AppliedJob");
                }
                return View(application);
            }
            else
            {
                return RedirectToAction("Index");
            }


        }
       
    }
}
