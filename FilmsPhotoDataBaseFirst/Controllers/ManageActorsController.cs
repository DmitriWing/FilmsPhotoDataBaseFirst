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
    public class ManageActorsController : Controller
    {
        private FilmsJKTV21Entities db = new FilmsJKTV21Entities();

        [Authorize(Roles = "admin")]
        //---------------GetImage
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

        // GET: ManageActors
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var actors = db.Actor.Include(a => a.FilmActor);
            return View(actors.ToList());
        }

        // GET: ManageActors/Details/5
        [Authorize(Roles = "admin")]
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

        // GET: ManageActors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageActors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,LastName,Photo,PhotoType")] Actor actor, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    actor.PhotoType = Image.ContentType;
                    actor.Photo = new byte[Image.ContentLength];
                    Image.InputStream.Read(actor.Photo, 0, Image.ContentLength);
                }
                db.Actor.Add(actor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(actor);
        }

        // GET: ManageActors/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: ManageActors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,LastName,Photo,PhotoType")] Actor actor, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    actor.PhotoType = Image.ContentType;
                    actor.Photo = new byte[Image.ContentLength];
                    Image.InputStream.Read(actor.Photo, 0, Image.ContentLength);
                }
                db.Entry(actor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(actor);
        }

        // GET: ManageActors/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: ManageActors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Actor actor = db.Actor.Find(id);
            db.Actor.Remove(actor);
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
