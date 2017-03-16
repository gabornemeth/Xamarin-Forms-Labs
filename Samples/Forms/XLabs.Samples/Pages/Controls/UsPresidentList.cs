// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="UsPresidentList.cs" company="XLabs Team">
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
using XLabs.Forms.Controls;
using XLabs.Ioc;
using XLabs.Samples.Data;
using XLabs.Serialization;

namespace XLabs.Samples.Pages.Controls
{
    public class UsPresidentList : ContentPage
    {
        public UsPresidentList()
        {
            this.Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                Children =
                {
                    new ActivityIndicator
                    {
                        Color = Color.Red,
                        IsRunning = true,
                        IsVisible = true
                    }
                }
            };
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var source = new JsonImageListSource(Resolver.Resolve<IJsonSerializer>());
            var items = await source.GetListItems();

            var list = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = items,
                ItemTemplate = new DataTemplate(() =>
                {
                    var image = new CircleImage
                    {
                        Aspect = Aspect.AspectFill,
                        HeightRequest = Device.OnPlatform(50, 75, 75),
                        WidthRequest = Device.OnPlatform(50, 75, 75),
                    };
                    image.SetBinding(Image.SourceProperty, "Image");
                    var label = new Label
                    {
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof (Label)),
                        YAlign = TextAlignment.Center,
                        XAlign = TextAlignment.Center
                    };
                    label.SetBinding(Label.TextProperty, "Title");

                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(20),
                            Spacing = 10,
                            Orientation = StackOrientation.Horizontal,
                            Children = { image, label }
                        }
                    };
                })
            };

            this.Content = new StackLayout
            {
                Children =
                {
                    new Label { Text = Device.OnPlatform(
                        "Works on iOS", 
                        "Works on Android now as well", 
                        "Works on WP") },
                    list
                }
            };
        }
    }
}