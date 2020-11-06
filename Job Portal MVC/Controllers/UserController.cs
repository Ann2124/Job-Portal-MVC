using Job_Portal_MVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity;
using System.Web.Helpers;
using System.Web.Optimization;

namespace Job_Portal_MVC.Controllers
{
    public class UserController : Controller
    {
        JobPortalContext db = new JobPortalContext();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User usr)
        {
            usr.password = encrypt(usr.password);
            var user = db.Users.Where(a => a.email.Equals(usr.email) && a.password.Equals(usr.password)).FirstOrDefault();
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(usr.email, false);
                Session["UserId"] = user.email.ToString();
                Session["Username"] = (user.firstname).ToString();
                Session["Role"] = "user";
                return RedirectToAction("JobList");

            }
            else
            {
                ModelState.AddModelError("", "Invalid Login Credentials");
            }
            return View(usr);
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
        public ActionResult Register([Bind(Include = "email,password,confirmPassword,firstname,lastname,address,contactNumber,qualification,year,experience,yearofExperience,employer,employerDetails")] User usr)
        {
            usr.password = encrypt(usr.password);
            usr.confirmPassword = encrypt(usr.confirmPassword);
            var check = db.Users.Find(usr.email);
            if (check == null)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.Users.Add(usr);
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

        public ActionResult JobList()
        {
            return View(db.Openings.ToList());
        }

        [Authorize]
        [HttpGet]
        public ActionResult Apply(string jobId)
        {
            if (Session["UserId"] != null)
            {
                ViewBag.UserId = Session["UserId"].ToString();
                ViewBag.JobId = jobId;
                return View();
            }
            else
            {
                return RedirectToAction("Register");
            }
        }
        [Authorize]
        [HttpPost]
        public ActionResult Apply([Bind(Include = "email,jobId")] Application application, HttpPostedFileBase resume)
        {
            string pathresume = "";
            string FileExtension = "";
            try
            { if (resume != null)
                {
                    if (resume.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(resume.FileName);
                        FileExtension = fileName.Substring(fileName.LastIndexOf('.') + 1).ToLower();
                        if (FileExtension == "pdf")
                        {
                            fileName = application.email.Substring(0, application.email.Length - 4);
                            fileName = fileName + ".pdf";
                            pathresume = Path.Combine(Server.MapPath("~/Resumes"), fileName);
                            resume.SaveAs(pathresume);
                            ViewBag.Message = "File Uploaded Successfully!!";
                        }
                        else
                        {
                            ViewBag.Status = "Select a PDF file";
                        }
                    }
                }
                else
                    ViewBag.Status = "Upload a File";
            }
            catch
            {
                ModelState.AddModelError("", "File Upload Failed");
            }

            if (resume != null && FileExtension=="pdf")
            {
                var Application = new Application()
                {
                    email = application.email,
                    jobId = application.jobId,
                    status = "pending"

                };
                db.Applications.Add(Application);
                db.SaveChanges();

                //Mail obj = new Mail();
                //obj.ToEmail = application.email;
                //obj.EmailSubject = "Applied for Job Position";
                //obj.EMailBody = "You have successfully applied for the job position";
                //WebMail.SmtpServer = "smtp.gmail.com";
                //WebMail.SmtpPort = 587;
                //WebMail.SmtpUseDefaultCredentials = true;
                //WebMail.EnableSsl = true;
                //WebMail.UserName = "JobPortal";
                //WebMail.Password = "Mail Password";
                //WebMail.From = "Your mailId here";
                //WebMail.Send(to: obj.ToEmail, subject: obj.EmailSubject, body: obj.EMailBody, cc: obj.EmailCC, bcc: obj.EmailBCC, isBodyHtml: true);

                return RedirectToAction("JobList");
            }
            else
                return View();
        }
        [Authorize]
        public ActionResult Edit()
        {
            string username = User.Identity.Name;
            User user = db.Users.FirstOrDefault(u => u.email.Equals(username));
            User model = new User();
            model.firstname = user.firstname;
            model.lastname = user.lastname;
            model.address = user.address;
            model.qualification = user.qualification;
            model.contactNumber = user.contactNumber;
            model.year = user.year;
            model.experience = user.experience;
            model.yearofExperience = user.yearofExperience;
            model.employer = user.employer;
            model.employerDetails = user.employerDetails;
            return View(model);

        }
        [Authorize]
        [HttpPost]
        public ActionResult Edit(User usr)
        {
            if (Session["UserId"] != null && Session["Role"].ToString() == "user")
            {

                string username = User.Identity.Name;
                User user = db.Users.FirstOrDefault(u => u.email.Equals(username));
                user.firstname = usr.firstname;
                user.lastname = usr.lastname;
                user.address = usr.address;
                user.contactNumber = usr.contactNumber;
                user.qualification = usr.qualification;
                user.year = usr.year;
                user.experience = usr.experience;
                user.yearofExperience = usr.yearofExperience;
                user.employer = usr.employer;
                user.employerDetails = usr.employerDetails;
                user.password = user.password;
                user.confirmPassword = user.password;
                Session["Username"] = (user.firstname).ToString();
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return View(usr);
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
        public ActionResult Search(string designation)
        {
            var list = new List<Openings>();
            if (String.IsNullOrEmpty(designation))
            {
                list = db.Openings.ToList();
            }
            else
            {
                list = db.Openings.Where(d => d.designation.Equals(designation)).ToList();
                if(list.Count==0)
                {
                    list = db.Openings.Where(d => d.qualification.Equals(designation)).ToList();
                    if(list.Count==0)
                    {
                        list = db.Openings.Where(d => d.company.Equals(designation)).ToList();
                        if(list.Count==0)
                        {
                            list = db.Openings.Where(d => d.location.Equals(designation)).ToList();
                        }
                    }
                }
            }
            return View("JobList", list);

        }
       
    }

}
    


