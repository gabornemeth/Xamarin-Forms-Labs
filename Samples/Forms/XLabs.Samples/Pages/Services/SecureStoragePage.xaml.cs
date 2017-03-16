// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="SecureStoragePage.xaml.cs" company="XLabs Team">
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
using System.Text;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Services;

namespace XLabs.Samples.Pages.Services
{
    public partial class SecureStoragePage : ContentPage
    {
        public SecureStoragePage()
        {
            InitializeComponent();

            var secureStorage = Resolver.Resolve<ISecureStorage>();

            this.SaveButton.Command = new Command(() =>
                {
                    try
                    {
                        secureStorage.Store(this.KeyEntry.Text, Encoding.UTF8.GetBytes(this.DataEntry.Text));
                    }
                    catch (Exception ex)
                    {
                        this.DisplayAlert("Error", ex.Message, "OK");
                    }
                },
                () => secureStorage != null && !(string.IsNullOrWhiteSpace(this.KeyEntry.Text) || string.IsNullOrWhiteSpace(this.DataEntry.Text)));

            this.DeleteButton.Command = new Command(() =>
                {
                    try
                    {
                        secureStorage.Delete(this.KeyEntry.Text);
                    }
                    catch (Exception ex)
                    {
                        this.DisplayAlert("Error", ex.Message, "OK");
                    }
                },
                () => secureStorage != null && !string.IsNullOrWhiteSpace(this.KeyEntry.Text));

            this.LoadButton.Command = new Command(() =>
                {
                    try
                    {
                        var bytes = secureStorage.Retrieve(this.KeyEntry.Text);
                        this.DataEntry.Text = Encoding.UTF8.GetString(bytes, 0, bytes.Length);
                    }
                    catch (Exception ex)
                    {
                        this.DisplayAlert("Error", ex.Message, "OK");
                    }
                },
                () => secureStorage != null && !string.IsNullOrWhiteSpace(this.KeyEntry.Text));

            this.KeyEntry.TextChanged += (sender, args) =>
            {
                ((Command)this.SaveButton.Command).ChangeCanExecute();
                ((Command)this.DeleteButton.Command).ChangeCanExecute();
                ((Command)this.LoadButton.Command).ChangeCanExecute();
            };

            this.DataEntry.TextChanged += (sender, args) =>
            {
                ((Command)this.SaveButton.Command).ChangeCanExecute();
            };
        }
    }
}
