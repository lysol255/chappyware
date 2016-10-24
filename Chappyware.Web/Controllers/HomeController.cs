using Chappyware.Business;
using Chappyware.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Chappyware.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            FantasyTeamManager manager = new FantasyTeamManager();
            FantasyLeague league = manager.CreateLeague("Robs");
            manager.UpdateLeagueRoster(league, "TeamImport.txt");
            return View();
        }
    }
}