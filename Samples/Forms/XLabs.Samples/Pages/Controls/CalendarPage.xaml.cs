// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="CalendarPage.xaml.cs" company="XLabs Team">
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
	/// <summary>
	/// Class CalendarPage.
	/// </summary>
	public partial class CalendarPage : ContentPage
	{
		/// <summary>
		/// The _calendar view
		/// </summary>
		readonly CalendarView _calendarView;
		/// <summary>
		/// The _relative layout
		/// </summary>
		readonly RelativeLayout _relativeLayout;
		/// <summary>
		/// The _stacker
		/// </summary>
		readonly StackLayout _stacker;

		/// <summary>
		/// Initializes a new instance of the <see cref="CalendarPage"/> class.
		/// </summary>
		public CalendarPage()
		{
			InitializeComponent();
  
		   

			_relativeLayout = new RelativeLayout() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			Content = _relativeLayout;



			_calendarView = new CalendarView() {
				//BackgroundColor = Color.Blue
				MinDate = CalendarView.FirstDayOfMonth(DateTime.Now),
				MaxDate = CalendarView.LastDayOfMonth(DateTime.Now.AddMonths(3)),
				HighlightedDateBackgroundColor = Color.FromRgb(227	,227,	227	),
				ShouldHighlightDaysOfWeekLabels = false,
				SelectionBackgroundStyle = CalendarView.BackgroundStyle.CircleFill,
				TodayBackgroundStyle = CalendarView.BackgroundStyle.CircleOutline,
				HighlightedDaysOfWeek = new DayOfWeek[]{DayOfWeek.Saturday,DayOfWeek.Sunday},
				ShowNavigationArrows = true,
				MonthTitleFont = Font.OfSize("Open 24 Display St",NamedSize.Medium)

			};

			_relativeLayout.Children.Add(_calendarView,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent(p => p.Width),
				Constraint.RelativeToParent(p => p.Height * 2/3));

			_stacker = new StackLayout() {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.StartAndExpand
			};
			_relativeLayout.Children.Add(_stacker,
				Constraint.Constant(0),
				Constraint.RelativeToParent(p => p.Height *2/3),
				Constraint.RelativeToParent(p => p.Width),
				Constraint.RelativeToParent(p => p.Height *1/3)
			);
			_calendarView.DateSelected += (object sender, DateTime e) =>
			{
				_stacker.Children.Add(new Label()
					{
						Text = "Date Was Selected" + e.ToString("d"),
						VerticalOptions = LayoutOptions.Start,
						HorizontalOptions = LayoutOptions.CenterAndExpand,
					});
			};
		}
	}
}

