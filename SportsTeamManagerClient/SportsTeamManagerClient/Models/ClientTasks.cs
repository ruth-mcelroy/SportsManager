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

       public static async Task GetPlayer(string irfu)    //player irfu number
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8540/api/player"); //base part of uri

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   //accepts json

                HttpResponseMessage response = await client.GetAsync(irfu);                   // an async call yoke is one that waits for the other thing to do stuff      
                if (response.IsSuccessStatusCode)
                {
                    var player = await response.Content.ReadAsAsync<Player>();
                    Console.WriteLine(player.Name);                                             //Writelines so I can see I am getting player
                }                                                                               //Create new player object here?
            }
        }





       public static async Task GetAvailabilities(int id)     //player id
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8540/api/availability"); //base part of uri

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   //accepts json

                HttpResponseMessage response = await client.GetAsync(id.ToString());                   // an async call yoke is one that waits for the other thing to do stuff      
                if (response.IsSuccessStatusCode)
                {
                    var availabilities = await response.Content.ReadAsAsync<IEnumerable<Availability>>();       //Need to store this as a list somewhere? New class as list?

                    foreach(Availability a in availabilities )
                    {
                        Console.WriteLine(a.Opposition);
                        Console.WriteLine(a.Available);
                    }
                }
            }
        }




        public static async Task PutAvailable(int playerId, int matchId, bool Availability)
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


        public class Test                                               //This only works when here and not in seperate class why??
        {
            static void Main()
        {
            Console.WriteLine("Please enter your Irfu number:");
            
            Task t1 = GetPlayer(Console.ReadLine());                //Doen't seem to be get them ok. Do I need JSON other end?
            t1.Wait();
            Task t2 = GetAvailabilities(1);                                 
            t2.Wait();
            Task t3 = PutAvailable(1, 1, true);                     //Sending message ok
            t3.Wait();
        }
        }

    }
}
