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

namespace Windguru.Droid.Activities
{
    [Activity(Label = "Forecast")]
    public class SpotForecastActivity : Activity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.SpotForecast);

            var apiProvider = ServiceLocator.Current.GetInstance<IApiProvider>();

            var id = Intent.GetIntExtra("spotId", -1);
            if (id > 0)
            {
                var forecast = await apiProvider.GetSpotForecastAsync(id);
                var data = forecast.Forecast.Data;

                var report = new List<HourlyForecast>();

                for (var i = 0; i < data.HrD.Length; i++)
                {
                    report.Add(new HourlyForecast
                    {
                        Precipitation = data.APCP[i],
                        Temperature = data.TMPE[i],
                        Hour = data.HrH[i]
                    });
                }
            }
        }
    }
}