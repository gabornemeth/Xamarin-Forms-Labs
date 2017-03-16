// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ButtonPage.xaml.cs" company="XLabs Team">
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
using XLabs.Data;
using XLabs.Forms.Controls;

namespace XLabs.Samples.Pages.Controls
{
    /// <summary>
    /// Class ButtonPage.
    /// </summary>
    public partial class ButtonPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ButtonPage"/> class.
        /// </summary>
        public ButtonPage()
        {
            InitializeComponent();

            TwitterButton.Clicked += ButtonClick;
            FacebookButton.Clicked += ButtonClick;
            //Showing custom font in image button
            FacebookButton.Font = Font.OfSize("Open 24 Display St", 20);
            GoogleButton.Clicked += ButtonClick;
            MicrosoftButton.Clicked += ButtonClick;

            this.BindingContext = new ButtonPageViewModel();
        }

        /// <summary>
        /// Handles the Click event of the Button control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ButtonClick(object sender, EventArgs e)
        {
            var button = sender as ImageButton;
            this.DisplayAlert("Button Pressed", string.Format("The {0} button was pressed.", button.Text), "OK",
                "Cancel");
        }
    }

    public class ButtonPageViewModel : ObservableObject
    {
        private bool buttonEnabled;

        public bool ButtonEnabled
        {
            get
            {
                return this.buttonEnabled;
            }
            set
            {
                if (this.SetProperty(ref this.buttonEnabled, value))
                {
                    this.NotifyPropertyChanged("EnabledButtonTitle");
                }
            }
        }

        public string EnabledButtonTitle
        {
            get
            {
                return this.buttonEnabled ? "Enabled Image" : "Disabled image";
            }
        }
    }
}
