// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="GestureSample.xaml.cs" company="XLabs Team">
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
using XLabs.Samples.ViewModel;

namespace XLabs.Samples.Pages.Controls
{
	/// <summary>
	/// Class GestureSample.
	/// </summary>
	public partial class GestureSample  : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="GestureSample"/> class.
		/// </summary>
		public GestureSample()
		{
			InitializeComponent();
			BindingContext = new GestureSampleVm();
		}
	}
}
