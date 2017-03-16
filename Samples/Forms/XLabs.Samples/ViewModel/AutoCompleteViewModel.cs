// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="AutoCompleteViewModel.cs" company="XLabs Team">
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

using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using XLabs.Samples.Model;

namespace XLabs.Samples.ViewModel
{
    /// <summary>
    /// The main view model.
    /// </summary>
    public class AutoCompleteViewModel : Forms.Mvvm.ViewModel
    {
        private ObservableCollection<TestPerson> _items;
        private Command<string> _searchCommand;
        private Command<TestPerson> _cellSelectedCommand;
        private TestPerson _selectedItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoCompleteViewModel"/> class.
        /// </summary>
        public AutoCompleteViewModel()
        {
            Items = new ObservableCollection<TestPerson>();
            for (var i = 0; i < 10; i++)
            {
                Items.Add(new TestPerson
                {
                    FirstName = string.Format("FirstName {0}", i),
                    LastName = string.Format("LastName {0}", i)
                });
            }
        }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public ObservableCollection<TestPerson> Items
        {
            get
            {
                return _items;
            }
            set
            {
                SetProperty(ref _items, value);
            }
        }

        /// <summary>
        /// Gets the selected cell command.
        /// </summary>
        /// <value>
        /// The selected cell command.
        /// </value>
        public Command<TestPerson> CellSelectedCommand
        {
            get
            {
                return _cellSelectedCommand ?? (_cellSelectedCommand = new Command<TestPerson>(parameter => Debug.WriteLine(parameter.FirstName + parameter.LastName + parameter.Age)));
            }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>
        /// The search command.
        /// </value>
        public Command<string> SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new Command<string>(
                    obj => { },
                    obj => !string.IsNullOrEmpty(obj.ToString())));
            }
        }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        /// <value>The selected item.</value>
        public TestPerson SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                SetProperty(ref _selectedItem, value);
            }
        }
    }
}
