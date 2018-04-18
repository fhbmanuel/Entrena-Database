using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Net;
using WebApplication1.Models;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        private Entrena_QL_Database_Entities db = new Entrena_QL_Database_Entities();

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User user)
        {
            if (ModelState.IsValid)
            {
                
                if (user.IsValid(user.UserName, user.Password))
                {
                    string query = "select Role from System_Users where Username = '" + user.UserName + "'";
                    //string studentName = db.System_Users.SqlQuery(query).ToList().FirstOrDefault<string>();

                    string theRole;
                    using (var con = new SqlConnection(@"Data Source=MANUELHDEZ\MSSQLSERVER01;Initial Catalog=Entrena_QL_Database;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                    {
                        var sql = "select Role from System_Users where Username = '" + user.UserName + "'";
                        using (var cmd = new SqlCommand(sql, con))
                        {
                            con.Open();
                            theRole = (string)cmd.ExecuteScalar();
                        }
                    }

                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    Response.Cookies["session"]["ses"] = "1";
                    Response.Cookies["session"]["user"] = user.UserName;
                    Response.Cookies["session"]["role"] = theRole;

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Something is wrong: Username or Password invalid. ^_^");
                }
            }
            return View(user);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies["session"]["ses"] = "0";
            Response.Cookies["session"]["user"] = "";
            Response.Cookies["session"]["role"] = "";
            return RedirectToAction("Login", "User");
        }
    }
}