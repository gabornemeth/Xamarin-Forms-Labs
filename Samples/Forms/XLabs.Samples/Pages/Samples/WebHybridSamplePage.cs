// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="WebHybridSamplePage.cs" company="XLabs Team">
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

using System.IO;
using System.Reflection;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Ioc;
using XLabs.Serialization;

namespace XLabs.Samples.Pages.Samples
{
    public class WebHybridSamplePage : ContentPage
    {
        private readonly HybridWebView hybrid;
        public WebHybridSamplePage()
        {
            this.Content = this.hybrid = new HybridWebView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White
            };

            this.hybrid.RegisterCallback("dataCallback", t =>
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.DisplayAlert("Data callback", t, "OK");
                })
            );

            this.hybrid.RegisterCallback("sendObject", s =>
            {
                var serializer = Resolver.Resolve<IJsonSerializer>();

                var o = serializer.Deserialize<SendObject>(s);

                this.DisplayAlert("Object", string.Format("JavaScript sent x: {0}, y: {1}", o.X, o.Y), "OK");
            });

            this.hybrid.RegisterNativeFunction("funcCallback", t => new object[] { "From Func callback: " + t });
        }

        #region Overrides of Page
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var assembly = this.GetType().GetTypeInfo().Assembly;
            using (var reader = new StreamReader(assembly.GetManifestResourceStream("XLabs.Sample.Data.WebHybridTest.html")))
            {
                var str = reader.ReadToEnd();
                this.hybrid.LoadContent(str);
            }
        }

        #endregion
    }

    public class SendObject
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}