using CurioExchange.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CurioExchange.Models;
using System.Text.RegularExpressions;

namespace CurioExchange.Controllers
{
    [Authorize] 
    public class UserPiecesController : Controller
    {
        private IUserPieceAgent _userPieceAgent;
        private IPieceAgent _pieceAgent;

        public UserPiecesController(IUserPieceAgent userPieceAgent, IPieceAgent pieceAgent)
        {
            _userPieceAgent = userPieceAgent;
            _pieceAgent = pieceAgent;
        }

        // GET: UserPiece
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> WantedPieces()
        {
            var model = new List<UserPieceModel>();
            var userPieces = await _userPieceAgent.RetrieveUserPiecesWanted(User.Identity.GetUserId());
            model.AddRange(userPieces);
            return View(model);
        }

        public async Task<ActionResult> OwnedPieces()
        {
            var model = new List<UserPieceModel>();
            var userPieces = await _userPieceAgent.RetrieveUserPiecesOwned(User.Identity.GetUserId());
            model.AddRange(userPieces);
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
                await _userPieceAgent.CreaseUserPieces(model);
                return RedirectToAction("WantedPieces");
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
                await _userPieceAgent.CreaseUserPieces(model);
                return RedirectToAction("OwnedPieces");
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
        public async Task<ActionResult> Delete(int id, bool owned)
        {
            await _userPieceAgent.DeleteUserPiece(id);
            if (owned)
                return RedirectToAction("OwnedPieces");
            else
                return RedirectToAction("WantedPieces");
        }

        public async Task<ActionResult> Refresh(int id, bool owned)
        {
            await _userPieceAgent.RefreshUserPiece(id);
            if (owned)
                return RedirectToAction("OwnedPieces");
            else
                return RedirectToAction("WantedPieces");
        }

        public ActionResult ImportOwned()
        {
            return View();
        }

        public ActionResult ImportWanted()
        {
            return View();
        }

        public ActionResult ImportOwnedPartials()
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
                    await _userPieceAgent.DeleteUserPieces(User.Identity.GetUserId(), true);
                }

                var results = Regex.Matches(import, "^([\\d ]{7}) [\\w\\s]{8} ([\\w\\s-']{20}) [\\w\\s-']{12} ([\\w\\s-']{0,28})$", RegexOptions.Multiline);

                if (results.Count > 0)
                {
                    foreach (Match item in results)
                    {
                        var pieceId = await _pieceAgent.GetPieceIdForName(item.Groups[2].Value.TrimEnd(), item.Groups[3].Value.Replace("\r",""));

                        if (pieceId > 0)
                        {
                            await _userPieceAgent.CreaseUserPiece(new UserPieceModel
                            {
                                Owned = true,
                                Piece_Id = pieceId,
                                User_Id = User.Identity.GetUserId(),
                                Added = DateTime.Now,
                                OwnedID = Convert.ToInt32(item.Groups[1].Value.TrimEnd())
                            });
                        }
                        else
                        {
                            if (TempData["ErrorMessage"] == null || TempData["ErrorMessage"].ToString() == "")
                            {
                                TempData["ErrorMessage"] = "The following piece(s) do not yet exist in the database: ";
                            }
                            TempData["ErrorMessage"] += item.Groups[2].Value + " " + item.Groups[3].Value.Replace("\r", "") + ", ";
                        }
                    }
                }

                if (TempData.Values.Count > 0 && (TempData["ErrorMessage"] != null || TempData["ErrorMessage"].ToString() != ""))
                {
                    TempData["ErrorMessage"] = TempData["ErrorMessage"].ToString().Remove(TempData["ErrorMessage"].ToString().Length - 2);
                    TempData["ErrorMessage"] += ".";
                }

