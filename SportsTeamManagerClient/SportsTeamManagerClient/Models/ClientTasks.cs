using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

//The results of all of these tasks need to be stored or shown.


namespace SportsTeamManagerClient.Models
{
    public class ClientTasks
    {
        static async Task GetPlayer(string irfu)    //player irfu number
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8540/api/player"); //base part of uri

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   //accepts json

                HttpResponseMessage response = await client.GetAsync(irfu);                   // an async call yoke is one that waits for the other thing to do stuff      
                if (response.IsSuccessStatusCode)
                {
                    var player = await response.Content.ReadAsAsync<Player>();
                }
            }
        }



        static async Task GetAvailabilities(int id)     //player id
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8540/api/availability"); //base part of uri

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   //accepts json

                HttpResponseMessage response = await client.GetAsync(id.ToString());                   // an async call yoke is one that waits for the other thing to do stuff      
                if (response.IsSuccessStatusCode)
                {
                    var player = await response.Content.ReadAsAsync<IEnumerable<Availability>>();       //Need to store this as a list somewhere? New class as list?
                }
            }
        }


        static async Task PostAvailable(int playerId, int matchId, bool Availability)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8540/api/availability/change/"); 

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync(playerId.ToString() +"/" + matchId.ToString() + "/" + Availability.ToString(), Availability);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("Message sent ok");
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
            }
        }



    }
}
