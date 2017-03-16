// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="GridViewPage.xaml.cs" company="XLabs Team">
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

using XLabs.Forms.Mvvm;
using XLabs.Samples.ViewModel;

namespace XLabs.Samples.Pages.Controls
{
	/// <summary>
	/// Class GridViewPage.
	/// </summary>
	public partial class GridViewPage : BaseView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GridViewPage"/> class.
		/// </summary>
		public GridViewPage ()
		{
			InitializeComponent ();
			BindingContext = ViewModelLocator.Main;
			this.GrdView.ItemSelected += (sender, e) => {
				DisplayAlert ("selected value", e.Value.ToString (), "ok");
			};
		}


	}
}

