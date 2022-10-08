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
    public class ActorsController : Controller
    {
        private FilmsJKTV21Entities db = new FilmsJKTV21Entities();

        // GET: Actors
        public ActionResult Index()
        {
            return View(db.Actor.ToList());
        }

        public FileContentResult GetImage(int id)
        {
            //запрос в БД таблица Actor по переданному id
            Actor actor = db.Actor.FirstOrDefault(f => f.Id == id);
            if (actor != null)
            {
                return File(actor.Photo, actor.PhotoType);
            }
            return null;
        }
        //-----------films partial 
        [ChildActionOnly]
        public ActionResult FilmsOfActor(int id)
        {
            var actorFilms = db.FilmActor.Where(f => f.ActorId == id).ToList();
            //ViewBag.FilmId = ActorID;
            return PartialView(actorFilms);
        }

        // GET: Actors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Actor actor = db.Actor.Find(id);
            if (actor == null)
            {
                return HttpNotFound();
            }
            return View(actor);
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
