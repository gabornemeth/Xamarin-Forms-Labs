// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
//
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ExtendedEntryPage.xaml.cs" company="XLabs Team">
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
    /// <summary>
    /// Example page showing the ExtendedEntry control
    /// </summary>
    public partial class ExtendedEntryPage : ContentPage
    {
        /// <summary>
        /// Creates a new instance of the <see cref="ExtendedEntryPage"/> class
        /// </summary>
        public ExtendedEntryPage()
        {
            InitializeComponent();
            var entryFromCode = new ExtendedEntry() {
                Placeholder = "Aded from code, custom font",
                Font = Font.OfSize("Open 24 Display St", NamedSize.Medium)
            };

            layout.Children.Add(entryFromCode);

            var swipeEntry = new ExtendedEntry()
            {
                    Placeholder = "Swipe me..."
            };

            swipeEntry.LeftSwipe += (s, e) => swipeEntry.Text = "Swiped left";

            swipeEntry.RightSwipe += (s, e) => swipeEntry.Text = "Swiped right";

            layout.Children.Add(swipeEntry);
        }
    }
}
