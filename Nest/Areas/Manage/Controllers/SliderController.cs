using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Nest.DAL;
using Nest.Models;
using Nest.Utlis.Extensions;

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

            if (slider.ImageFile is null)
            {
                ModelState.AddModelError("ImageFile", "Zəhmət olmasa profil şəklinizi yükləyin");
                return View();
            }
            var sliderImg = slider.ImageFile;
            if (!sliderImg.CheckFileExtension("image/"))
            {
                ModelState.AddModelError("ImageFile", "Yüklədiyiniz fayl şəkil deyil");
                return View();
            }
            if (sliderImg.CheckFileSize(1))
            {
                ModelState.AddModelError("ImageFile", "Yüklədiyiniz şəkil 2mb-dan artıq olmamalıdır");
                return View();
            }
            string newSliderImgName = Guid.NewGuid() + sliderImg.CutFileName();
            sliderImg.SaveFile(Path.Combine("imgs", "vendor", newSliderImgName));
            slider.ImageUrl = newSliderImgName;



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

