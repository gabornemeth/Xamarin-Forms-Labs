// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="WebHybridTestPage.cs" company="XLabs Team">
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
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace XLabs.Samples.Pages.Controls
{
    public class WebHybridTestPage : ContentPage
    {
        public WebHybridTestPage()
        {
            var stack = new StackLayout { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
            var hwv = new HybridWebView { VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

            stack.Children.Add(hwv);
            this.Content = stack;

            hwv.Uri = new Uri("http://test.padrose.co.uk/hvw/test1.html");

            hwv.RegisterCallback("dataCallback", t =>
                Device.BeginInvokeOnMainThread(() =>
                {
                    /**********************************/
                    //THIS WILL WORK FOR PAGE 1 ONLY
                    /*********************************/
                    System.Diagnostics.Debug.WriteLine("!!!!!!!!!!!!!!!!! dataCallback: " + t);
                })
            );

            hwv.LoadFinished += (s, e) =>
            {
                /***********************************/
                //THIS WILL WORK FOR PAGE 1 ONLY
                //WEAK REFERENCE LOST???
                /***********************************/
                System.Diagnostics.Debug.WriteLine("(!!!!!!!!!!!!!!!!!!!! LoadFinished");
            };
        }


    }
}
