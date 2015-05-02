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
    public class AvailabilitiesController : Controller
    {
        private Context db = new Context();

        // GET: Availabilities
        public ActionResult Index(string searchString)
        {
            var availabilities = db.Availabilities.Include(a => a.Match).Include(a => a.Player);

            if (!String.IsNullOrEmpty(searchString))
            {
                availabilities = availabilities.Where(a => a.Player.Name.Contains(searchString)
                                                        || a.Match.Opposition.Contains(searchString)
                                                        || a.Match.Date.Contains(searchString));
            }
            
            return View(availabilities.ToList());
        }






        // GET: Availabilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability availability = db.Availabilities.Find(id);
            if (availability == null)
            {
                return HttpNotFound();
            }
            ViewBag.MatchID = new SelectList(db.Matches, "MatchID", "Opposition", availability.MatchID);
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "IRFUNumber", availability.PlayerID);
            return View(availability);
        }

        // POST: Availabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AvailabilityID,PlayerID,MatchID,Available")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(availability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MatchID = new SelectList(db.Matches, "MatchID", "Opposition", availability.MatchID);
            ViewBag.PlayerID = new SelectList(db.Players, "PlayerID", "IRFUNumber", availability.PlayerID);
            return View(availability);
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
