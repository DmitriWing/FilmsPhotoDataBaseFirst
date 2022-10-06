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

        [HttpPost]
        public ActionResult FilmSearch(int? page, string country)
        {
            var allFilms = db.Film.Where(c => c.Country.Contains(country)).OrderBy(c => c.Country).ToList();
            ViewBag.search = country;
            int pageSize = 2;
            int pageNumber = (page ?? 1);
            if (allFilms.Count <= 0)
            {
                return HttpNotFound();
            }
            return View(allFilms.ToPagedList(pageNumber, pageSize));
        }

        // GET: Films searched
       /* public ActionResult SearchedFilms(int? page)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var searched = RedirectToAction("FilmSearch");
            return View(searched.ToPagedList(pageNumber, pageSize));

        }*/


        // GET: Films
        public ActionResult Index()
        {
            return View(db.Film.ToList());
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


        // GET: Films/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Description,Cover,CoverType,Country")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.Film.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Description,Cover,CoverType,Country")] Film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Film.Find(id);
            db.Film.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
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
