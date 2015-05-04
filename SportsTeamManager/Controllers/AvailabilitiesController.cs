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
            IEnumerable<Availability> availabilities = db.Availabilities.Include(a => a.Match).Include(a => a.Player);

            if (!String.IsNullOrEmpty(searchString))
            {
                availabilities = availabilities.Where(a => a.Player.Name.Contains(searchString)
                                                        || a.Match.Opposition.Contains(searchString)
                                                        || a.Match.Date.Contains(searchString));

                int amountMatches = availabilities.GroupBy(m => m.MatchID)
                                                    .Count();

                if (amountMatches == 1)
                {
                    int playerCount = availabilities.Where(a => a.Available == true)
                                                    .Count();
                    ViewBag.MessageCount = "There are " + playerCount + " players available for this match.";
                }               
            }   

            return View(availabilities.Where(a => a.Match.TimeAndDate > DateTime.Now)
                                       .OrderBy(a => a.Match.TimeAndDate)
                                       .ThenBy(a => a.Player.Position)
                                       .ThenBy(a => a.Player.Name)
                                       .ToList());
        }

        public ActionResult PastAvailabilities(string searchString)
        {
            IEnumerable<Availability> availabilities = db.Availabilities.Include(a => a.Match).Include(a => a.Player);

            if (!String.IsNullOrEmpty(searchString))
            {
                availabilities = availabilities.Where(a =>a.Match.Date.Contains(searchString));
            }

            return View(availabilities.Where(a =>a.Available == true)
                                       .OrderBy(a => a.Player.Position)
                                       .ThenBy(a => a.Player.Name)
                                       .ToList());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AvailabilityID,PlayerID,MatchID,Available")] Availability availability)
        {
            string search = null;
            if (ModelState.IsValid)
            {
                db.Entry(availability).State = EntityState.Modified;
                db.SaveChanges();
                
                    search = db.Matches.Where(m => m.MatchID == availability.MatchID)
                                                .Select(m => m.Date)
                                                .FirstOrDefault();                   //search = the match date of the availability object                                     
            }

            if (search != null)
            {
                return RedirectToAction("Index", new { searchstring = search });    // returns to all availabilities matching the match date
            } 
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
