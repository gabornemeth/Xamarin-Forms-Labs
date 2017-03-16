// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="RadioButtonPage.xaml.cs" company="XLabs Team">
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

namespace XLabs.Samples.Pages.Controls
{
    public partial class RadioButtonPage
    {
        public RadioButtonPage()
        {
            InitializeComponent();

            BackgroundColor = Color.Black;

            ansPicker.ItemsSource = new[]
            {
                "Red",
                "Blue",
                "Green",
                "Yellow",
                "Orange"
            };

            ansPicker.CheckedChanged += ansPicker_CheckedChanged;
        }

        private void ansPicker_CheckedChanged(object sender, int e)
        {
            var radio = sender as CustomRadioButton;

            if (radio == null || radio.Id == -1)
            {
                return;
            }

            DisplayAlert("Info", radio.Text, "OK");
        }
    }

}
