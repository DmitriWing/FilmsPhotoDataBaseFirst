using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilmsPhotoDataBaseFirst.Models;
using System.Globalization;
using PagedList;

namespace FilmsPhotoDataBaseFirst.Controllers
{
    public class FilmsController : Controller
    {
        private FilmsJKTV21Entities db = new FilmsJKTV21Entities();

        //---------------GetImage
        public FileContentResult GetImage(int id)
        {
            //запрос в БД таблица Film по переданному id
            Film film = db.Film.FirstOrDefault(f => f.Id == id);
            if (film != null)
            {
                return File(film.Cover, film.CoverType);
            }
            return null;
        }
        //-----------actor partial 
        [ChildActionOnly]
        public ActionResult ActorsInFilm(int id)
        {

            var filmActors = db.FilmActor.Where(f => f.FilmId == id);
            return PartialView(filmActors.ToList());
        }

        // GET: Films
        public ActionResult Index(int? page)
        {
            var allFilms = db.Film.OrderBy(c => c.Country).ToList();
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View(allFilms.ToPagedList(pageNumber, pageSize));
        }



        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Film.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
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
