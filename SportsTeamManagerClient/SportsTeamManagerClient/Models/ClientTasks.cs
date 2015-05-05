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

       public static async Task GetAvailabilities(string irfu)    //player irfu number
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8540/api/availability"); //base part of uri

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   //accepts json

                HttpResponseMessage response = await client.GetAsync(irfu);                   // an async call yoke is one that waits for the other thing to do stuff      
                if (response.IsSuccessStatusCode)
                {
                    var avail = await response.Content.ReadAsAsync<IEnumerable<Availability>>();
                    foreach (Availability a in avail)
                    {
                        string answer = "Opposition: " + a.Opposition + " Location: " + a.Location + "  Date: " + a.Date + " Time: " + a.Time;
                        Console.WriteLine(answer);
                    }
                   
                }                                                                               
            }
        }





       

        public static async Task PutAvailable(int AvilabilityID, bool Availability)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:8540/api/availability"); 

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PutAsJsonAsync(Availability.ToString() +"/" + Availability.ToString(), Availability);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response)
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
            }
        }



    }
}
