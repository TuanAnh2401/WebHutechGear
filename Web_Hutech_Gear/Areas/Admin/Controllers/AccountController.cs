using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Web_Hutech_Gear.Models;
using Web_Hutech_Gear.Models.EF;
using Web_Hutech_Gear.Models.Support;

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
        public ActionResult Index(int? page, string searchString, string currentFilter)
        {
            if (searchString != null)
                page = 1;
            else
                searchString = currentFilter;
            IEnumerable<ApplicationUser> items = db.Users.ToList();
            var pageSize = 10;
            if (page == null)
            {
                page = 1;
            }
            var pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            if (!string.IsNullOrEmpty(searchString))
                items = items.Where(p => p.Email.ToLower().Contains(searchString.ToLower()) 
                                    || p.UserName.ToLower().Contains(searchString.ToLower())
                                    || p.FullName.ToLower().Contains(searchString.ToLower())).ToList().ToPagedList(pageIndex, pageSize);
            else
                items = items.ToPagedList(pageIndex, pageSize);
            ViewBag.CurrentFilter = searchString;
            ViewBag.PageSize = pageSize;
            ViewBag.Page = page;
            return View(items);
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
        //POST: /Account/IsLock
        [HttpPost]
        public async Task<ActionResult> IsLock(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                if( user.LockoutEnabled)
                {
                    await UserManager.SetLockoutEnabledAsync(user.Id, false);
                    await UserManager.SetLockoutEndDateAsync(user.Id, DateTimeOffset.MaxValue);
                }
                else
                {
                    await UserManager.SetLockoutEnabledAsync(user.Id, true);
                    await UserManager.SetLockoutEndDateAsync(user.Id, DateTimeOffset.UtcNow);
                    await UserManager.ResetAccessFailedCountAsync(user.Id);
                }
                return Json(new { success = true, IsLock = user.LockoutEnabled });
            }

            return Json(new { success = false });
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
                var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, PhoneNumber = model.PhoneNumber, FullName = model.FullName, Address = model.Address,EmailConfirmed = true, Avatar= "avt.jpg" };

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
    }
}