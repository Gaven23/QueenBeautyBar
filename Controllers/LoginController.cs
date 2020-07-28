using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QueenBeautyBar.Data;
using QueenBeautyBar.Enumerators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace QueenBeautyBar.Controllers
{
    public class LoginController : Controller
    {
        private const string Key = "UserId";
        dbsQueenzBeautyBarContext db = new dbsQueenzBeautyBarContext();

        public IActionResult Index()
        {
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await db.Users.Where(k => k.Loginname == model.Username && k.Password == model.Password).FirstOrDefaultAsync();

                    if (user != null)
                    {
                        //  if you are here it means the user logged in successfully.

                        //  get the role of the user
                        var users = db.Users.Where(h => h.UserId == user.UserId).FirstOrDefault();

                        if (users == null)
                        {
                            return Content("No role has been configured for this user");
                        }

                        //  set sessions here
                        //  you need to set sessions before you redirect, the corresponding dashboard (Marker / Admin) will read from session
                       
                        HttpContext.Session.SetString("roleID", Convert.ToString(users.RoleId));
                        HttpContext.Session.SetString("userID", Convert.ToString(user.UserId));
                        HttpContext.Session.SetString("usersToken", Convert.ToString(user.UsersToken));

                        //  BASED ON THE ROLES ON THE SERVER REDIRECT TO SPECIFIC PAGE FOR EITHER MARKER OF SUPER ADMIN
                        //  MARKER
                        if (users.RoleId == (int)RoleIDs.User)
                        {
                            //  get marker record
                            var User = db.Users.Where(u => u.UserId == users.UserId).FirstOrDefault();

                            if (users == null)
                            {
                                return new ContentResult()
                                {
                                    StatusCode = 404,
                                    ContentType = "application/text",
                                    Content = "Marker record does not exist"
                                };
                            }


                            HttpContext.Session.SetString(Key, Convert.ToString(users.UserId));
                            return RedirectToAction("Index", "Admin");
                        }


                        //  ADMINISTRATOR
                        else if (users.RoleId == (int)RoleIDs.Administrator)
                        {
                            return RedirectToAction("Index", "Admin");
                        }

                        //  SUPERADMIN
                        else if (users.RoleId == (int)RoleIDs.SuperAdmin)
                        {
                            return RedirectToAction("Index", "Admin");
                        }
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return View(model);
        }
        public async Task<IActionResult> SignOut()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}