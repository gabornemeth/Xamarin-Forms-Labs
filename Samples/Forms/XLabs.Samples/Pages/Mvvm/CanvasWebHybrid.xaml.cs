// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
//
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="CanvasWebHybrid.xaml.cs" company="XLabs Team">
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

using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Samples.ViewModel;

namespace XLabs.Samples.Pages.Mvvm
{
    public partial class CanvasWebHybrid : BaseView
    {
        private bool loaded;

        public CanvasWebHybrid ()
        {
            InitializeComponent ();

            //this.NativeList.HeightRequest = Device.OnPlatform(250, 320, 150);
            //this.hybridWebView.HeightRequest = Device.OnPlatform(300, 300, 400);

            this.hybridWebView.RegisterCallback("dataCallback", t =>
                Debug.WriteLine(t)
            );

            this.hybridWebView.RegisterCallback("chartUpdated", t =>
                Debug.WriteLine(t)
            );

            var model = ChartViewModel.Dummy;


            this.BindingContext = model;

            model.PropertyChanged += HandlePropertyChanged;

            model.DataPoints.CollectionChanged += HandleCollectionChanged;

            foreach (var datapoint in model.DataPoints)
            {
                datapoint.PropertyChanged += HandlePropertyChanged;
            }

            this.hybridWebView.LoadFinished += (s, e) =>
            {
                var display = Resolver.Resolve<IDisplay>();

                var height = Device.OnPlatform(this.hybridWebView.Height * display.Scale, this.hybridWebView.Height,
                    this.hybridWebView.Height * display.Scale);

                this.loaded = true;
                this.hybridWebView.CallJsFunction("setChartHeight", height);
                this.hybridWebView.CallJsFunction("onViewModelData", this.BindingContext);
            };

            this.hybridWebView.LeftSwipe += (s, e) =>
                Debug.WriteLine("Left swipe from HybridWebView");

            this.hybridWebView.RightSwipe += (s, e) =>
                Debug.WriteLine("Right swipe from HybridWebView");

            // this would not work as the control has not been loaded yet
            // this.hybridWebView.LoadFromContent("HTML/home.html");
        }

        void HandleCollectionChanged (object sender, NotifyCollectionChangedEventArgs e)
        {
            foreach (var datapoint in e.NewItems.OfType<DataPoint>())
            {
                datapoint.PropertyChanged += HandlePropertyChanged;
            }

            foreach (var datapoint in e.OldItems.OfType<DataPoint>())
            {
                datapoint.PropertyChanged -= HandlePropertyChanged;
            }
        }

        void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.loaded)
            {
                this.hybridWebView.CallJsFunction("onViewModelData", this.BindingContext);
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.hybridWebView.LoadFromContent("HTML/home.html");
        }
    }
}

