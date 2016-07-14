using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LITDucks.Data;
using LITDucks.Models;
using Microsoft.AspNet.SignalR;

namespace LITDucks.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repo = new LITDucksRepository(Properties.Settings.Default.ConStr);
            return View(new IndexViewModel { QuackCounts = repo.GetCounts() });
        }

        [HttpPost]
        public void AddRealDuckQuack()
        {
            var repo = new LITDucksRepository(Properties.Settings.Default.ConStr);
            repo.AddRealDuckQuack();
            SendCountsToClient();
        }

        [HttpPost]
        public void AddRubberDuckQuack()
        {
            var repo = new LITDucksRepository(Properties.Settings.Default.ConStr);
            repo.AddRubberDuckQuack();
            SendCountsToClient();
        }

        private void SendCountsToClient()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<QuackHub>();
            var repo = new LITDucksRepository(Properties.Settings.Default.ConStr);
            context.Clients.All.quackCountsReceived(repo.GetCounts());
        }

    }
}
