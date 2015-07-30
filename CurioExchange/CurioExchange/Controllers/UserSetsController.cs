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
    public class UserSetsController : Controller
    {
        private IUserSetAgent _userSetAgent;
        private ISetAgent _SetAgent;

        public UserSetsController(IUserSetAgent userSetAgent, ISetAgent SetAgent)
        {
            _userSetAgent = userSetAgent;
            _SetAgent = SetAgent;
        }

        // GET: UserSet
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> WantedSets()
        {
            var model = new List<UserSetModel>();
            var userSets = await _userSetAgent.RetrieveUserSetsWanted(User.Identity.GetUserId());
            model.AddRange(userSets);
            return View(model);
        }

        public async Task<ActionResult> OwnedSets()
        {
            var model = new List<UserSetModel>();
            var userSets = await _userSetAgent.RetrieveUserSetsOwned(User.Identity.GetUserId());
            model.AddRange(userSets);
            return View(model);
        }

        // GET: UserSet/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserSet/Create
        public async Task<ActionResult> CreateWanted()
        {
            var model = new UserSetModel
            {
                Sets = await _SetAgent.RetrieveSets(),
                User_Id = User.Identity.GetUserId(),
                Amount = 1,
                Added = DateTime.Now
            };
            model.Sets = model.Sets.OrderBy(t => t.Name).ToList();
            return View(model);
        }

        public async Task<ActionResult> CreateOwned()
        {
            var model = new UserSetModel
            {
                Owned = true,
                Sets = await _SetAgent.RetrieveSets(),
                User_Id = User.Identity.GetUserId(),
                Amount = 1,
                Added = DateTime.Now
            };
            model.Sets = model.Sets.OrderBy(t => t.Name).ToList();
            return View(model);
        }

        // POST: UserSet/Create
        [HttpPost]
        public async Task<ActionResult> CreateWanted(UserSetModel model)
        {
            try
            {
                await _userSetAgent.CreaseUserSets(model);
                return RedirectToAction("WantedSets");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public async Task<ActionResult> CreateOwned(UserSetModel model)
        {
            try
            {
                await _userSetAgent.CreaseUserSets(model);
                return RedirectToAction("OwnedSets");
            }
            catch
            {
                return View(model);
            }
        }

        // GET: UserSet/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserSet/Edit/5
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

        // GET: UserSet/Delete/5
        public async Task<ActionResult> Delete(int id, bool owned)
        {
            await _userSetAgent.DeleteUserSet(id);
            if (owned)
                return RedirectToAction("OwnedSets");
            else
                return RedirectToAction("WantedSets");
        }

        public async Task<ActionResult> Refresh(int id, bool owned)
        {
            await _userSetAgent.RefreshUserSet(id);
            if (owned)
                return RedirectToAction("OwnedSets");
            else
                return RedirectToAction("WantedSets");
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
                    await _userSetAgent.DeleteUserSets(User.Identity.GetUserId(), true);
                }

                var results = Regex.Matches(import, "^([\\d ]{7}) [\\w\\s]{8} ([\\w\\s-']{20}) [\\w\\s-']{12} [\\w\\s-']{0,28}$", RegexOptions.Multiline);

                if (results.Count > 0)
                {
                    foreach (Match item in results)
                    {
                        var SetId = await _SetAgent.GetSetIdForName(item.Groups[2].Value.TrimEnd());

                        if (SetId > 0)
                        {
                            await _userSetAgent.CreaseUserSet(new UserSetModel
                            {
                                Owned = true,
                                Set_Id = SetId,
                                User_Id = User.Identity.GetUserId(),
                                Added = DateTime.Now,
                                OwnedID = Convert.ToInt32(item.Groups[1].Value.TrimEnd())
                            });
                        }
                        else
                        {
                            if (TempData["ErrorMessage"] == null || TempData["ErrorMessage"].ToString() == "")
                            {
                                TempData["ErrorMessage"] = "The following set(s) do not yet exist in the database: ";
                            }
                            TempData["ErrorMessage"] += item.Groups[2].Value + ", ";
                        }
                    }
                }

                if (TempData.Values.Count > 0 && (TempData["ErrorMessage"] != null || TempData["ErrorMessage"].ToString() != ""))
                {
                    TempData["ErrorMessage"] = TempData["ErrorMessage"].ToString().Remove(TempData["ErrorMessage"].ToString().Length - 2);
                    TempData["ErrorMessage"] += ".";
                }

                return RedirectToAction("OwnedSets");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public async Task<ActionResult> ProcessOwned(int[] selectedOwned, string button)
        {
            if (selectedOwned != null && selectedOwned.Count() > 0 && button == "Delete selected sets")
            {
                foreach (var item in selectedOwned)
                {
                    await _userSetAgent.DeleteUserSet(item);
                }
            } 
            else if (selectedOwned != null && selectedOwned.Count() > 0 && button == "Refresh selected sets")
            {
                foreach (var item in selectedOwned)
                {
                    await _userSetAgent.RefreshUserSet(item);
                }
            }
            return RedirectToAction("OwnedSets");
        }

        [HttpPost]
        public async Task<ActionResult> ProcessWanted(int[] selectedWanted, string button)
        {
            if (selectedWanted != null && selectedWanted.Count() > 0 && button == "Delete selected sets")
            {
                foreach (var item in selectedWanted)
                {
                    await _userSetAgent.DeleteUserSet(item);
                }
            }
            else if (selectedWanted != null && selectedWanted.Count() > 0 && button == "Refresh selected sets")
            {
                foreach (var item in selectedWanted)
                {
                    await _userSetAgent.RefreshUserSet(item);
                }
            }
            return RedirectToAction("WantedSets");
        }
    }
}
