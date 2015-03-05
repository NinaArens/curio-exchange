using CurioExchange.Interfaces;
using CurioExchange.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CurioExchange.Controllers
{
    public class UsersController : Controller
    {
        private IUserPieceAgent _userPieceAgent;
        private IUserAgent _userAgent;

        public UsersController(IUserPieceAgent userPieceAgent, IUserAgent userAgent)
        {
            _userPieceAgent = userPieceAgent;
            _userAgent = userAgent;
        }

        // GET: User
        public async Task<ActionResult> Index()
        {
            var model = await _userAgent.RetrieveUsers();
            var test = model.FirstOrDefault(t => t.UserName.ToLower() == "test");
            model.Remove(test);
            model = model.OrderBy(t => t.UserName).ToList();
            return View(model);
        }

        // GET: User/Details/5
        public async Task<ActionResult> Details(string userId)
        {
            var model = new UserPieceViewModel();
            var userPieces = await _userPieceAgent.RetrieveUserPieces(userId, User.Identity.Name);
            model.WantedPieces.AddRange(userPieces.Where(t => t.Owned == false));
            model.OwnedPieces.AddRange(userPieces.Where(t => t.Owned));
            var user = await _userAgent.RetrieveUserById(userId);
            if (user != null) model.Username = user.UserName;
            return View(model);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
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

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: User/Edit/5
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

        // GET: User/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: User/Delete/5
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
