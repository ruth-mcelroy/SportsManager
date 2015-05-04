using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SportsTeamManager.Models;


namespace SportsTeamManager.Controllers
{
    public class MatchesController : Controller
    {
       
        private Context db = new Context();
        

        // GET: Matches
        public ActionResult Index(string searchString)      //Can search for opposition or date or part thereof
        {
            IEnumerable<Match> matches = db.Matches;

            if (!String.IsNullOrEmpty(searchString))
            {
               matches = matches.Where(a => a.Opposition.Contains(searchString)                                                        
                                               || a.Date.Contains(searchString));

            }

            return View(matches.ToList()
                                    .Where(match => match.TimeAndDate >= DateTime.Now)  //Only shows current matches
                                    .OrderBy(match => match.TimeAndDate));    
        }

                // GET: Matches
        public ActionResult PastMatches()
        {
            return View(db.Matches.ToList()
                                    .Where(match => match.TimeAndDate < DateTime.Now)
                                    .OrderBy(match => match.TimeAndDate));     //Different page for past matches
        }



        // GET: Matches/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Matches/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MatchID,Opposition,Location,Time,Date,TimeAndDate,Competition")] Match match)
        {
            
            if (ModelState.IsValid)
            {
                db.Matches.Add(match);
                match.UpdateTime();
                
                db.SaveChanges();

                match.CreateAvailableNewMatch();                //Creates availability objects assosiated with this match and every player.
                return RedirectToAction("Index");
            }

            return View(match);
        }

        // GET: Matches/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MatchID,Opposition,Location,Time,Date,TimeAndDate,Competition")] Match match)
        {
            if (ModelState.IsValid)
            {
                match.UpdateTime();
                db.Entry(match).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(match);
        }

        // GET: Matches/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Match match = db.Matches.Find(id);
            if (match == null)
            {
                return HttpNotFound();
            }
            return View(match);
        }

        // POST: Matches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Match match = db.Matches.Find(id);
            db.Matches.Remove(match);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
