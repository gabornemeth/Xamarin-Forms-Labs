// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="NewPageViewModel.cs" company="XLabs Team">
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
	//[ViewType(typeof(NewPageView))] can specify this
	/// <summary>
	/// Class NewPageViewModel.
	/// </summary>
	public class NewPageViewModel : XLabs.Forms.Mvvm.ViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NewPageViewModel"/> class.
		/// </summary>
		public NewPageViewModel ()
		{
			NewPage =@"This page was created by the view factory and binded to the viewmodel and injected a navigation context using the following code:
						ViewFactory.Register<NewPageView,NewPageViewModel> ();
						We can also navigate to this page from any view model using the following code: 
						await Navigation.PushAsync<NewPageViewModel>() ";

		}

		/// <summary>
		/// The _new page
		/// </summary>
		private string _newPage =string.Empty;
		/// <summary>
		/// Gets or sets the new page.
		/// </summary>
		/// <value>The new page.</value>
		public string NewPage
		{
			get
			{
				return _newPage;
			}
			set
						
			{
				this.SetProperty(ref _newPage, value);
			}
		}

		/// <summary>
		/// The _page title
		/// </summary>
		private string _pageTitle ="New Page";
		/// <summary>
		/// Gets or sets the page title.
		/// </summary>
		/// <value>The page title.</value>
		public string PageTitle
		{
			get
			{
				return _pageTitle;
			}
			set
			{
				this.SetProperty(ref _pageTitle, value);
			}
		}
	}
}

