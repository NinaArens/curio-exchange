using CurioExchange.Interfaces;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using CurioExchange.ViewModels;

namespace CurioExchange.Controllers
{
    [Authorize] 
    public class TradesController : Controller
    {
        private IUserPieceAgent _userPieceAgent;
        private IUserSetAgent _userSetAgent;

        public TradesController(IUserPieceAgent userPieceAgent, IUserSetAgent userSetAgent)
        {
            _userPieceAgent = userPieceAgent;
            _userSetAgent = userSetAgent;
        }

        // GET: Trades
        public async Task<ActionResult> Index()
        {
            var model = new UserPieceViewModel();
            var wanted = await _userPieceAgent.RetrieveTradesWanted(User.Identity.GetUserId());
            model.WantedPieces.AddRange(wanted);
            var owned = await _userPieceAgent.RetrieveTradesOwned(User.Identity.GetUserId());
            model.OwnedPieces.AddRange(owned);
            var wantedSets = await _userSetAgent.RetrieveTradesWanted(User.Identity.GetUserId());
            model.WantedSets.AddRange(wantedSets);
            var ownedSets = await _userSetAgent.RetrieveTradesOwned(User.Identity.GetUserId());
            model.OwnedSets.AddRange(ownedSets);
            model.Username = User.Identity.Name;
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
