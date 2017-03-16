// ***********************************************************************
// Assembly         : XLabs.Sample.iOS
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

using System;
using Foundation;
using UIKit;
using Xamarin.Forms;
using XLabs.Forms.Controls;
using XLabs.Sample.iOS.DynamicListView;

[assembly: ExportRenderer(typeof(DynamicListView<object>), typeof(BasicListRenderer))]

namespace XLabs.Sample.iOS.DynamicListView
{
    /// <summary>
    /// Class BasicListRenderer.
    /// </summary>
    public class BasicListRenderer : DynamicUITableViewRenderer<object>
    {
        /// <summary>
        /// Gets the cell.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="item">The item.</param>
        /// <returns>UITableViewCell.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
    
        protected override UITableViewCell GetCell(UITableView tableView, object item)
        {
            if (item is string)
            {
                return base.GetCell(tableView, item);
            }
            
            if (item is DateTime)
            {
                var cell = new UITableViewCell(UITableViewCellStyle.Value1, this.GetType().Name);

                cell.TextLabel.Text = ((DateTime)item).ToShortDateString();
                cell.DetailTextLabel.Text = ((DateTime)item).ToShortTimeString();
                return cell;
            }

            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the height for row.
        /// </summary>
        /// <param name="tableView">The table view.</param>
        /// <param name="indexPath">The index path.</param>
        /// <returns>System.Single.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            var item = this.Element.Data[(int)indexPath.Item];
            if (item is string)
            {
                return base.GetHeightForRow(tableView, indexPath);
            }
            else if (item is DateTime)
            {
                return 44f;
            }

            throw new NotImplementedException();
        }
    }
}