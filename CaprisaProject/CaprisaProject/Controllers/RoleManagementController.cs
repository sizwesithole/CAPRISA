using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaprisaProject.Models;

namespace CaprisaProject.Controllers
{
    
    public class RoleManagementController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public RoleManagementController()
        {
        }

        public RoleManagementController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: role
        public ActionResult Index()
        {
            return View();
        }



        public ActionResult SelectUser()
        {
            List<ApplicationUser> user = new List<ApplicationUser>();
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                user = db.Users.OrderBy(a => a.Email).ToList();

            }

            return View(user);
  
        }

        [HttpGet]
        public ActionResult CurrentRole(string id)
        {
            UserRole currentrole1 = new UserRole();
            UserRole currentrole2 = new UserRole();
            UserRole currentrole3 = new UserRole();
            UserRole currentrole4 = new UserRole();

            List<UserRole> userrole = new List<UserRole>();

            try
            {
                ApplicationUser name = new ApplicationUser();

                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    name = db.Users.Find(id);


                    if (UserManager.IsInRole(name.Id, "Admin"))
                    {
                        currentrole1.Id = name.Id;
                        currentrole1.name = name.Email;
                        currentrole1.Role = "Admin";
                        userrole.Add(currentrole1);
                    }

                    if (UserManager.IsInRole(name.Id, "Superuser"))
                    {
                        currentrole2.Id = name.Id;
                        currentrole2.name = name.Email;
                        currentrole2.Role = "Superuser";
                        userrole.Add(currentrole2);
                    }

                    if (UserManager.IsInRole(name.Id, "User"))
                    {
                        currentrole3.Id = name.Id;
                        currentrole3.name = name.Email;
                        currentrole3.Role = "User";
                        //if (UserManager.IsInRole(name.Id, "User") == true)
                        //{
                        userrole.Add(currentrole3);
                        //}

                    }
                    if (userrole.Count() == 0)
                    {
                        currentrole4.Id = name.Id;
                        currentrole4.name = name.Email;
                        currentrole4.Role = "NO ROLES ASSIGNED";
                        userrole.Add(currentrole4);
                    }



                    ViewBag.Name = name.Email;
                }

            }
            catch (Exception e)
            {
                throw e;
            }



            return View(userrole);
        }


        [HttpGet]
        public ActionResult AddNewRole(string id)
        {
            UserRole userroles = new UserRole();
            try
            {
                ApplicationUser user = new ApplicationUser();


                using (ApplicationDbContext db = new ApplicationDbContext())
                {
                    //user = db.Users.Find("cca628e9-d8c9-4c6f-9195-47aa75db3ffc");
                    user = db.Users.Find(id);
                    userroles.Id = user.Id;
                    userroles.name = user.Email;
                    userroles.Role = "fgh";

                }

                RoleNames role1 = new RoleNames()
                {
                    id = "1",
                    Role = "Admin"
                };

                RoleNames role2 = new RoleNames()
                {
                    id = "2",
                    Role = "Superuser"
                };
                RoleNames role3 = new RoleNames()
                {
                    id = "3",
                    Role = "User"
                };

                List<RoleNames> roles = new List<RoleNames>();
                roles.Add(role1);
                roles.Add(role2);
                roles.Add(role3);


                ViewBag.Role = new SelectList(roles, "Role", "Role");
            }
            catch (Exception e)
            {
                throw e;
            }
            return View(userroles);
        }
        [HttpPost]
        public ActionResult AddNewRole(UserRole par)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (UserManager.IsInRole(par.Id, par.Role))
                    {

                        ModelState.AddModelError("", "Login data error");

                    }
                    else
                    {
                        if (par.Role.Equals("Admin"))
                        {
                            if (UserManager.IsInRole(par.Id, "User"))
                            {
                                UserManager.RemoveFromRole(par.Id, "User");
                            }
                            if (UserManager.IsInRole(par.Id, "Superuser"))
                            {
                                UserManager.RemoveFromRole(par.Id, "Superuser");
                            }

                        }
                        if (par.Role.Equals("User") || (par.Role.Equals("Superuser")))
                        {
                            if (UserManager.IsInRole(par.Id, "Admin"))
                            {
                                UserManager.RemoveFromRole(par.Id, "Admin");
                            }
                        }



                    }


                }

            }
            catch (Exception e)
            {
                throw e;
            }
            UserManager.AddToRole(par.Id, par.Role);
            return RedirectToAction("CurrentRole", "RoleManagement", new { id = par.Id });


        }
        public ActionResult RemoveRoleFromUser(string Id, string email, string role)
        {
            UserRole user = new UserRole();
            user.Id = Id;
            user.name = email;
            user.Role = role;
            return View(user);

        }
        [HttpPost, ActionName("RemoveRoleFromUser")]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveRoleConfirmed(string Id, string email, string role)
        {
            UserManager.RemoveFromRole(Id, role);
            return RedirectToAction("CurrentRole", "RoleManagement", new { id = Id });

        }
    }
}