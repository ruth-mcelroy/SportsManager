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

        [Route("matches")]
        public IEnumerable<Match> GetAllMatches()
        {
            return matchesDb.Matches.ToList();
        }

        [Route("matches/date")]
        public Match GetMatchDate(DateTime date)
        {
            var match = matchesDb.Matches.FirstOrDefault((m) => m.Time == date);
            return match;
        }


        [Route("player/name")]
        public Player GetPlayerName(string name)
        {
            var player = playerDb.Players.Where(p => p.Name == name);
            return (Player)player;
        }

        [Route("player/IRFU")]
        public Player GetPlayerName(string Irfu)
        {
            var player = playerDb.Players.Where(p => p.IRFUNumber == Irfu);
            return (Player)player;
        }


        [Route("availablity")]
        public Availability GetAvailabilityPlayerMatch(Player p, Match m)           //or do inputs need to be stringd or ints? change to ids of player and match?
        {
            var available = availabilityDb.Available.Where(a => a.Player == p)
                                                    .Where(a => a.Match == m);
            return (Availability)available;
        }

        [Route("availablity/player")]
        public IEnumerable<Availability> GetAvailabilityPlayerMatch(Player p)
        {
            var available = availabilityDb.Available.Where(a => a.Player == p);
            return (IEnumerable<Availability>)available;
        }


        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}