using CurioExchange.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CurioExchange.ViewModels;
using CurioExchange.Models;
using System.Text.RegularExpressions;

namespace CurioExchange.Controllers
{
    [Authorize] 
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
        public async Task<ActionResult> CreateWanted()
        {
            var model = new UserPieceModel
            {
                Pieces = await _pieceAgent.RetrievePieces(),
                User_Id = User.Identity.GetUserId(),
                Amount = 1,
                Added = DateTime.Now
            };
            model.Pieces = model.Pieces.OrderBy(t => t.Name).ToList();
            return View(model);
        }

        public async Task<ActionResult> CreateOwned()
        {
            var model = new UserPieceModel
            {
                Owned = true,
                Pieces = await _pieceAgent.RetrievePieces(),
                User_Id = User.Identity.GetUserId(),
                Amount = 1,
                Added = DateTime.Now
            };
            model.Pieces = model.Pieces.OrderBy(t => t.Name).ToList();
            return View(model);
        }

        // POST: UserPiece/Create
        [HttpPost]
        public async Task<ActionResult> CreateWanted(UserPieceModel model)
        {
            try
            {
                await _pieceAgent.CreaseUserPieces(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOwned(UserPieceModel model)
        {
            try
            {
                await _pieceAgent.CreaseUserPieces(model);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
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
        public async Task<ActionResult> Delete(int id)
        {
            await _pieceAgent.DeleteUserPiece(id);
            return RedirectToAction("Index");
        }

        public ActionResult ImportOwned()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ImportOwned(string import, bool purge)
        {
            try
            {
                if (purge)
                {
                    await _pieceAgent.DeleteOwnedPieces(User.Identity.GetUserId());
                }

                var results = Regex.Matches(import, "^[\\d ]{7} [\\w\\s]{8} ([\\w\\s-']{20}) [\\w\\s-']{12} ([\\w\\s-']{0,28})$", RegexOptions.Multiline);

                if (results.Count > 0)
                {
                    foreach (Match item in results)
                    {
                        var pieceId = await _pieceAgent.GetPieceIdForName(item.Groups[1].Value.TrimEnd(), item.Groups[2].Value.Replace("\r",""));

                        if (pieceId > 0)
                        {
                            await _pieceAgent.CreaseUserPiece(new UserPieceModel
                            {
                                Owned = true,
                                Piece_Id = pieceId,
                                User_Id = User.Identity.GetUserId(),
                                Added = DateTime.Now
                            });
                        }
                        else
                        {
                            if (TempData["ErrorMessage"] == null || TempData["ErrorMessage"].ToString() == "")
                            {
                                TempData["ErrorMessage"] = "The following piece(s) do not yet exist in the database: ";
                            }
                            TempData["ErrorMessage"] += item.Groups[1].Value + " " + item.Groups[2].Value.Replace("\r", "") + ", ";
                        }
                    }
                }

                if (TempData.Values.Count > 0 && (TempData["ErrorMessage"] != null || TempData["ErrorMessage"].ToString() != ""))
                {
                    TempData["ErrorMessage"] = TempData["ErrorMessage"].ToString().Remove(TempData["ErrorMessage"].ToString().Length - 2);
                    TempData["ErrorMessage"] += ".";
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> DeleteOwned(int[] toDeleteOwned)
        {
            if (toDeleteOwned != null && toDeleteOwned.Count() > 0)
            {
                foreach (var item in toDeleteOwned)
                {
                    await _pieceAgent.DeleteUserPiece(item);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> DeleteWanted(int[] toDeleteWanted)
        {
            if (toDeleteWanted != null && toDeleteWanted.Count() > 0)
            {
                foreach (var item in toDeleteWanted)
                {
                    await _pieceAgent.DeleteUserPiece(item);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
