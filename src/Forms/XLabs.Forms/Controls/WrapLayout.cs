﻿// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 12-31-2018
// ***********************************************************************
// <copyright file="WrapLayout.cs" company="XLabs Team">
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
using System.Linq;
using Xamarin.Forms;

namespace XLabs.Forms.Controls
{
    /// <summary>
    /// Simple Layout panel which performs wrapping on the boundaries.
    /// Original Source:
    /// https://github.com/conceptdev/xamarin-forms-samples/blob/master/Evolve13/Evolve13/Controls/WrapLayout.cs
    /// </summary>
    public class WrapLayout : Layout<View>
    {
        /// <summary>
        /// Backing Storage for the Orientation property
        /// </summary>
        public static readonly BindableProperty OrientationProperty =
            BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(WrapLayout), StackOrientation.Vertical,
                propertyChanged: (bindable, oldvalue, newvalue) => ((WrapLayout)bindable).InvalidateMeasure());

        /// <summary>
        /// Orientation (Horizontal or Vertical)
        /// </summary>
        public StackOrientation Orientation
        {
            get { return (StackOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        /// <summary>
        /// Backing Storage for the Spacing property
        /// </summary>
        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(
            nameof(Spacing), typeof(double), typeof(WrapLayout), 6.0, propertyChanged: OnSpacingChanged);

        /// <summary>
        /// Spacing added between elements (both directions)
        /// </summary>
        /// <value>The spacing.</value>
        public double Spacing
        {
            get { return (double)GetValue(SpacingProperty); }
            set { SetValue(SpacingProperty, value); }
        }

        static void OnSpacingChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is WrapLayout view)
            {
                view.InvalidateMeasure();
            }
        }

        /// <summary>
        /// This method is called during the measure pass of a layout cycle to get the desired size of an element.
        /// </summary>
        /// <param name="widthConstraint">The available width for the element to use.</param>
        /// <param name="heightConstraint">The available height for the element to use.</param>
        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            if (WidthRequest > 0)
            {
                widthConstraint = Math.Min(widthConstraint, WidthRequest);
            }

            if (HeightRequest > 0)
            {
                heightConstraint = Math.Min(heightConstraint, HeightRequest);
            }

            var internalWidth = double.IsPositiveInfinity(widthConstraint) ? double.PositiveInfinity : Math.Max(0, widthConstraint);
            var internalHeight = double.IsPositiveInfinity(heightConstraint) ? double.PositiveInfinity : Math.Max(0, heightConstraint);

            return Orientation == StackOrientation.Vertical
                ? DoVerticalMeasure(internalWidth, internalHeight)
                    : DoHorizontalMeasure(internalWidth, internalHeight);
        }

        /// <summary>
        /// Does the vertical measure.
        /// </summary>
        /// <returns>The vertical measure.</returns>
        /// <param name="widthConstraint">Width constraint.</param>
        /// <param name="heightConstraint">Height constraint.</param>
        private SizeRequest DoVerticalMeasure(double widthConstraint, double heightConstraint)
        {
            var columnCount = 1;

            double width = 0;
            double height = 0;
            double minWidth = 0;
            double minHeight = 0;
            double heightUsed = 0;

            foreach (var size in Children.Where(c => c.IsVisible).Select(item => item.Measure(widthConstraint, heightConstraint)))
            {
                width = Math.Max(width, size.Request.Width);

                var newHeight = height + size.Request.Height + Spacing;

                if (newHeight > heightConstraint)
                {
                    columnCount++;
                    heightUsed = Math.Max(height, heightUsed);
                    height = size.Request.Height;
                }
                else
                {
                    height = newHeight;
                }

                minHeight = Math.Max(minHeight, size.Minimum.Height);
                minWidth = Math.Max(minWidth, size.Minimum.Width);
            }

            if (columnCount <= 1)
            {
                return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
            }

            height = Math.Max(height, heightUsed);
            width *= columnCount;  // take max width

            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }

        /// <summary>
        /// Does the horizontal measure.
        /// </summary>
        /// <returns>The horizontal measure.</returns>
        /// <param name="widthConstraint">Width constraint.</param>
        /// <param name="heightConstraint">Height constraint.</param>
        private SizeRequest DoHorizontalMeasure(double widthConstraint, double heightConstraint)
        {
            var rowCount = 1;

            double width = 0;
            double height = 0;
            double minWidth = 0;
            double minHeight = 0;
            double widthUsed = 0;

            foreach (var item in Children.Where(c => c.IsVisible))
            {
                var size = item.Measure(widthConstraint, heightConstraint);

                height = Math.Max(height, size.Request.Height);

                var newWidth = width + size.Request.Width + Spacing;

                if (newWidth > widthConstraint)
                {
                    rowCount++;
                    widthUsed = Math.Max(width, widthUsed);
                    width = size.Request.Width;
                }
                else
                {
                    width = newWidth;
                }

                minHeight = Math.Max(minHeight, size.Minimum.Height);
                minWidth = Math.Max(minWidth, size.Minimum.Width);
            }

            if (rowCount <= 1)
            {
                return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
            }

            width = Math.Max(width, widthUsed);
            height = (height + Spacing) * rowCount;   // - Spacing;
            //height *= rowCount;  // take max height

            return new SizeRequest(new Size(width, height), new Size(minWidth, minHeight));
        }

        /// <summary>
        /// Positions and sizes the children of a Layout.
        /// </summary>
        /// <param name="x">A value representing the x coordinate of the child region bounding box.</param>
        /// <param name="y">A value representing the y coordinate of the child region bounding box.</param>
        /// <param name="width">A value representing the width of the child region bounding box.</param>
        /// <param name="height">A value representing the height of the child region bounding box.</param>
        protected override void LayoutChildren(double x, double y, double width, double height)
        {
            if (Orientation == StackOrientation.Vertical)
            {
                double colWidth = 0;
                var yPos = y;
                var xPos = x;

                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);

                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    colWidth = Math.Max(colWidth, childWidth);

                    if (yPos + childHeight > height)
                    {
                        yPos = y;
                        xPos += colWidth + Spacing;
                        colWidth = 0;
                    }

                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);

                    LayoutChildIntoBoundingRegion(child, region);

                    yPos += region.Height + Spacing;
                }
            }
            else
            {
                double rowHeight = 0;
                var yPos = y;
                var xPos = x;

                foreach (var child in Children.Where(c => c.IsVisible))
                {
                    var request = child.Measure(width, height);

                    var childWidth = request.Request.Width;
                    var childHeight = request.Request.Height;

                    if (xPos + childWidth > width)
                    {
                        xPos = x;
                        yPos += rowHeight + Spacing;
                        rowHeight = childHeight;
                    }
                    else
                    {
                        rowHeight = Math.Max(rowHeight, childHeight);
                    }

                    var region = new Rectangle(xPos, yPos, childWidth, childHeight);

                    LayoutChildIntoBoundingRegion(child, region);

                    xPos += region.Width + Spacing;
                }
            }
        }
    }
}

