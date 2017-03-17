// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 03-17-2017
// 
// Last Modified By : XLabs Team
// Last Modified On : 03-17-2017
// ***********************************************************************
// <copyright file="GridView.cs" company="XLabs Team">
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
using System.Collections;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.Forms;

namespace XLabs.Forms.Controls
{
    /// <summary>
    /// Class WrapGrid.
    /// </summary>
    public class WrapGrid : ContentView
    {
        private WrapLayout _layout = new WrapLayout();

        /// <summary>
        /// Initializes a new instance of the <see cref="WrapGrid"/> class.
        /// </summary>
        public WrapGrid()
        {
            _layout.Orientation = Orientation;
            this.Content = _layout;
        }

        /// <summary>
        /// The Orientation property
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(WrapGrid),
            StackOrientation.Horizontal, BindingMode.OneWay, propertyChanged: OnOrientationChanged);

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public StackOrientation Orientation
        {
            get
            {
                return (StackOrientation)GetValue(WrapGrid.OrientationProperty);
            }
            set
            {
                base.SetValue(WrapGrid.OrientationProperty, value);
            }
        }

        private static void OnOrientationChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as WrapGrid;
            if (view != null)
            {
                view._layout.Orientation = (StackOrientation)newvalue;
                //view.RebuildLayout();
            }
        }

        /// <summary>
        /// The items source property
        /// </summary>
        public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(WrapGrid),
            null, BindingMode.OneWay, propertyChanged: OnItemsSourceChanged);

        /// <summary>
        /// Gets or sets the items source.
        /// </summary>
        /// <value>The items source.</value>
        public IEnumerable ItemsSource
        {
            get
            {
                return GetValue(WrapGrid.ItemsSourceProperty) as IEnumerable;
            }
            set
            {
                base.SetValue(WrapGrid.ItemsSourceProperty, value);
            }
        }
        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as WrapGrid;
            if (view != null)
            {
                if (oldvalue != null)
                    view.Unbind(oldvalue);
                if (newvalue != null)
                    view.Bind(newvalue);
                view.RebuildLayout();
            }
        }

        /// <summary>
        /// The item template property
        /// </summary>
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(WrapGrid),
            null, BindingMode.OneWay, propertyChanged: OnItemTemplateChanged);

        /// <summary>
        /// Gets or sets the item template.
        /// </summary>
        /// <value>The item template.</value>
        public DataTemplate ItemTemplate
        {
            get
            {
                return GetValue(ItemTemplateProperty) as DataTemplate;
            }
            set
            {
                SetValue(ItemTemplateProperty, value);
            }
        }

        private static void OnItemTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as WrapGrid;
            if (view != null)
            {
                view.RebuildLayout();
            }
        }

        private void Unbind(object items)
        {
            var notifyCollectionChanged = items as INotifyCollectionChanged;
            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged -= OnItemsCollectionChanged;
        }

        private void Bind(object items)
        {
            var notifyCollectionChanged = items as INotifyCollectionChanged;
            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged += OnItemsCollectionChanged;
        }

        void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Add new items to the layout
                var index = e.NewStartingIndex;
                foreach (var itemAdded in e.NewItems)
                {
                    _layout.Children.Insert(index++, CreateTemplatedView(itemAdded));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // remove the deleted items from the layout
                var index = e.OldStartingIndex;
                foreach (var itemRemoved in e.OldItems)
                {
                    var itemToRemove = _layout.Children.FirstOrDefault(view => view.BindingContext == itemRemoved);
                    if (itemToRemove != null)
                        _layout.Children.Remove(itemToRemove);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                // Remove all children
                _layout.Children.Clear();
            }
        }

        private View CreateTemplatedView(object item)
        {
            if (ItemTemplate == null)
            {
                return new Label
                {
                    Text = item.ToString(),
                    BindingContext = item
                };
            }

            var templateContent = ItemTemplate.CreateContent();
            var templatedItem = templateContent as Xamarin.Forms.ViewCell;
            templatedItem.View.BindingContext = item;

            return templatedItem.View;
        }

        private void RebuildLayout()
        {
            _layout.Children.Clear();
            if (ItemsSource == null)
                return;

            foreach (var item in ItemsSource)
            {
                var templatedItem = CreateTemplatedView(item);
                _layout.Children.Add(templatedItem);
            }
        }
    }
}
