// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="ExtendedPickerPage.xaml.cs" company="XLabs Team">
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
using System.Windows.Input;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace XLabs.Samples.Pages.Controls
{	
	public partial class ExtendedPickerPage : ContentPage
	{
		public class DataClass
		{
			public string FirstName{get;set;}
			public string LastName{get;set;}
		}

		public ExtendedPickerPage ()
		{
			InitializeComponent ();
			BindingContext = this;
			myPicker = new ExtendedPicker (){DisplayProperty = "FirstName"};
			myPicker.SetBinding(ExtendedPicker.ItemsSourceProperty,new Binding("MyDataList",BindingMode.TwoWay));
			myPicker.SetBinding(ExtendedPicker.SelectedItemProperty,new Binding("TheChosenOne",BindingMode.TwoWay));
			myStackLayout.Children.Add (new Label{ Text = "Code Created:" });
			myStackLayout.Children.Add (myPicker);


			TheChosenOne = new DataClass(){ FirstName = "Jet", LastName = "Li" };
			MyDataList = new ObservableCollection<object> () {
				new DataClass(){FirstName="Jack",LastName="Doe"},
				TheChosenOne,
				new DataClass(){FirstName="Matt",LastName="Bar"},
				new DataClass(){FirstName="Mic",LastName="Jaggery"},
				new DataClass(){FirstName="Michael",LastName="Jackon"}
			};

		}
		public ICommand DoIt{ get; set; }

		private object chosenOne;
		public object TheChosenOne{ 
			get{ 
				return chosenOne; 
			} 
			set{
				chosenOne = value;
				OnPropertyChanged ("TheChosenOne");
			}
		}

		ObservableCollection<object> dataList; 
		public ObservableCollection<object> MyDataList {
			get{ return dataList; }
			set{
				dataList = value;
				OnPropertyChanged ("MyDataList");
			}
		}

	}
}

