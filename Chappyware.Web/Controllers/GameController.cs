using Chappyware.Data;
using Chappyware.Data.DataObjects;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chappyware.Web.Controllers
{
    public class GameController : Controller
    {

        private const string _GetPlayerGameRoute = "~/games/player/{id?}";


        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        // GET: Game/Details/5
        [Route(_GetPlayerGameRoute, Name = "games")]
        [HttpGet]
        public ActionResult GetGamesByPlayer(string id)
        {
            JsonResult result = new JsonResult();

            GameStatFactory factory = GameStatFactory.Instance;

            List<GameStat> games = factory.FindGamesForPlayer(id);            

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(games);

            return result;
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
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

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Game/Edit/5
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

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Game/Delete/5
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
