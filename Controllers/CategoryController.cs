using JokesWebApp.Data;
using JokesWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace JokesWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }



        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return _context.Category != null ?
                        View(await _context.Category.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Category'  is null.");
        }




        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
           
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Models.Category _Category)
        {

            // ServerSide Validation
            if (_Category.Name == _Category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name","Input different name that is not same as Order number");
            
            }

              
             if(ModelState.IsValid)
            {
                _context.Add(_Category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }      
            else
              return View();

        }




        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Delete()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> ViewDetail()
        {
            return Ok();
        }





    }
}
