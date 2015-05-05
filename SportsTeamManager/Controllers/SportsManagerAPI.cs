using SportsTeamManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace SportsTeamManager.Controllers
{
    [RoutePrefix("api")]
    public class SportsManagerController : ApiController
    {
        //Get:  /api/availability/{Irfu}  Gets the availabilities for the player assosiated with the id
        [HttpGet]
        [Route("availability/{Irfu}")]                                                                         //Get id from player id of player found in GetPlayer
        public IEnumerable<ClientAvailability> GetAvailabilityPlayer(string Irfu)
        {
            Context availabilityDb = new Context();

            var isAvailable = availabilityDb.Availabilities.Where(a => a.Player.IRFUNumber == Irfu)
                                                                .Select (a=> (new ClientAvailability{AvailabilityID = a.AvailabilityID, IRFUNumber = a.Player.IRFUNumber, Name = a.Player.Name, Opposition = a.Match.Opposition.ToString(), Location = a.Match.Location, Date = a.Match.Date, Time = a.Match.Time, Available = a.Available})); //Not serialising if just sending availability
                return isAvailable;
        }





        //Put:  /api/availability/{AvailabilityID}/{availableParam} Change the availability for this player and the match selected
        [HttpPut]
        [Route("availability/{AvailabilityID}/{availableParam}")]                                         //Put availability because availability object already made,  replacing availability object not creating new one
        public Availability PutAvailability(int AvailabilityID, [FromUri]bool availableParam)
        {
            Context availabilityDb = new Context();

            Availability changeAvail = availabilityDb.Availabilities.Where(a=> a.AvailabilityID == AvailabilityID)
                                                                    .FirstOrDefault();

            if (changeAvail.Available != availableParam)                                       
            {
                changeAvail.Available = availableParam;
                availabilityDb.Entry(changeAvail).State = System.Data.Entity.EntityState.Modified;
                availabilityDb.SaveChanges();
            }

            return changeAvail;
            
        }


    }
}