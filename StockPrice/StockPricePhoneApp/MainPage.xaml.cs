// a Windows Phone 8 app which calls an operation on a RESTful web service and displays the results
// also uses ASP.Net Web API client utilities - NuGet install
// display on a long list selector (flat not grouped)

using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;


using System.Collections.ObjectModel;

namespace StockPricePhoneApp
{
    // main page for app
    public partial class MainPage : PhoneApplicationPage
    {

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        // display prices button clicked - event handler
        private async void GetMatches(object sender, RoutedEventArgs e)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://sportsteammanager.cloudapp.net/api/availability"); //base part of uri

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));   //accepts json
                string number = irfu.Text;

                HttpResponseMessage response = await client.GetAsync("http://sportsteammanager.cloudapp.net/api/availability/" + number);

                if (response.IsSuccessStatusCode)                                                   // 200.299
                {
                    // read result and display on UI

                    var listings = await response.Content.ReadAsAsync<IEnumerable<Availability>>();

                    // set the data source for the priceList long list selector
                    listSelect.ItemsSource = new ObservableCollection<Availability>(listings);
                }
                else
                {
                    //
                }
            }
        }

        private void irfuFocus(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            irfu.Text ="";
        }

    }
}