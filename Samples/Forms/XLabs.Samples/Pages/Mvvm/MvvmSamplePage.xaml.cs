// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="MvvmSamplePage.xaml.cs" company="XLabs Team">
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
using XLabs.Samples.ViewModel;

namespace XLabs.Samples.Pages.Mvvm
{
	/// <summary>
	/// Class MvvmSamplePage.
	/// </summary>
	public partial class MvvmSamplePage : BaseView
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MvvmSamplePage"/> class.
		/// </summary>
		public MvvmSamplePage()
		{
			InitializeComponent ();
			BindingContext = new MvvmSampleViewModel ();

			Icon = Device.OnPlatform("pie27_32.png", "pie27_32.png", "Images/pie27_32.png");
		}
	}
}

