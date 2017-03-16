// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="SegmentPage.cs" company="XLabs Team">
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
	/// Class SegmentPage.
	/// </summary>
	public class SegmentPage : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SegmentPage"/> class.
		/// </summary>
		public SegmentPage()
		{
			var segment = new SegmentControl
			{
				TintColor = Color.Green
			};

			segment.AddSegment("Green");
			segment.AddSegment("Yellow");
			segment.AddSegment("Red");

			segment.SelectedSegmentChanged += (sender, segmentIndex) =>
			{
				switch (segmentIndex)
				{
					case 0:
						segment.TintColor = Color.Green;
						break;
					case 1:
						segment.TintColor = Color.Yellow;
						break;
					case 2:
						segment.TintColor = Color.Red;
						break;
				}
			};

			segment.SelectedSegment = 1;

			Content = new StackLayout
			{
				Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 10),
				Orientation = StackOrientation.Vertical,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Children = { segment },
			};
		}
	}
}
