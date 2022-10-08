﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FilmsPhotoDataBaseFirst.Models;


namespace FilmsPhotoDataBaseFirst.Controllers
{
    public class HomeController : Controller
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

        public ActionResult Index()
        {
          //  var allFilms = db.Films.OrderByDescending(v => v.Id).Take(3).ToList();
            var allFilms = db.Film.OrderByDescending(v => v.Id).Take(3).ToList();
            return View(allFilms);
        }

        // render image from byte[] to picture
        /*public async Task<ActionResult> RenderImage(int id)
        {
            Film item = await db.Film.FindAsync(id);
            byte[] photoBack = item.Cover;
            return File(photoBack, "image/png");
        }*/

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Film.FirstOrDefault(t => t.Id == id);
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