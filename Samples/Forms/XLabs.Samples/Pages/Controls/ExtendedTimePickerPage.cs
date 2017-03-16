// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ExtendedTimePickerPage.cs" company="XLabs Team">
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
	public class ExtendedTimePickerPage : ContentPage
	{
		public ExtendedTimePickerPage()
		{
			var timePicker = new ExtendedTimePicker();

			timePicker.Time = new TimeSpan(11, 12, 0);
			timePicker.MinimumTime = new TimeSpan(10, 10, 0);
			timePicker.MaximumTime = new TimeSpan(18, 40, 0);

			Content = new StackLayout
			{
				Children =
				{
					timePicker
				}
			};
		}
	}
}

