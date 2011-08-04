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
    public class Default1Controller : Controller
    {
        private DB3687_daveEntities db = new DB3687_daveEntities();

        //
        // GET: /Default1/

        public ViewResult Index()
        {
            return View(db.ballances.ToList());
        }

        //
        // GET: /Default1/Details/5

        public ViewResult Details(int id)
        {
            ballance ballance = db.ballances.Single(b => b.Id == id);
            return View(ballance);
        }

        //
        // GET: /Default1/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Default1/Create

        [HttpPost]
        public ActionResult Create(ballance ballance)
        {
            if (ModelState.IsValid)
            {
                db.ballances.AddObject(ballance);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(ballance);
        }
        
        //
        // GET: /Default1/Edit/5
 
        public ActionResult Edit(int id)
        {
            ballance ballance = db.ballances.Single(b => b.Id == id);
            return View(ballance);
        }

        //
        // POST: /Default1/Edit/5

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

        //
        // GET: /Default1/Delete/5
 
        public ActionResult Delete(int id)
        {
            ballance ballance = db.ballances.Single(b => b.Id == id);
            return View(ballance);
        }

        //
        // POST: /Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            ballance ballance = db.ballances.Single(b => b.Id == id);
            db.ballances.DeleteObject(ballance);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}