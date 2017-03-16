// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="PopupPage.xaml.cs" company="XLabs Team">
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
    public partial class PopupPage
    {
        public PopupPage()
        {
            InitializeComponent();

            this.OpenButton.Clicked += OpenButtonClicked;
        }

        void OpenButtonClicked(object sender, EventArgs e)
        {
            var popupLayout = this.Content as PopupLayout;

            if (popupLayout.IsPopupActive)
            {
                popupLayout.DismissPopup();
            }
            else
            {
                var list = new ListView()
                {
                    BackgroundColor = Color.White,
                    ItemsSource = new[] { "1", "2", "3" },
                    HeightRequest = this.Height * .5,
                    WidthRequest = this.Width * .8
                };

                list.ItemSelected += (s, args) => 
                    popupLayout.DismissPopup();

                popupLayout.ShowPopup(list);
            }
        }
    }
}
