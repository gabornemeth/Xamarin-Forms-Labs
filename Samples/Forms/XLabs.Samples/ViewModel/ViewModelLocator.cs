// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ViewModelLocator.cs" company="XLabs Team">
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
namespace XLabs.Samples.ViewModel
{
    public class ViewModelLocator
    {
        private static MainViewModel _main;
        private static AutoCompleteViewModel _autoCompleteViewModel;

        /// <summary>
        /// Gets the main.
        /// </summary>
        /// <value>The main.</value>
        public static MainViewModel Main
        {
            get
            {
                if (_main == null)
                    _main = new MainViewModel ();
                return _main;
            }
        }

        /// <summary>
        /// Gets the automatic complete view model.
        /// </summary>
        /// <value>The automatic complete view model.</value>
        public static AutoCompleteViewModel AutoCompleteViewModel
        {
            get
            {
                if (_autoCompleteViewModel == null)
                {
                    _autoCompleteViewModel = new AutoCompleteViewModel();
                }
                return _autoCompleteViewModel;
            }
        }
    }
}
