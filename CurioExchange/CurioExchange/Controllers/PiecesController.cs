using CurioExchange.Interfaces;
using CurioExchange.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CurioExchange.Controllers
{
    public class PiecesController : Controller
    {
        private IPieceAgent _pieceAgent;

        public PiecesController(IPieceAgent pieceAgent)
        {
            _pieceAgent = pieceAgent;
        }

        // GET: Pieces
        public async Task<ActionResult> Index()
        {
            var model = new List<PieceModel>();

            var pieces = await _pieceAgent.RetrievePieces();

            model.AddRange(pieces);

            return View(model);
        }

        // GET: Pieces/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pieces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pieces/Create
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

        // GET: Pieces/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pieces/Edit/5
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

        // GET: Pieces/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pieces/Delete/5
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
