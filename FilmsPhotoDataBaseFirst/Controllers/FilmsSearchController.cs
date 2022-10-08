using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilmsPhotoDataBaseFirst.Models;

namespace FilmsPhotoDataBaseFirst.Controllers
{
    public class FilmsSearchController : Controller
    {
        private FilmsJKTV21Entities db = new FilmsJKTV21Entities();

        // GET: FilmsSearch


        [ChildActionOnly]
        public ActionResult Index()
        {
            return PartialView();
        }

        /**/[HttpPost]
       public ActionResult FilmSearch(string country)
       {
           var allFilms = db.Film.Where(c => c.Country.Contains(country)).ToList();
           ViewBag.search = country;

           if (allFilms.Count <= 0)
           {
               return HttpNotFound();
           }
           return View(allFilms);
       }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
