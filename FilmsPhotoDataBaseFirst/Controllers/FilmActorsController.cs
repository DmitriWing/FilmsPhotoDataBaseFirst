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
    public class FilmActorsController : Controller
    {
        private FilmsJKTV21Entities db = new FilmsJKTV21Entities();

        // GET: FilmActors
        public ActionResult Index()
        {
            var filmActor = db.FilmActor.Include(f => f.Actor).OrderBy(n => n.Actor.Name).Include(f => f.Film).OrderBy(t => t.Film.Title);
            return View(filmActor.ToList());
        }

        // GET: FilmActors/Create
        public ActionResult Create()
        {
            ViewBag.ActorId = new SelectList(db.Actor, "Id", "Name");
            ViewBag.FilmId = new SelectList(db.Film, "Id", "Title");
            return View();
        }

        // POST: FilmActors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FilmId,ActorId")] FilmActor filmActor)
        {
            if (ModelState.IsValid)
            {
                db.FilmActor.Add(filmActor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ActorId = new SelectList(db.Actor, "Id", "Name", filmActor.ActorId);
            ViewBag.FilmId = new SelectList(db.Film, "Id", "Title", filmActor.FilmId);
            return View(filmActor);
        }

        // GET: FilmActors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmActor filmActor = db.FilmActor.Find(id);
            if (filmActor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActorId = new SelectList(db.Actor, "Id", "Name", filmActor.ActorId);
            ViewBag.FilmId = new SelectList(db.Film, "Id", "Title", filmActor.FilmId);
            return View(filmActor);
        }

        // POST: FilmActors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FilmId,ActorId")] FilmActor filmActor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(filmActor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActorId = new SelectList(db.Actor, "Id", "Name", filmActor.ActorId);
            ViewBag.FilmId = new SelectList(db.Film, "Id", "Title", filmActor.FilmId);
            return View(filmActor);
        }

        // GET: FilmActors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FilmActor filmActor = db.FilmActor.Find(id);
            if (filmActor == null)
            {
                return HttpNotFound();
            }
            return View(filmActor);
        }

        // POST: FilmActors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FilmActor filmActor = db.FilmActor.Find(id);
            db.FilmActor.Remove(filmActor);
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
