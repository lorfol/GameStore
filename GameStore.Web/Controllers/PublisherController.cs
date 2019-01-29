using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using GameStore.Domain.Core.DomainModels;
using GameStore.Services.Interfaces;
using GameStore.Web.ViewModels;

namespace GameStore.Web.Controllers
{
    public class PublisherController : Controller
    {
        private readonly IPublisherManager _publisherManager;

        public PublisherController(IPublisherManager publisherManager)
        {
            _publisherManager = publisherManager;
        }
        
        public ActionResult PublisherDetails(string companyName)
        {
            var publisher = _publisherManager.Find(x => x.CompanyName == companyName).SingleOrDefault();

            if (publisher != null)
                return View("PublisherDetails", Mapper.Map<Publisher, PublisherViewModel>(publisher));

            ViewBag.ErrorMessage = $"Publisher \"{companyName}\" was deleted.";

            return View("Error");
        }

        [HttpGet]
        public ActionResult CreatePublisher()
        {
            return View("CreatePublisherForm", new CreatePublisherViewModel());
        }

        [HttpPost]
        public ActionResult CreatePublisher(CreatePublisherViewModel model)
        {
            var newPublisher = Mapper.Map<CreatePublisherViewModel, Publisher>(model);
            var result = _publisherManager.CreateNewPublisher(newPublisher);
            var response = new HttpStatusCodeResult(result);

            return RedirectToAction("GetAllGames", "Game");
        }
        
        public ActionResult DeletePublisher(int publisherId)
        {
            _publisherManager.DeletePublisherById(publisherId);

            return RedirectToAction("GetAllGames","Game");
        }
    }
}