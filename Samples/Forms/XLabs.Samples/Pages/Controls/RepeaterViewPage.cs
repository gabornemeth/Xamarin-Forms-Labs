// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="RepeaterViewPage.cs" company="XLabs Team">
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
using XLabs.Samples.ViewModel;

namespace XLabs.Samples.Pages.Controls
{
	/// <summary>
	/// Class RepeaterViewPage.
	/// </summary>
	public class RepeaterViewPage : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="RepeaterViewPage"/> class.
		/// </summary>
		public RepeaterViewPage()
		{
			var viewModel = new RepeaterViewViewModel();
			BindingContext = viewModel;

			var repeater = new RepeaterView<Thing>
			{
				Spacing = 10,
				ItemsSource = viewModel.Things,
				ItemTemplate = new DataTemplate(() =>
				{
					var nameLabel = new Label { Font = Font.SystemFontOfSize(NamedSize.Medium) };
					nameLabel.SetBinding(Label.TextProperty, RepeaterViewViewModel.ThingsNamePropertyName);

					var descriptionLabel = new Label { Font = Font.SystemFontOfSize(NamedSize.Small) };
					descriptionLabel.SetBinding(Label.TextProperty, RepeaterViewViewModel.ThingsDescriptionPropertyName);

					ViewCell cell = new ViewCell
					{
						View = new StackLayout
						{
							Spacing = 0,
							Children =
							{
								nameLabel,
								descriptionLabel
							}
						}
					};

					return cell;
				})
			};

			var removeButton = new Button
			{
				Text = "Remove 1st Item",      
				HorizontalOptions = LayoutOptions.Start
			};

			removeButton.SetBinding(Button.CommandProperty, RepeaterViewViewModel.RemoveFirstItemCommandName);

			var addButton = new Button
			{
				Text = "Add New Item",
				HorizontalOptions = LayoutOptions.Start
			};

			addButton.SetBinding(Button.CommandProperty, RepeaterViewViewModel.AddItemCommandName);

			Content = new StackLayout
			{
				Padding = 20,
				Spacing = 5,
				Children = 
				{
					new Label 
					{ 
						Text = "RepeaterView Demo", 
						Font = Font.SystemFontOfSize(NamedSize.Large)
					},
					repeater,
					removeButton,
					addButton
				}
			};

			viewModel.LoadData();
		}
	}
}
