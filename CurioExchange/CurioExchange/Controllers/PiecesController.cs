using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CurioExchange;
using CurioExchange.Entities;

namespace CurioExchange.Controllers
{
    public class PiecesController : Controller
    {
        private CurioExchangeContext db = new CurioExchangeContext();

        // GET: Pieces
        public async Task<ActionResult> Index()
        {
            return View(await db.Pieces.ToListAsync());
        }

        // GET: Pieces/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piece piece = await db.Pieces.FindAsync(id);
            if (piece == null)
            {
                return HttpNotFound();
            }
            return View(piece);
        }

        // GET: Pieces/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pieces/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Rare")] Piece piece)
        {
            if (ModelState.IsValid)
            {
                db.Pieces.Add(piece);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(piece);
        }

        // GET: Pieces/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piece piece = await db.Pieces.FindAsync(id);
            if (piece == null)
            {
                return HttpNotFound();
            }
            return View(piece);
        }

        // POST: Pieces/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Rare")] Piece piece)
        {
            if (ModelState.IsValid)
            {
                db.Entry(piece).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(piece);
        }

        // GET: Pieces/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Piece piece = await db.Pieces.FindAsync(id);
            if (piece == null)
            {
                return HttpNotFound();
            }
            return View(piece);
        }

        // POST: Pieces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Piece piece = await db.Pieces.FindAsync(id);
            db.Pieces.Remove(piece);
            await db.SaveChangesAsync();
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
