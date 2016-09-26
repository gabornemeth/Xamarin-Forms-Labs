// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ExtendedDatePicker.cs" company="XLabs Team">
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
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace XLabs.Forms.Controls
{
    /// <summary>
    /// Class ExtendedDatePicker.
    /// </summary>
    public class ExtendedDatePicker : DatePicker
    {
        #region HasBorder property

        /// <summary>
        /// The HasBorder property
        /// </summary>
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(nameof(HasBorder), typeof(bool), typeof(ExtendedDatePicker), true);

        /// <summary>
        /// Gets or sets if the border should be shown or not
        /// </summary>
        public bool HasBorder
        {
            get { return (bool)GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        #endregion

        #region NullText property

        public static readonly BindableProperty NullTextProperty = BindableProperty.Create(nameof(NullText), typeof(string), typeof(ExtendedDatePicker), "(no value)");

        public string NullText
        {
            get { return GetValue(NullTextProperty) as string; }
            set { SetValue(NullTextProperty, value); }
        }

        #endregion

        #region Date property

        public new static readonly BindableProperty DateProperty = BindableProperty.Create(nameof(Date), typeof(DateTime?), typeof(ExtendedDatePicker), DateTime.Now);

        /// <summary>
        /// used for determining whether Xamarin.Forms.DatePicker.Date or ExtendedDatePicker.Date has been changed
        /// </summary>
        private bool _isDateChangeFromCurrent;

        public new DateTime? Date
        {
            get
            {
                return GetValue(DateProperty) as DateTime?;
            }
            set
            {
                try
                {
                    _isDateChangeFromCurrent = true;
                    SetValue(DateProperty, value);
                }
                finally
                {
                    _isDateChangeFromCurrent = false;
                }
            }
        }

        #endregion

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == DateProperty.PropertyName)
            {
                if (!_isDateChangeFromCurrent)
                    this.Date = base.Date; // update from base class
            }
        }
    }
}
