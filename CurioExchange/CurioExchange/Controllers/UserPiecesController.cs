using CurioExchange.Interfaces;
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
    public class UserPiecesController : Controller
    {
        private IPieceAgent _pieceAgent;

        public UserPiecesController(IPieceAgent pieceAgent)
        {
            _pieceAgent = pieceAgent;
        }

        // GET: UserPiece
        public async Task<ActionResult> Index()
        {
            var model = new UserPieceViewModel();
            var userPieces = await _pieceAgent.RetrieveUserPieces(User.Identity.GetUserId());
            model.WantedPieces.AddRange(userPieces.Where(t => t.Owned == false));
            model.OwnedPieces.AddRange(userPieces.Where(t => t.Owned));
            return View(model);
        }

        // GET: UserPiece/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserPiece/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserPiece/Create
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

        // GET: UserPiece/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserPiece/Edit/5
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

        // GET: UserPiece/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserPiece/Delete/5
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
