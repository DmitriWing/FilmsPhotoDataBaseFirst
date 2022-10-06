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

namespace FilmsPhotoDataBaseFirst.Controllers
{
    public class FilmActorsController : Controller
    {
        private FilmsJKTV21Entities db = new FilmsJKTV21Entities();

        // GET: FilmActors
        [ChildActionOnly]
        public PartialViewResult Index(int FilmID)
        {
            var filmActor = db.FilmActor.Include(f => f.Actor).Include(f => f.Film);
            ViewBag.FilmId = FilmID;
            return PartialView(filmActor.ToList());
        }

        // GET: FilmActors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmActor filmActor = db.FilmActor.Find(id);
            filmActor = db.FilmActor.Include(a => a.Actor).FirstOrDefault(f => f.FilmId == id);
            if (filmActor == null)
            {
                return HttpNotFound();
            }
            return View(filmActor);
        }

        [ChildActionOnly]
        public ActionResult ActorsInFilm(int id)
        {
            var filmActors = db.Actor.Include(t => t.FilmActor).Where(p => p.Id == id).ToList();
            ViewBag.FilmActorId = id;   
            return PartialView(filmActors.ToList());
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
