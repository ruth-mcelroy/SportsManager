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
    public class SportsManagerAPI : ApiController
    {

        private PlayerDBContext playerDb = new PlayerDBContext();
        private MatchDBContext matchesDb = new MatchDBContext();
        private AvailabilityDBContext availabilityDb = new AvailabilityDBContext();



        [Route("/player/{Irfu}")]
        public Player GetPlayerName(string Irfu)                                                //First thing mobile app does gets correct player by them entering their irfu number
        {
            var player = playerDb.Players.FirstOrDefault(p => p.IRFUNumber == Irfu);
            return (Player)player;
        }


        [Route("availablity/{id}")]                                                                         //Get id from player id of player found above
        public IEnumerable<Availability> GetAvailabilityAllMatchesThisPlayer(int id)           
        {
            var isAvailable = availabilityDb.Available.Where(a => a.Player.ID == id);

            return isAvailable;
        }

        [Route("availablity/{id}/{date:DateTime}")]                                                             //Get availability for matches on a given date(Further work can try and do a between dates find)
        public Availability GetAvailabilityThisPlayerMatchDate(int id, DateTime date)
        {
            var isAvailableDate = availabilityDb.Available.Where(a => a.Player.ID == id)
                                                          .FirstOrDefault(a => a.Match.Time == date);

            return (Availability)isAvailableDate;
        }






        [Route("availability/change/{playerId}/{MatchId}/{availableParam}")]                                         //Put availability because availability object automatically set to false so replacing availability object not creating new one
        public Availability PutAvailability(int playerId, int matchId, bool availableParam)
        {
            Availability changeAvail = availabilityDb.Available.Where(a => a.Player.ID == playerId)
                                                            .FirstOrDefault(a => a.Match.ID == matchId);

            if(changeAvail.Available != availableParam)                                                             //Don't think this is correct, open database and change record directly? Get an example to look at.
            {
                changeAvail.Available = availableParam;
            }

            return changeAvail;
        }


    }
}