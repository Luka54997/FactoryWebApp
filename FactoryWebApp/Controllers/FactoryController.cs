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
    public class FactoryController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public FactoryController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }
        
        public async Task<ActionResult> Get(string id)
        {
            var model = await _dataRepository.GetFactories(id);


            return View(model);
        }

        [HttpGet]

        public async Task<ActionResult> Insert(string id)
        {
            City city = await _dataRepository.GetCity(id);

            return View("Insert", new Factory() { City = city.Name});
        }


        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
            Factory factory = await _dataRepository.GetFactory(id);
            return View("Update", factory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update([Bind(include: "Name,City,CEO,AreaOfExpertise")] Factory factory)
        {
            if (ModelState.IsValid)
            {
                var factoryFromDb = await _dataRepository.GetFactory(factory.Name);
                if (factoryFromDb == null)
                {
                    return new NotFoundResult();
                }
                factory.Id = factoryFromDb.Id;
                await _dataRepository.UpdateFactory(factory);


            }
            return RedirectToAction("Get/" + factory.City);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert([Bind(include: "Name,City,CEO,AreaOfExpertise")] Factory factory)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.CreateFactory(factory);
            }
            return RedirectToAction("Get/" + factory.City);
        }
    }
}