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



        [HttpGet]
        [Route("player/{Irfu}")]
        public Player GetPlayerName(string Irfu)                                                //First thing mobile app does gets correct player by them entering their irfu number
        {
            using (Context playersDb = new Context())
            {
                var player = playersDb.Players.FirstOrDefault(p => p.IRFUNumber == Irfu);
                return (Player)player;
            }
        }

        [HttpGet]
        [Route("availablity/{id}")]                                                                         //Get id from player id of player found above
        public IEnumerable<Availability> GetAvailabilityAllMatchesThisPlayer(int id)
        {
            using (Context availabilityDb = new Context())
            {
                var isAvailable = availabilityDb.Availabilities.Where(a => a.PlayerID == id);

                return isAvailable;
            }
        }


        [HttpGet]
        [Route("availablity/{id}/{date:DateTime}")]                                                             //Get availability for matches on a given date(Further work can try and do a between dates find)
        public Availability GetAvailabilityThisPlayerMatchDate(int id, DateTime date)
        {
            using (Context availabilityDb = new Context())
            {
                var isAvailableDate = availabilityDb.Availabilities.Where(a => a.PlayerID == id)
                                                              .FirstOrDefault(a => a.Match.Time == date);

                return (Availability)isAvailableDate;
            }
        }





        [HttpPost]
        [Route("availability/change/{playerId}/{MatchId}/{availableParam}")]                                         //Put availability because availability object automatically set to false so replacing availability object not creating new one
        public Availability PutAvailability(int playerId, int matchId, bool availableParam)
        {
            using (Context availabilityDb = new Context())
            {
                Availability changeAvail = availabilityDb.Availabilities.Where(a => a.PlayerID == playerId)
                                                                .FirstOrDefault(a => a.MatchID == matchId);

                if (changeAvail.Available != availableParam)                                                             //Don't think this is correct, open database and change record directly? Get an example to look at.
                {
                    changeAvail.Available = availableParam;
                }

                return changeAvail;
            }
        }


    }
}