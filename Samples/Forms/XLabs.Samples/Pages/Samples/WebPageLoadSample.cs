// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="WebPageLoadSample.cs" company="XLabs Team">
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

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace XLabs.Samples.Pages.Samples
{
    public class WebPageLoadSample : ContentPage
    {
        private HybridWebView hybrid;
        private readonly List<Uri> pages;
        private readonly Label addressLabel;
        private CancellationTokenSource source;
        private int index = 0;

        public WebPageLoadSample()
        {
            this.pages = new List<Uri>(new[]
            {
                new Uri("http://www.openstreetmap.org "),
                new Uri("https://www.google.pl/maps"),
                new Uri("https://www.bing.com/maps/ "),
            });
            
            this.Padding = new Thickness(0, 20, 0, 0);

            this.hybrid = new HybridWebView()
            {
                BackgroundColor = Color.Red,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                StyleId = "HybridWebView",
            };


            var mainLayout = new RelativeLayout
            {
                StyleId = "MainLayout",
            };

            this.addressLabel = new Label()
            {
                StyleId = "UrlLoaded",
                FontSize = 8
            };

            var labelHeight = Device.OnPlatform(10, 10, 20);

            mainLayout.Children.Add(this.addressLabel,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => p.Width),
                Constraint.Constant(labelHeight));

            mainLayout.Children.Add(this.hybrid,
                Constraint.Constant(0),
                Constraint.RelativeToView(addressLabel, (p, v) => v.Y + v.Height),
                Constraint.RelativeToParent(p => 
                    {
                        return p.Width;
                    }),
                Constraint.RelativeToView(addressLabel, (p, v) => p.Height - v.Height));

            this.Content = mainLayout;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            this.source = new CancellationTokenSource();

            CircleUrls(this.source.Token);
        }

        protected override void OnDisappearing()
        {
            this.source.Cancel();
            base.OnDisappearing();
        }

        private async Task CircleUrls(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await LoadPage(this.pages[this.index++], token);
                this.index %= this.pages.Count;
                await Task.Delay(10000);
            }
        }

        public Task<bool> LoadPage(Uri page, CancellationToken token)
        {
            var tcs = new TaskCompletionSource<bool>();

            EventHandler e = null;

            e = (sender, args) =>
            {
                if (!token.IsCancellationRequested) 
                {
                    Device.BeginInvokeOnMainThread(() => 
                    {
                        this.addressLabel.Text = page.ToString();
                        this.addressLabel.BackgroundColor = Color.Green;
                    });
                }

                this.hybrid.RegisterCallback("getUri", s =>
                {
                    System.Diagnostics.Debug.WriteLine(s);
                    this.hybrid.LoadFinished -= e;
                    this.hybrid.RemoveCallback("getUri");
                    tcs.SetResult(true);
                });

                this.hybrid.InjectJavaScript("Native(\"getUri\", window.location.href)");
            };

            this.hybrid.LoadFinished += e;

            Device.BeginInvokeOnMainThread(() =>
            {
                this.addressLabel.Text = string.Empty;
                this.addressLabel.BackgroundColor = Color.Yellow;
                this.hybrid.Uri = page;
                
            });

            return tcs.Task;
        }
    }
}

