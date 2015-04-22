using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SportsTeamManager.Models;

namespace SportsTeamManager.Controllers
{
    public class AvailabilityController : Controller        
    {
        private Context db = new Context();

        // GET: Availabilities
        public ActionResult Index()
        {
            return View(db.Availabilities.ToList());     //Does this show player and match id? Show player and match names and date instead.
        }

        // GET: Availabilities/Details/5
        public ActionResult Details(int? AvailabilityID)
        {
            if (AvailabilityID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Availability availability = db.Availabilities.Find(AvailabilityID);
            if (availability == null)
            {
                return HttpNotFound();
            }
            return View(availability);
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
            return View(availability);
        }

        // POST: Availabilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Player.Name,Match.Opposition,Available")] Availability availability)
        {
            if (ModelState.IsValid)
            {
                db.Entry(availability).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
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
