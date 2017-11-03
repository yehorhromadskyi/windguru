using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.ServiceLocation;
using Windguru.Core.Services;
using Windguru.Core.Models.Common;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Windguru.Droid.Adapters;
using System.Globalization;

namespace Windguru.Droid.Activities
{
    [Activity(Label = "Forecast", Theme = "@style/MainTheme")]
    public class SpotForecastActivity : AppCompatActivity
    {
        public RecyclerView HourlyForecastRecyclerView { get; private set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SpotForecastView);

            HourlyForecastRecyclerView = FindViewById<RecyclerView>(Resource.Id.forecastRecyclerView);

            var apiProvider = ServiceLocator.Current.GetInstance<IApiProvider>();

            var id = Intent.GetIntExtra("spotId", -1);
            if (id > 0)
            {
                var forecast = await apiProvider.GetSpotForecastAsync(id);
                var data = forecast.Forecast.Data;

                var report = new List<HourlyForecast>();

                //if (!DateTime.TryParseExact(data.InitialDate, new string[] { "dd.MM.yyyy", "MM.dd.yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime initialDate))
                //{
                //    initialDate = DateTime.Today;
                //}
                for (var i = 0; i < data.DayWeekly.Length; i++)
                {
                    report.Add(new HourlyForecast
                    {
                        Precipitation = $"{data.APCP[i]}",
                        Temperature = $"{data.Temperature[i]} C",
                        Day = $"{((DayOfWeek)data.DayWeekly[i])} {data.DayHourly[i]}",
                        Hour = $"{data.HourHourly[i]}h",
                        WindSpeed = $"{data.WINDSPD[i]} kn",
                        WindGusts = data.GUST[i],
                        WindDirection = data.WINDDIR[i],
                    });
                }

                HourlyForecastRecyclerView.SetAdapter(new HourlyForecastsAdapter(report));
                HourlyForecastRecyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false));
            }
        }
    }
}