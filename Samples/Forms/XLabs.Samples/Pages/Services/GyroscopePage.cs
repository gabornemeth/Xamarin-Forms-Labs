// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="GyroscopePage.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using Xamarin.Forms;
using XLabs.Forms.Controls.SensorBar;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace XLabs.Samples.Pages.Services
{
    public class GyroscopePage : ContentPage
    {
        private readonly IGyroscope gyroscope;
        private readonly SensorBarView xsensor;
        private readonly SensorBarView ysensor;
        private readonly SensorBarView zsensor;

        /// <summary>
        /// Initializes a new instance of the <see cref="AcceleratorSensorPage"/> class.
        /// </summary>
        public GyroscopePage()
        {
            var device = Resolver.Resolve<IDevice> ();
            if (device != null && device.Gyroscope != null)
            {
                this.gyroscope = device.Gyroscope;
            }
            else
            {
                this.gyroscope = Resolver.Resolve<IGyroscope>() ?? DependencyService.Get<IGyroscope>();
            }

            if (gyroscope == null)
            {
                Content = new Label()
                {
                    TextColor = Color.Red,
                    Text = "Device does not have gyroscope sensor or it is not enabled."
                };

                return;
            }

            Title = "Accelerator Sensor";
         
            var grid = new StackLayout ();

            this.xsensor = new SensorBarView () 
            {
                HeightRequest = 75,
                WidthRequest = 250,
                MinimumHeightRequest = 10,
                MinimumWidthRequest = 50,
                BackgroundColor = BackgroundColor
//                VerticalOptions = LayoutOptions.Fill,
//                HorizontalOptions = LayoutOptions.Fill
            };

            this.ysensor = new SensorBarView()
            {
                HeightRequest = 75,
                WidthRequest = 250,
                MinimumHeightRequest = 10,
                MinimumWidthRequest = 50,
                BackgroundColor = BackgroundColor
//                VerticalOptions = LayoutOptions.Fill,
//                HorizontalOptions = LayoutOptions.Fill
            };

            this.zsensor = new SensorBarView()
            {
                HeightRequest = 75,
                WidthRequest = 250,
                MinimumHeightRequest = 10,
                MinimumWidthRequest = 50,
                BackgroundColor = BackgroundColor
//                VerticalOptions = LayoutOptions.Fill,
//                HorizontalOptions = LayoutOptions.Fill
            };


            grid.Children.Add (new Label () { Text = string.Format ("Accelerometer data for {0}", device.Name) });
            grid.Children.Add (new Label () { Text = "X", XAlign = TextAlignment.Center });
            grid.Children.Add (this.xsensor);
            grid.Children.Add (new Label () { Text = "Y", XAlign = TextAlignment.Center });
            grid.Children.Add (this.ysensor);
            grid.Children.Add (new Label () { Text = "Z", XAlign = TextAlignment.Center });
            grid.Children.Add (this.zsensor);

            Content = grid;
        }

        /// <summary>
        /// When overridden, allows application developers to customize behavior immediately prior to the <see cref="T:Xamarin.Forms.Page" /> becoming visible.
        /// </summary>
        /// <remarks>To be added.</remarks>
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.gyroscope == null) return;

            this.gyroscope.ReadingAvailable += GyroscopeReadingAvailable;
        }

        /// <summary>
        /// When overridden, allows the application developer to customize behavior as the <see cref="T:Xamarin.Forms.Page" /> disappears.
        /// </summary>
        /// <remarks>To be added.</remarks>
        protected override void OnDisappearing()
        {
            if (this.gyroscope != null)
            {
                this.gyroscope.ReadingAvailable -= GyroscopeReadingAvailable;
            }
            
            base.OnDisappearing();
        }

        /// <summary>
        /// Accelerometers the reading available.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        void GyroscopeReadingAvailable(object sender, EventArgs<Vector3> e)
        {
            this.xsensor.CurrentValue = e.Value.X;
            this.ysensor.CurrentValue = e.Value.Y;
            this.zsensor.CurrentValue = e.Value.Z;
        }
    }
}
