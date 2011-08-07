using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ballanceApp.Models;

namespace ballanceApp.Controllers
{
    public class ballanceController : Controller
    {

        private DB3687_daveEntities db = new DB3687_daveEntities();

        //
        // GET: /ballance/
        public ViewResult Index()
        {
            makeUpToDate();

            int StartDate = 15;
            decimal payPerMonth = 3000.00M;
            int daysInMonth = 31;

            var ballList = db.ballances.ToList().OrderBy(p => p.Date).Where(p => p.Date >= DateTime.Now.AddMonths(-1)).ToList();

            // TODO: months could be 30,31,29 or 28 days.
            decimal idealAmountPerDay = payPerMonth / daysInMonth;

            foreach (var ball in ballList)
            {
                // TODO: months could be 30,31,29 or 28 days. 
                var numberOfDaysSinceStart = ball.Date.Day > StartDate - 1 ? ball.Date.Day - StartDate + 1 : ball.Date.Day + (daysInMonth - StartDate + 1);

                ball.daysSinceStart = numberOfDaysSinceStart;

                ball.idealBallance = payPerMonth -(  idealAmountPerDay * numberOfDaysSinceStart);

                if (ball.Amount.HasValue)
                {
                    ball.Difference =   ball.Amount.Value  - ball.idealBallance ;
                }
            }

            return View(ballList);
        }



        private void makeUpToDate()
        {

            var exsistingBallances = db.ballances;

            DateTime DateOfLastBallance = exsistingBallances.Max(m => m.Date).Date;

            if (DateOfLastBallance < DateTime.Now.Date)
            {
                // Insert ballance records of for any Days in last month that do no have any records. 
                for (var i = 0; i < 30; i++)
                {
                    var dateToCheck = DateTime.Now.AddDays(-i);

                    var containsRecord = false;
                    foreach (var m in exsistingBallances)
                    {
                        if (DateTime.Equals(m.Date.Date, dateToCheck.Date))
                        {
                            containsRecord = true;
                        }
                    }

                    if (!containsRecord)
                    {
                        db.ballances.AddObject(new ballance { Date = dateToCheck });
                    }
                }
                db.SaveChanges();
            }
        }

        // Get :/Balance/Edit/5
        public ActionResult Edit(int id)
        {
            var ballance = db.ballances.Single(b => b.Id == id);
            return View(ballance);
        }

        //post: /Ballance/Edit/5
        [HttpPost]
        public ActionResult Edit(ballance ballance)
        {
            if (ModelState.IsValid)
            {
                db.ballances.Attach(ballance);
                db.ObjectStateManager.ChangeObjectState(ballance, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ballance);
        }




    }
}
