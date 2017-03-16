// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="FontManagerPage.cs" company="XLabs Team">
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
using XLabs.Forms.Services;
using XLabs.Platform.Device;

namespace XLabs.Samples.Pages.Services
{
    /// <summary>
    /// Class FontManagerPage.
    /// </summary>
    public class FontManagerPage : TabbedPage
    {
        private const double FontSize = 0.25;

        /// <summary>
        /// Initializes a new instance of the <see cref="FontManagerPage"/> class.
        /// </summary>
        /// <param name="display">The display.</param>
        /// <param name="fontManager">The font manager.</param>
        public FontManagerPage(IDisplay display, IFontManager fontManager)
        {
            var stack = new StackLayout();

            foreach (var namedSize in Enum.GetValues(typeof(NamedSize)))
            {
                var font = Font.SystemFontOfSize((NamedSize)namedSize);

                var height = fontManager.GetHeight(font);

                var heightRequest = display.HeightRequestInInches(height);

                var label = new Label()
                {
                    Font = font,
                    HeightRequest = heightRequest + 10,
                    Text = string.Format("System font {0} is {1:0.000}in tall.", namedSize, height),
                    XAlign = TextAlignment.Center
                };

                stack.Children.Add(label);
            }

            var f = Font.SystemFontOfSize(24);

            var inchFont = fontManager.FindClosest(f.FontFamily, FontSize);

            stack.Children.Add(new Label()
            {
                Text = "The below text should be " + FontSize + "in height from its highest point to lowest.",
                XAlign = TextAlignment.Center
            });


            stack.Children.Add(new Label()
            {
                Text = "FfTtLlGgJjPp",
                TextColor = Color.Lime,
                FontSize = inchFont.FontSize,
//                BackgroundColor = Color.Gray,
//                FontFamily = inchFont.FontFamily,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Start
            });


            stack.Children.Add(new Label()
            {
                Text = FontSize + "in height = SystemFontOfSize(" + inchFont.FontSize + ")",
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.End
            });

            this.Children.Add(new ContentPage() { Title = "Sizes", Content = stack });

            var listView = new ListView
            {
                ItemsSource = fontManager.AvailableFonts,
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label();
                    label.SetBinding(Label.TextProperty, ".");
                    label.SetBinding(Label.FontFamilyProperty, ".");
                    return new ViewCell { View = label};
                })
            };

            this.Children.Add(new ContentPage { Title = "Fonts", Content = listView });
        }
    }
}
