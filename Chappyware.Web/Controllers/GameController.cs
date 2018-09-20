using Chappyware.Data;
using Chappyware.Data.DataObjects;
using Chappyware.Web.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Chappyware.Web.Controllers
{
    public class GameController : Controller
    {

        private const string _GetPlayerGameRoute = "~/games/player/{id?}";
        private const string _GetGamesRoute = "~/games";



        // GET: Game
        public ActionResult Index()
        {
            return View();
        }

        // GET: Game/Details/5
        [Route(_GetGamesRoute, Name = "games")]
        [HttpGet]
        public ActionResult GetGames()
        {
            JsonResult result = new JsonResult();

            GameStatFactory factory = GameStatFactory.Instance;

            GameStatCollection games = factory.GetGames();

            List<GameModel> gameModels = GetGameModels(games);

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            result.Data = JsonConvert.SerializeObject(gameModels);

            return result;
        }

        // GET: Game/Details/5
        [Route(_GetPlayerGameRoute, Name = "playergames")]
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


        private List<GameModel> GetGameModels(GameStatCollection games)
        {
            List<GameModel> gameModels = new List<GameModel>();
            foreach (GameStat game in games.GameStats )
            {
                GameModel model = new GameModel();
                model.AwayTeam = game.AwayTeamCode;
                model.AwayGoals = game.AwayTeamPlayerStats.Values.SingleOrDefault(p => p.Name == "TOTAL").Goals; 


                model.HomeTeam = game.HomeTeamCode;
                model.HomeGoals = game.HomeTeamPlayerStats.Values.SingleOrDefault(p => p.Name == "TOTAL").Goals;

                model.RecordDate = game.GetGameDate();

                gameModels.Add(model);
            }

            return gameModels;
        }
    }
}
