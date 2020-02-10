using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Repository;
using FactoryWebApp.Models;

namespace FactoryWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public HomeController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        
        public async Task<ActionResult> Index()
        {
            var model = await _dataRepository.GetCities();            
            return View(model);
        }

        [HttpGet]
        public ActionResult Insert()
        {          

            return View("Insert", new City());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert([Bind(include: "Name,Population")] City city)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.CreateCity(city);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
            City city = await _dataRepository.GetCity(id);
            return View("Update", city);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update([Bind(include: "Name,Population")] City city)
        {
            if (ModelState.IsValid)
            {
                var cityFromDb = await _dataRepository.GetCity(city.Name);
                if (cityFromDb == null)
                {
                    return new NotFoundResult();
                }
                city.Id = cityFromDb.Id;
                await _dataRepository.UpdateCity(city);


            }
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


    }
}
