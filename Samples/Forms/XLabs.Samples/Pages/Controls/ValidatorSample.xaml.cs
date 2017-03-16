// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ValidatorSample.xaml.cs" company="XLabs Team">
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
using XLabs.Forms.Validation;

namespace XLabs.Samples.Pages.Controls
{
	/// <summary>
	/// Class ValidatorSample.
	/// </summary>
	public partial class ValidatorSample : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidatorSample"/> class.
		/// </summary>
		public ValidatorSample()
		{
			//// User defined validators must be added before
			//// the xaml is parsed
			Rule.AddValidator("EndInCom", MustEndInCom);
			InitializeComponent();
		}

		/// <summary>
		/// Musts the end in COM.
		/// </summary>
		/// <param name="rule">The rule.</param>
		/// <param name="val">The value.</param>
		/// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
		private bool MustEndInCom(Rule rule, string val)
		{
			return string.IsNullOrEmpty(val) || val.EndsWith("com");
		}
	}
}