                return RedirectToAction("OwnedPieces");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ImportWanted(string import, bool purge)
        {
            try
            {
                if (purge)
                {
                    await _userPieceAgent.DeleteUserPieces(User.Identity.GetUserId(), false);
                }

                if (import.Contains("--")) 
                {
                    if (import.Contains("*[ PARTIAL")) 
                    { 
                        var splitlist = import.Split(new string[] { "--"} , StringSplitOptions.RemoveEmptyEntries);

                        if (splitlist.Count() > 0)
                        {
                            foreach (var item in splitlist)
                            {
                                //var set = Regex.Matches(item, @"\*+\[ PARTIAL ([\w\s-']+) \]\*+");

                                var set = Regex.Matches(item, @"Curio Set: (.*)\n");

                                var pieces = Regex.Matches(item, @"([\w\s-':]{29})(?:Complete|Missing)", RegexOptions.Multiline);

                                if (set.Count > 0 & pieces.Count > 1)
                                {
                                    foreach (Match piece in pieces)
                                    {
                                        if (piece.Groups[0].Value.EndsWith("Missing")) 
                                        {
                                            var missingpiece = piece.Groups[1].Value;
                                            missingpiece = missingpiece.Trim().Replace(":", "");

                                            var pieceId = await _pieceAgent.GetPieceIdForName(set[0].Groups[1].Value.Replace("\r",""), missingpiece);

                                            if (pieceId > 0)
                                            {
                                                await _userPieceAgent.CreaseUserPiece(new UserPieceModel
                                                {
                                                    Owned = false,
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
                                                TempData["ErrorMessage"] += set[0].Groups[1].Value + " " + missingpiece + ", ";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    } 
                    else
                    {
                        TempData["ErrorMessage"] = "Please include the starred header of CURIO SHOW PIECE <id>>.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Please include the trailing dashed line of CURIO SHOW PIECE <id>>.";
                }

                if (TempData.Values.Count > 0 && (TempData["ErrorMessage"] != null || TempData["ErrorMessage"].ToString() != ""))
                {
                    TempData["ErrorMessage"] = TempData["ErrorMessage"].ToString().Remove(TempData["ErrorMessage"].ToString().Length - 2);
                    TempData["ErrorMessage"] += ".";
                }

                return RedirectToAction("WantedPieces");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ImportOwnedPartials(string import, bool purge)
        {
            try
            {
                if (purge)
                {
                    await _userPieceAgent.DeleteUserPieces(User.Identity.GetUserId(), true);
                }

                if (import.Contains("--"))
                {
                    if (import.Contains("*[ PARTIAL"))
                    {
                        var splitlist = import.Split(new string[] { "--" }, StringSplitOptions.RemoveEmptyEntries);

                        if (splitlist.Count() > 0)
                        {
                            foreach (var item in splitlist)
                            {
                                //var set = Regex.Matches(item, @"\*+\[ PARTIAL ([\w\s-']+) \]\*+");

                                var set = Regex.Matches(item, @"Curio Set: (.*)\n");

                                var pieces = Regex.Matches(item, @"([\w\s-':]{29})(?:Complete|Missing)", RegexOptions.Multiline);

                                if (set.Count > 0 & pieces.Count > 1)
                                {
                                    foreach (Match piece in pieces)
                                    {
                                        if (piece.Groups[0].Value.EndsWith("Complete"))
                                        {
                                            var missingpiece = piece.Groups[1].Value;
                                            missingpiece = missingpiece.Trim().Replace(":", "");

                                            var pieceId = await _pieceAgent.GetPieceIdForName(set[0].Groups[1].Value.Replace("\r", ""), missingpiece);

                                            if (pieceId > 0)
                                            {
                                                await _userPieceAgent.CreaseUserPiece(new UserPieceModel
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
                                                TempData["ErrorMessage"] += set[0].Groups[1].Value + " " + missingpiece + ", ";
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Please include the starred header of CURIO SHOW PIECE <id>>.";
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Please include the trailing dashed line of CURIO SHOW PIECE <id>>.";
                }

                if (TempData.Values.Count > 0 && (TempData["ErrorMessage"] != null || TempData["ErrorMessage"].ToString() != ""))
                {
                    TempData["ErrorMessage"] = TempData["ErrorMessage"].ToString().Remove(TempData["ErrorMessage"].ToString().Length - 2);
                    TempData["ErrorMessage"] += ".";
                }

                return RedirectToAction("OwnedPieces");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ProcessOwned(int[] selectedOwned, string button)
        {
            if (selectedOwned != null && selectedOwned.Count() > 0 && button == "Delete selected pieces")
            {
                foreach (var item in selectedOwned)
                {
                    await _userPieceAgent.DeleteUserPiece(item);
                }
            } 
            else if (selectedOwned != null && selectedOwned.Count() > 0 && button == "Refresh selected pieces")
            {
                foreach (var item in selectedOwned)
                {
                    await _userPieceAgent.RefreshUserPiece(item);
                }
            }
            return RedirectToAction("OwnedPieces");
        }

        [HttpPost]
        public async Task<ActionResult> ProcessWanted(int[] selectedWanted, string button)
        {
            if (selectedWanted != null && selectedWanted.Count() > 0 && button == "Delete selected pieces")
            {
                foreach (var item in selectedWanted)
                {
                    await _userPieceAgent.DeleteUserPiece(item);
                }
            }
            else if (selectedWanted != null && selectedWanted.Count() > 0 && button == "Refresh selected pieces")
            {
                foreach (var item in selectedWanted)
                {
                    await _userPieceAgent.RefreshUserPiece(item);
                }
            }
            return RedirectToAction("WantedPieces");
        }


    }
}
