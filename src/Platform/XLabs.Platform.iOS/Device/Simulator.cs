// ***********************************************************************
// Assembly         : XLabs.Platform.iOS
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="Simulator.cs" company="XLabs Team">
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

using UIKit;

namespace XLabs.Platform.Device
{
	/// <summary>
	/// Apple device Simulator.
	/// </summary>
	public class Simulator : AppleDevice
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="Simulator" /> class.
		/// </summary>
		internal Simulator()
		{
            var size = GetDisplaySize();
            var dpi = size.Scale * 163;
            Display = new Display(size.Height, size.Width, dpi, dpi);

			Name = HardwareVersion = "Simulator";
		}
	}
}