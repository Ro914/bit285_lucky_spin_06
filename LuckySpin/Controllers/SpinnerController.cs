using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels;

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        private LuckySpinDataContext _dbc;
        Random random;

        /***
         * Controller Constructor
         */
        public SpinnerController(LuckySpinDataContext luckySpinDataContext)
        {
            random = new Random();
            //TODO: Inject the LuckySpinDataContext
            _dbc = luckySpinDataContext;

        }

        /***
         * Entry Page Action
         **/

        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }

        [HttpPost]
        public IActionResult Index(Player player)
        {
            if (!ModelState.IsValid) { return View(); }

            // TODO: Add the Player to the DB and save the changes
            _dbc.Players.Add(player);
            _dbc.SaveChanges();

            // TODO: BONUS: Build a new SpinItViewModel object with data from the Player and pass it to the View
            SpinViewModel svm = new SpinViewModel();
            svm.Name = player.FirstName;
            svm.Luck = player.Luck;
            svm.Balance = player.Balance;

            return RedirectToAction("SpinIt", svm);
        }

        /***
         * Spin Action
         **/  
               
         public IActionResult SpinIt(SpinViewModel svm)
        {
            svm.A = random.Next(1, 10);
            svm.B = random.Next(1, 10);
            svm.C = random.Next(1, 10);

            svm.IsWinning = (svm.A == svm.Luck || svm.B == svm.Luck || svm.C == svm.Luck);
            Spin spin = new Spin();
            spin.IsWinning = svm.IsWinning;

            //Add to Spin Repository
            _dbc.Spins.Add(spin);
            _dbc.SaveChanges();
            //repository.AddSpin(spin);

            //Prepare the View
            if (svm.IsWinning)
                ViewBag.Display = "block";
            else
                ViewBag.Display = "none";

            //ViewBag.FirstName = player.FirstName;

            return View("SpinIt", svm);
        }

        /***
         * ListSpins Action
         **/

         public IActionResult LuckList()
        {
                return View();
        }

    }
}

