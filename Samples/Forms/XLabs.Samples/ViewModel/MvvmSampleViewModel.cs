// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="MvvmSampleViewModel.cs" company="XLabs Team">
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
using XLabs.Forms.Mvvm;
using XLabs.Samples.Pages.Mvvm;

namespace XLabs.Samples.ViewModel
{
	/// <summary>
	/// The MVVM sample view model.
	/// </summary>
	[ViewType(typeof(MvvmSamplePage))]
	public class MvvmSampleViewModel : XLabs.Forms.Mvvm.ViewModel
	{
		private Command _navigateToViewModel;
		private string _navigateToViewModelButtonText = "Navigate to another view model";

		/// <summary>
		/// Gets the navigate to view model.
		/// </summary>
		/// <value>
		/// The navigate to view model.
		/// </value>
		public Command NavigateToViewModel 
		{
			get
			{
				return _navigateToViewModel ?? (_navigateToViewModel = new Command(
					async () => NavigationService.NavigateTo<NewPageViewModel>(),
																		   () => true));
			}
		}

		/// <summary>
		/// Gets or sets the navigate to view model button text.
		/// </summary>
		/// <value>
		/// The navigate to view model button text.
		/// </value>
		public string NavigateToViewModelButtonText
		{
			get
			{
				return _navigateToViewModelButtonText;
			}
			set
			{ 
				this.SetProperty(ref _navigateToViewModelButtonText, value);
			}
		}
	}
}

