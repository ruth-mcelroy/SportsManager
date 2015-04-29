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


        //Get:  /api/player/{Irfu}  Gets the player assosiated with the IRFU(Registration) number
        [HttpGet]
        [Route("player/{Irfu}")]
        public Player GetPlayer(string Irfu)                                                //First thing mobile app does gets correct player by them entering their irfu number. Each player will know their id number
        {
            using (Context playersDb = new Context())
            {
                var player = playersDb.Players.FirstOrDefault(p => p.IRFUNumber == Irfu);
                return (Player)player;
            }
        }

        //Get:  /api/availability/{id}  Gets the availabilities for the player assosiated with the id
        [HttpGet]
        [Route("availability/{id}")]                                                                         //Get id from player id of player found in GetPlayer
        public IEnumerable<ClientAvailability> GetAvailabilityPlayer(int id)
        {
            Context availabilityDb = new Context();
            
                var isAvailable = availabilityDb.Availabilities.Where(a => a.PlayerID == id)
                                                                .Select (a=> (new ClientAvailability{ID = a.AvailabilityID, Name = a.Player.Name, Opposition = a.Match.Opposition, Time = a.Match.Time, Available = a.Available })); //Not serialising if just sending availability
                return isAvailable;
        }

        //Get:  /api/availability/{id}/yyyy-mm-dd Gets the availabilities for the player assosiated with the id on this date
        [HttpGet]
        [Route("availability/{id}/{date:DateTime}")]                                                                                     //Further work can do a between date A and Date B 
        public ClientAvailability GetAvailabilityThisPlayerMatchDate(int id, DateTime date)                                              //Haven't implemented this in the client yet
        {
            using (Context availabilityDb = new Context())
            {
                var isAvailableDate = availabilityDb.Availabilities.Where(a => a.PlayerID == id)
                                                                    .Select(a => new ClientAvailability { ID = a.AvailabilityID, Name = a.Player.Name,MatchId = a.Match.MatchID, Opposition = a.Match.Opposition, Time = a.Match.Time, Available = a.Available })
                                                                    .FirstOrDefault(a => a.Time == date);
                                                                    

                return (ClientAvailability)isAvailableDate;
            }
        }




        //Put:  /api/availability/{playerId}/{MatchId}/{availableParam}  Change the availability for this player and the match selected
        [HttpPut]
        [Route("availability/change/{playerId}/{MatchId}/{availableParam}")]                                         //Put availability because availability object already made,  replacing availability object not creating new one
        public Availability PutAvailability(int playerId, int matchId, [FromUri]bool availableParam)
        {
            Context availabilityDb = new Context();
            
            Availability changeAvail = availabilityDb.Availabilities.Where(a => a.Player.PlayerID == playerId)
                                                                .FirstOrDefault(a => a.Match.MatchID == matchId);

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