using JokesWebApp.Data;
using JokesWebApp.Interface;
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
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }      
            else
              return View();

        }




        [AllowAnonymous]
        [HttpGet]
        [Route("/Category/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? result =await _context.Category.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
                return NotFound();


            return View(result);
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Edit(Models.Category _Category)
        {
            if (_Category==null)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
              
                try
                {
                    _context.Update(_Category);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Category edited successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                
                   
                        throw;
                   
                }
                return RedirectToAction(nameof(Index));

            }

            return NotFound();

        }




        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? result = await _context.Category.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (result == null)
                return NotFound();


            return View(result);

        }



        [Authorize]
        [Route("/Category/Delete/{id}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> Delete(Models.Category _category)
        {

            if (_category == null)
            {
                return NotFound();
            }


                Category? Cat =await _context.Category.Where(x => x.Id == _category.Id).FirstOrDefaultAsync();
                try
                {
                    if (Cat != null)
                    {
                        _context.Remove(Cat);
                        await _context.SaveChangesAsync();
                    TempData["success"] = "Category deleted successfully";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }


                return RedirectToAction(nameof(Index));

            




        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewDetail(Models.Category _Category)
        {
            return Ok();
        }





    }
}
