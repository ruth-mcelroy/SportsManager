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

        //Get:  /api/availablity/{id}  Gets the availabilities for the player assosiated with the id
        [HttpGet]
        [Route("availablity/{id}")]                                                                         //Get id from player id of player found in GetPlayer
        public IEnumerable<Availability> GetAvailabilityPlayer(int id)
        {
            using (Context availabilityDb = new Context())
            {
                var isAvailable = availabilityDb.Availabilities.Where(a => a.PlayerID == id);

                return isAvailable;
            }
        }

        //Get:  /api/availablity/{id}/{date:DateTime}  Gets the availabilities for the player assosiated with the id on this date
        [HttpGet]
        [Route("availablity/{id}/{date:DateTime}")]                                                                                     //Further work can do a between date A and Date B 
        public Availability GetAvailabilityThisPlayerMatchDate(int id, DateTime date)
        {
            using (Context availabilityDb = new Context())
            {
                var isAvailableDate = availabilityDb.Availabilities.Where(a => a.PlayerID == id)
                                                              .FirstOrDefault(a => a.Match.Time == date);

                return (Availability)isAvailableDate;
            }
        }




        //Put:  /api/availablity/{playerId}/{MatchId}/{availableParam}  Change the availability for this player and the match selected
        [HttpPut]
        [Route("availability/change/{playerId}/{MatchId}/{availableParam}")]                                         //Put availability because availability object already made,  replacing availability object not creating new one
        public Availability PutAvailability(int playerId, int matchId, bool availableParam)
        {
            using (Context availabilityDb = new Context())
            {
                Availability changeAvail = availabilityDb.Availabilities.Where(a => a.PlayerID == playerId)
                                                                .FirstOrDefault(a => a.MatchID == matchId);

                if (changeAvail.Available != availableParam)                                       
                {
                    changeAvail.Available = availableParam;
                }

                return changeAvail;
            }
        }


    }
}