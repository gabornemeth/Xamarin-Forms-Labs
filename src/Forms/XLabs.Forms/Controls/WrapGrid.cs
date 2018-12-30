// ***********************************************************************
// Assembly         : XLabs.Forms
// Author           : XLabs Team
// Created          : 03-17-2017
// 
// Last Modified By : XLabs Team
// Last Modified On : 03-21-2017
// ***********************************************************************
// <copyright file="WrapGrid.cs" company="XLabs Team">
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
    /// Templateable list view that arranges items using <see cref="WrapLayout"/>.
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

        #region Grouping

        /// <summary>
        /// The IsGroupingEnabled dependency property
        /// </summary>
        public static readonly BindableProperty IsGroupingEnabledProperty = BindableProperty.Create(nameof(IsGroupingEnabled), typeof(bool), typeof(WrapGrid),
            false, BindingMode.OneWay, propertyChanged: OnIsGroupingEnabledChanged);

        /// <summary>
        /// Gets or sets whether grouping is enabled
        /// </summary>
        /// <value>The items source.</value>
        public bool IsGroupingEnabled
        {
            get
            {
                return (bool)GetValue(IsGroupingEnabledProperty);
            }
            set
            {
                base.SetValue(IsGroupingEnabledProperty, value);
            }
        }

        private static void OnIsGroupingEnabledChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as WrapGrid;
            if (view != null)
            {
                // grouping is switched on or off, let's rebuild the whole layout
                view.RebuildLayout();
            }
        }

        /// <summary>
        /// The group header template property
        /// </summary>
        public static readonly BindableProperty GroupHeaderTemplateProperty = BindableProperty.Create(nameof(GroupHeaderTemplate), typeof(DataTemplate), typeof(WrapGrid),
            null, BindingMode.OneWay, propertyChanged: OnGroupHeaderTemplateChanged);

        /// <summary>
        /// Gets or sets the group header template.
        /// </summary>
        /// <value>The group header template.</value>
        public DataTemplate GroupHeaderTemplate
        {
            get
            {
                return GetValue(GroupHeaderTemplateProperty) as DataTemplate;
            }
            set
            {
                SetValue(GroupHeaderTemplateProperty, value);
            }
        }

        private static void OnGroupHeaderTemplateChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as WrapGrid;
            if (view != null)
            {
                view.RebuildLayout();
            }
        }

        #endregion

        #region Orientation property

        /// <summary>
        /// The Orientation property
        /// </summary>
        public static readonly BindableProperty OrientationProperty = BindableProperty.Create(nameof(Orientation), typeof(StackOrientation), typeof(WrapGrid),
            StackOrientation.Horizontal, BindingMode.OneWay, propertyChanged: OnOrientationChanged);

        /// <summary>
        /// Gets or sets the orientation.
        /// </summary>
        /// <value>The orientation.</value>
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
            }
        }

        #endregion

        #region ItemSpacing property

        public static readonly BindableProperty ItemSpacingProperty = BindableProperty.Create(nameof(ItemSpacing),
            typeof(double), typeof(WrapGrid), 5.0, propertyChanged: OnItemSpacingChanged);

        public double ItemSpacing
        {
            get => (double)GetValue(ItemSpacingProperty);
            set => SetValue(ItemSpacingProperty, value);
        }

        private static void OnItemSpacingChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if (bindable is WrapGrid view)
            {
                view._layout.Spacing = (double)newvalue;
            }
        }

        #endregion

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
                return GetValue(ItemsSourceProperty) as IEnumerable;
            }
            set
            {
                base.SetValue(ItemsSourceProperty, value);
            }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var view = bindable as WrapGrid;
            if (view != null)
            {
                if (oldvalue != null)
                    view.Unbind(oldvalue as IEnumerable);
                if (newvalue != null)
                    view.Bind(newvalue as IEnumerable);
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

        private void Unbind(IEnumerable items, NotifyCollectionChangedEventHandler handler)
        {
            if (items == null)
                return;

            var notifyCollectionChanged = items as INotifyCollectionChanged;
            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged -= handler;
        }

        private void Bind(IEnumerable items, NotifyCollectionChangedEventHandler handler)
        {
            if (items == null)
                return;

            var notifyCollectionChanged = items as INotifyCollectionChanged;
            if (notifyCollectionChanged != null)
                notifyCollectionChanged.CollectionChanged += handler;
        }

        private void Unbind(IEnumerable items)
        {
            Unbind(items, OnItemsCollectionChanged);
            if (IsGroupingEnabled)
            {
                foreach (var item in items)
                    Unbind(item as IEnumerable, OnGroupItemCollectionChanged);
            }
        }

        private void Bind(IEnumerable items)
        {
            Bind(items, OnItemsCollectionChanged);
            if (IsGroupingEnabled)
            {
                foreach (var item in items)
                    Bind(item as IEnumerable, OnGroupItemCollectionChanged);
            }
        }

        /// <summary>
        /// Removes the view corresponding to the item.
        /// </summary>
        /// <param name="item">The item, the belonging view has to be removed.</param>
        private void RemoveViewOfItem(object item)
        {
            var itemToRemove = _layout.Children.FirstOrDefault(view => view.BindingContext == item);
            if (itemToRemove != null)
                _layout.Children.Remove(itemToRemove);
        }

        /// <summary>
        /// Determines the index where a new group's item has to be inserted into the layout
        /// </summary>
        /// <param name="item">The new group item.</param>
        /// <returns></returns>
        private int GetIndexForViewOfNewGroup(object item)
        {
            var enumerator = ItemsSource.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == item)
                {
                    if (enumerator.MoveNext())
                    {
                        return GetIndexOfViewForItem(enumerator.Current);
                    }
                }
            }

            return _layout.Children.Count;
        }

        void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // Add new items to the layout
                foreach (var itemAdded in e.NewItems)
                {
                    var index = GetIndexForViewOfNewGroup(itemAdded);
                    _layout.Children.Insert(index, CreateTemplatedView(itemAdded, IsGroupingEnabled ? GroupHeaderTemplate : ItemTemplate));
                    if (IsGroupingEnabled)
                    {
                        AddGroupItems(itemAdded);
                        Bind(itemAdded as IEnumerable, OnGroupItemCollectionChanged);
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // remove the deleted items from the layout
                var index = e.OldStartingIndex;
                foreach (var itemRemoved in e.OldItems)
                {
                    RemoveViewOfItem(itemRemoved);
                    if (IsGroupingEnabled)
                    {
                        foreach (var child in itemRemoved as IEnumerable)
                        {
                            RemoveViewOfItem(child);
                        }

                        // Unbind CollectionChanged event handler
                        Unbind(itemRemoved as IEnumerable, OnGroupItemCollectionChanged);
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                if (IsGroupingEnabled)
                {
                    throw new Exception("Event handlers cannot be unhooked properly, please remove items one by one.");
                }

                // Remove all children
                _layout.Children.Clear();
            }
        }

        private View GetViewOfItem(object item)
        {
            return _layout.Children.FirstOrDefault(child => child.BindingContext == item);
        }

        private int GetIndexOfViewForItem(object item)
        {
            var viewGroupHeader = GetViewOfItem(item);
            return _layout.Children.IndexOf(viewGroupHeader);
        }

        private void AddGroupItems(object group)
        {
            foreach (var itemInGroup in group as IEnumerable)
            {
                var templatedItemInGroup = CreateTemplatedView(itemInGroup, ItemTemplate);
                _layout.Children.Add(templatedItemInGroup);
            }
        }

        void OnGroupItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                var idx = e.NewStartingIndex + GetIndexOfViewForItem(sender) + 1;
                // Add new items to the layout
                foreach (var itemAdded in e.NewItems)
                {
                    _layout.Children.Insert(idx++, CreateTemplatedView(itemAdded, ItemTemplate));
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                // remove the deleted items from the layout
                foreach (var itemRemoved in e.OldItems)
                {
                    var itemToRemove = _layout.Children.FirstOrDefault(view => view.BindingContext == itemRemoved);
                    if (itemToRemove != null)
                        _layout.Children.Remove(itemToRemove);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                if (IsGroupingEnabled)
                {
                    throw new Exception("Event handlers cannot be unhooked properly, please remove items one by one.");
                }
            }
        }

        private View CreateTemplatedView(object item, DataTemplate template)
        {
            if (template == null)
            {
                return new Label
                {
                    Text = item.ToString(),
                    BindingContext = item
                };
            }

            var templatedView = template.CreateContent() as View;
            if (templatedView == null)
                throw new NotSupportedException("Only View based templates are supported.");
            templatedView.BindingContext = item;

            return templatedView;
        }

        private void RebuildLayout()
        {
            _layout.Children.Clear();
            if (ItemsSource == null)
                return;

            foreach (var item in ItemsSource)
            {
                var templatedItem = CreateTemplatedView(item, IsGroupingEnabled ? GroupHeaderTemplate : ItemTemplate);
                _layout.Children.Add(templatedItem);
                if (IsGroupingEnabled)
                {
                    // parse items inside the group
                    // group has to be IEnumerable
                    foreach (var itemInGroup in item as IEnumerable)
                    {
                        var templatedItemInGroup = CreateTemplatedView(itemInGroup, ItemTemplate);
                        _layout.Children.Add(templatedItemInGroup);
                    }
                }
            }
        }

    }
}
