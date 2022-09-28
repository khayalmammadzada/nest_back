using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Nest.DAL;
using Nest.Utilities;
using Nest.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Nest.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class SliderController : Controller
    {
        NestContext _context { get; }
        private readonly IWebHostEnvironment _env;


        public SliderController(NestContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View(_context.Sliders);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if(!ModelState.IsValid) return View();

            if(slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "You have to choose at least one image");
                return View();
            }

            if (slider.ImageFile.ImageIsOkay(2))
            {
                ModelState.AddModelError("Photo", "Please choose valid image file");
                return View();
            }

           

            slider.ImageUrl = await slider.ImageFile.FileCreate(_env.WebRootPath,"assets/imgs/slider");
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == 0 || id is null) return NotFound();
            Slider slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id);
            if (slider is null) return NotFound();
            return View(slider);
        }


    }
}

