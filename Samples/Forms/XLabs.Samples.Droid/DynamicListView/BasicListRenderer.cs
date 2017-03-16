// ***********************************************************************
// Assembly         : XLabs.Sample.Droid
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="BasicListRenderer.cs" company="XLabs Team">
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
using XLabs.Sample.Droid.DynamicListView;

[assembly: ExportRenderer(typeof(DynamicListView<object>), typeof(BasicListRenderer))]

namespace XLabs.Sample.Droid.DynamicListView
{
    public class BasicListRenderer : DynamicListViewRenderer<object>
    {
        protected override Android.Views.View GetView(object item, Android.Views.View convertView, Android.Views.ViewGroup parent)
        {
            return base.GetView(item, convertView, parent);
        }
    }
}

