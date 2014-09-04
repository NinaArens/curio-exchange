using CurioExchange.Interfaces;
using CurioExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CurioExchange.ViewModels;

namespace CurioExchange.Controllers
{
    public class TradesController : Controller
    {
        private IPieceAgent _pieceAgent;

        public TradesController(IPieceAgent pieceAgent)
        {
            _pieceAgent = pieceAgent;
        }

        // GET: Trades
        public async Task<ActionResult> Index()
        {
            var model = new UserPieceViewModel();
            var wanted = await _pieceAgent.RetrieveTradesWanted(User.Identity.GetUserId());
            model.WantedPieces.AddRange(wanted);
            var owned = await _pieceAgent.RetrieveTradesOwned(User.Identity.GetUserId());
            model.OwnedPieces.AddRange(owned);
            return View(model);
        }

        // GET: Trades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Trades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Trades/Create
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

        // GET: Trades/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Trades/Edit/5
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

        // GET: Trades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Trades/Delete/5
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
