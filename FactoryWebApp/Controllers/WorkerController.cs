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
    public class WorkerController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public WorkerController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [HttpGet]
        
        public async Task<ActionResult> Get(string id)
        {
            var model = await _dataRepository.GetWorkers(id);

            return View(model);
        }
        [HttpGet]       
        public async Task<ActionResult> GetWorkerById(string id)
        {
            var worker = await _dataRepository.GetWorker(id);
            if (worker == null)
                return new NotFoundResult();
            return View("GetWorker", worker);

        }
        [HttpGet]
        
        public async  Task<ActionResult> Insert(string id)
        {
            Factory factory = await _dataRepository.GetFactory(id);

            return View("Insert",new Worker() { Factory = factory.Name});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Insert([Bind(include: "WorkerId,Name,Age,Factory,Salary")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                await _dataRepository.CreateWorker(worker);
            }
            return RedirectToAction("Get/" + worker.Factory);
        }

        [HttpGet]
        public async Task<ActionResult> Update(string id)
        {
            Worker worker = await _dataRepository.GetWorker(id);
            return View("Update", worker);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update([Bind(include: "WorkerId,Name,Age,Factory,Salary")] Worker worker)
        {
            if (ModelState.IsValid)
            {
                var workerFromDb = await _dataRepository.GetWorker(worker.WorkerId);
                if(workerFromDb == null)
                {
                    return new NotFoundResult();
                }
                worker.Id = workerFromDb.Id;
                await _dataRepository.UpdateWorker(worker);
               

            }
            return RedirectToAction("Get/" + worker.Factory);
        }

        public async Task<ActionResult> ConfirmDelete(string id)
        {
            var workerFromDb = await _dataRepository.GetWorker(id);
            return View("ConfirmDelete", workerFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            var worker = await _dataRepository.GetWorker(id);
            if(worker == null)
            {
                return new NotFoundResult();
            }

            var result = await _dataRepository.DeteleWorker(worker.WorkerId);

            if (result)
            {
                TempData["Message"] = "Worker deleted successfully";
            }
            else
            {
                TempData["Message"] = "Error while deleting worker";
            }

            return RedirectToAction("Get/" + worker.Factory);
        }



    }
}