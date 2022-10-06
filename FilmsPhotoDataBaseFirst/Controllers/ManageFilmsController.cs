﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FilmsPhotoDataBaseFirst.Models;

namespace FilmsPhotoDataBaseFirst.Controllers
{
    public class ManageFilmsController : Controller
    {
        private FilmsJKTV21Entities db = new FilmsJKTV21Entities();

        [Authorize(Roles = "admin")]
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

        // GET: ManageFilms
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var films = db.Film.Include(f => f.FilmActor);
            return View(films.ToList());
        }

        // GET: ManageFilms/Details/5
        [Authorize(Roles = "admin")]
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

        // GET: ManageFilms/Create
        [Authorize(Roles ="admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: ManageFilms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Year,Description,Cover,CoverType,Country")] Film film, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                //если выбрано и передано изображение
                if (Image != null)
                {
                    film.CoverType = Image.ContentType;
                    film.Cover = new byte[Image.ContentLength];
                    Image.InputStream.Read(film.Cover, 0, Image.ContentLength);
                }
                db.Film.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", film.TeamId);
            return View(film);
        }

        // GET: ManageFilms/Edit/5
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

        // POST: ManageFilms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Year,Description,Cover,CoverType,Country")] Film film, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    film.CoverType = Image.ContentType;
                    film.Cover = new byte[Image.ContentLength];
                    Image.InputStream.Read(film.Cover, 0, Image.ContentLength);
                }
              
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            // ViewBag.TeamId = new SelectList(db.Teams, "Id", "Name", player.TeamId);
            return View(film);
        }


        // GET: ManageFilms/Delete/5
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

        // POST: ManageFilms/Delete/5
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
