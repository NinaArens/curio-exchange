﻿using System.Web.Mvc;

namespace CurioExchange.Controllers
{
    public class ChangelogController : Controller
    {
        // GET: Changelog
        public ActionResult Index()
        {
            return View();
        }

        // GET: Changelog/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Changelog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Changelog/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Changelog/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Changelog/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Changelog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Changelog/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
