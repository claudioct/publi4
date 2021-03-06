using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using Publi4.Domain.Entities;
using Publi4.Domain;
using Publi4.Models.AccountViewModels;
using Publi4.Domain.Repositories.Interfaces;
using Publi4.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Publi4.Web.Models.AccountViewModels;

namespace Publi4.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class UsersController : Controller
    {
        private readonly Publi4DbContext _context;
        private readonly UserManager<Publi4User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<UsersController> _logger;
        private readonly IMapper _mapper;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUserRepository _userRepository;
        private readonly RoleManager<Publi4Role> _roleManager;

        public static string PageTitle => "Usu�rios";
        public static string ContentName => "Usu�rio";

        public UsersController(Publi4DbContext context,
                               UserManager<Publi4User> userManager,
                               IEmailSender emailSender,
                               ILoggerFactory loggerFactory,
                               IMapper mapper,
                               ICompanyRepository companyRepository,
                               IUserRepository userRepository,
                               RoleManager<Publi4Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _emailSender = emailSender;
            _logger = loggerFactory.CreateLogger<UsersController>();
            _mapper = mapper;
            _companyRepository = companyRepository;
            _userRepository = userRepository;
            _roleManager = roleManager;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var allUser = await _userRepository.GetAllWithRoles();
            var userList = _mapper.Map<List<UserViewModel>>(allUser);
            return View(userList);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = _mapper.Map<UserForCreationViewModel>(await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id));

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/CreateRedator
        public IActionResult CreateRedator()
        {
            ViewBag.Tipo = "Redator";
            return View("Create");
        }

        // GET: Users/CreateCliente
        public IActionResult CreateCliente()
        {
            var companieEntityList = _companyRepository.GetAll().ToList();
            var companies = _mapper.Map<List<CompanyModel>>(companieEntityList);
            ViewBag.Tipo = "Cliente";
            ViewBag.Companies = companies
                .Select(c => new SelectListItem()
                    { Text = c.Nome, Value = c.IdCompany.ToString() })
                .ToList();

            return View("Create");
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserForCreationViewModel userForCreation)
        {
            if (ModelState.IsValid)
            {
                userForCreation.Id = Guid.NewGuid();
                var applicationUser = _mapper.Map<Publi4User>(userForCreation);
                applicationUser.UserName = userForCreation.Email;
                applicationUser.Company = await _companyRepository.GetById(new Guid(userForCreation.Company));
                var userResult = await _userManager.CreateAsync(applicationUser, userForCreation.Password);
                if (userResult.Succeeded)
                {
                    if (userForCreation.Company == null)
                    {
                        await _userManager.AddToRoleAsync(applicationUser, "Redator");
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(applicationUser, "Cliente");
                    }
                    _logger.LogInformation(3, "User created a new account with password.");
                }
                return RedirectToAction("Index");
            }
            return View(userForCreation);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            var user = _mapper.Map<UserForCreationViewModel>(appUser);

            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, UserForCreationViewModel userForCreation)
        {
            if (id != userForCreation.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Password");

            if (ModelState.IsValid)
            {
                try
                {
                    if (!UserForCreationExists(userForCreation.Id))
                    {
                        return NotFound();
                    }

                    var appUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);

                    appUser.Email = userForCreation.Email;
                    appUser.FirstName = userForCreation.FirstName;
                    appUser.LastName = userForCreation.LastName;
                    await _userManager.UpdateAsync(appUser);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical(0, ex, $"Erro ao atualizar usu�rio. UserId: {userForCreation.Id}");
                    ModelState.AddModelError("ErroUpdate", Publi4.Domain.Resources.ModelBindingValidationMessages.CriticalErrorUpdate);
                    return View(userForCreation);
                }
                return RedirectToAction("Index");
            }
            return View(userForCreation);
        }

        // GET: Users/ChangePassword/5
        public async Task<IActionResult> ChangePassword(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            var modelChangePassword = _mapper.Map<UserForCreationViewModel>(user);
            return View(modelChangePassword);
        }

        // POST: Users/ChangePassword/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangePassword(Guid id, [Bind("CurrentPassword, NewPassword")] UserChangePasswordViewModel userChangePassword)
        //{
        //    if (id != userChangePassword.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            if (!UserForCreationExists(userChangePassword.Id))
        //            {
        //                return NotFound();
        //            }

        //            var appUser = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        //            await  _userManager.ChangePasswordAsync(appUser, userChangePassword.CurrentPassword,
        //                userChangePassword.NewPassword);
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            throw;
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(userChangePassword);
        //}

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userForCreation = _mapper.Map<UserForCreationViewModel>(await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id));

            if (userForCreation == null)
            {
                return NotFound();
            }

            return View(userForCreation);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var userForCreation = await _context.Users.SingleOrDefaultAsync(m => m.Id == id);

            await _userManager.DeleteAsync(userForCreation);
            return RedirectToAction("Index");
        }

        private bool UserForCreationExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
