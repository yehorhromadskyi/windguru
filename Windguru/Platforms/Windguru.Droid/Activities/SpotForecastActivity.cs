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
        public RecyclerView DailyForecastRecyclerView { get; private set; }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SpotForecastView);

            DailyForecastRecyclerView = FindViewById<RecyclerView>(Resource.Id.dailyForecastRecyclerView);
            HourlyForecastRecyclerView = FindViewById<RecyclerView>(Resource.Id.hourlyForecastRecyclerView);

            var apiProvider = ServiceLocator.Current.GetInstance<IApiProvider>();

            var id = Intent.GetIntExtra("spotId", -1);
            if (id > 0)
            {
                var forecast = await apiProvider.GetSpotForecastAsync(id);
                var data = forecast.Forecast.Data;

                Title = forecast.Name;

                var dailyForecast = new List<DailyForecast>();

                //if (!DateTime.TryParseExact(data.InitialDate, new string[] { "dd.MM.yyyy", "MM.dd.yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime initialDate))
                //{
                //    initialDate = DateTime.Today;
                //}

                for (var i = 0; i < data.DayWeekly.Length; i++)
                {
                    var currentDaily = new DailyForecast { Day = $"{((DayOfWeek)data.DayWeekly[i])} {data.DayHourly[i]}" };
                    var savedDaily = dailyForecast.FirstOrDefault(d => d.Day == currentDaily.Day);
                    if (savedDaily == null)
                    {
                        dailyForecast.Add(currentDaily);
                    }
                    else
                    {
                        currentDaily = savedDaily;
                    }

                    currentDaily.HourlyForecast.Add(new HourlyForecast
                    {
                        Hour = $"{data.HourHourly[i]}h",
                        Temperature = $"{(int)data.Temperature[i]}°C",
                        Precipitation = $"{data.APCP[i] ?? 0} mm",
                        WindSpeed = $"{data.WINDSPD[i]} kn",
                        WindGusts = data.GUST[i],
                        WindDirection = data.WINDDIR[i],
                    });
                }

                if (!dailyForecast.Any())
                {
                    return;
                    // TODO: Show error message
                }

                DailyForecastRecyclerView.SetAdapter(new DailyForecastAdapter(dailyForecast));
                DailyForecastRecyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false));

                HourlyForecastRecyclerView.SetAdapter(new HourlyForecastAdapter(dailyForecast.First().HourlyForecast));
                HourlyForecastRecyclerView.SetLayoutManager(new LinearLayoutManager(this, LinearLayoutManager.Horizontal, false));
            }
        }
    }
}