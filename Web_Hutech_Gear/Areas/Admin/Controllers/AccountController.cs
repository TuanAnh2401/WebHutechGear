using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.Support;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Web.Security;

namespace Web_Hutech_Gear.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AccountController : Controller
    {
        // GET: Admin/Account
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private ApplicationDbContext db = new ApplicationDbContext();
        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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

        // GET: Admin/Account
        public ActionResult Index()
        {
            var ítems = db.Users.ToList();
            return View(ítems);
        }
        //
        // GET: /Account/Update
        [AllowAnonymous]
        public async Task<ActionResult> Update(string id)
        {
             
            var item = db.Users.Find(id);
            UpdateAccountViewModel model = new UpdateAccountViewModel
            {
                // Gán thông tin của đối tượng ApplicationUser vào đối tượng CreateAccountViewModel
                Id = item.Id,
                FullName = item.FullName,
                Address = item.Address,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber,
                UserName = item.UserName,
            };
            var userRoles = await UserManager.GetRolesAsync(id);
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            var allRoles = await roleManager.Roles.ToListAsync();
            var rolesSelectList = new List<SelectListItem>();
            foreach (var role in allRoles)
            {
                var isSelected = userRoles.Contains(role.Name);
                rolesSelectList.Add(new SelectListItem
                {
                    Text = role.Name,
                    Value = role.Id,
                    Selected = isSelected
                });
            }
            ViewBag.Roles = rolesSelectList;
            return View(model);
        }
        //
        // POST: /Account/Update
        //
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(UpdateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userRoles = await UserManager.GetRolesAsync(model.Id);
                await UserManager.RemoveFromRolesAsync(model.Id, userRoles.ToArray());
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                var allRoles = await roleManager.Roles.ToListAsync();
                var find = allRoles.FirstOrDefault(p => p.Id == model.Role);
                UserManager.AddToRole(model.Id, find.Name);

                return RedirectToAction("Index");
            }
            return View(model);
        }
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Create()
        {
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber, FullName = model.FullName, Address = model.Address };

                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.AddToRole(user.Id, model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    Common.SendMail("HutechGear", "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>", user.Email);
                    return RedirectToAction("Index", "Account");
                }
                AddErrors(result);
            }
            // If we got this far, something failed, redisplay form
            ViewBag.Role = new SelectList(db.Roles.ToList(), "Name", "Name");
            return View(model);
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
        public async Task<ActionResult> Delete(string id)
        {
            var item = db.Users.Find(id);
            if (item != null)
            {
                var userRoles = await UserManager.GetRolesAsync(id);
                await UserManager.RemoveFromRolesAsync(id, userRoles.ToArray());
                db.Users.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteAll(string ids)
        {
            if (!string.IsNullOrEmpty(ids))
            {
                var items = ids.Split(',');
                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        var userRoles = await UserManager.GetRolesAsync(item);
                        await UserManager.RemoveFromRolesAsync(item, userRoles.ToArray());
                        var obj = db.Users.Find(item);
                        db.Users.Remove(obj);
                        db.SaveChanges();
                    }
                }
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}