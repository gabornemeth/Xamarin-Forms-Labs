// ***********************************************************************
// Assembly         : XLabs.Forms.Droid
// Author           : XLabs Team
// Created          : 09-26-2016
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ExtendedDatePickerRenderer.cs" company="XLabs Team">
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
using System.ComponentModel;
using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms.Controls;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]

namespace XLabs.Forms.Controls
{
	/// <summary>
	/// Class ExtendedEditorRenderer.
	/// </summary>
	public class ExtendedDatePickerRenderer : DatePickerRenderer
	{
        protected ExtendedDatePicker DatePicker
        {
            get
            {
                return Element as ExtendedDatePicker;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == ExtendedDatePicker.DateProperty.PropertyName)
            {
                if (DatePicker.Date == null)
                    Control.Text = DatePicker.NullText;
            }
        }
    }
}
