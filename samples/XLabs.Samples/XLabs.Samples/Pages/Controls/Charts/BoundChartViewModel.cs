// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="BoundChartViewModel.cs" company="XLabs Team">
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace XLabs.Samples.Pages.Controls.Charts
{
	/// <summary>
	/// Class BoundChartViewModel.
	/// </summary>
	public class BoundChartViewModel : INotifyPropertyChanged
	{
		#region commands
		/// <summary>
		/// Gets or sets the change color yellow command.
		/// </summary>
		/// <value>The change color yellow command.</value>
		public ICommand ChangeColorYellowCommand { get; set; }
		/// <summary>
		/// Gets or sets the change color green command.
		/// </summary>
		/// <value>The change color green command.</value>
		public ICommand ChangeColorGreenCommand { get; set; }
		/// <summary>
		/// Gets or sets the change color white command.
		/// </summary>
		/// <value>The change color white command.</value>
		public ICommand ChangeColorWhiteCommand { get; set; }
		/// <summary>
		/// Gets or sets the change source command.
		/// </summary>
		/// <value>The change source command.</value>
		public ICommand ChangeSourceCommand { get; set; }
		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="BoundChartViewModel"/> class.
		/// </summary>
		public BoundChartViewModel()
		{
			#region commands
			ChangeColorYellowCommand = new Command<string>((color) =>
			{
				this.Color = Xamarin.Forms.Color.Yellow;
			},
			(color) =>
			{
				return Color != Color.Yellow;
			});
			ChangeColorGreenCommand = new Command<string>((color) =>
			{
				this.Color = Xamarin.Forms.Color.Green;
			},
			(color) =>
			{
				return Color != Color.Green;
			});
			ChangeColorWhiteCommand = new Command<string>((color) =>
			{
				this.Color = Xamarin.Forms.Color.White;
			},
			(color) =>
			{
				return Color != Color.White;
			});

			ChangeSourceCommand = new Command(() =>
			{
				this.ChartData = GetChartData();
			},
			() =>
			{
				return true;
			});
			#endregion

			Color = Color.Yellow;

			this.ChartData = GetChartData();

		}

		/// <summary>
		/// Gets the chart data.
		/// </summary>
		/// <returns>List&lt;List&lt;Tuple&lt;System.String, System.Double&gt;&gt;&gt;.</returns>
		private List<List<Tuple<string, double>>> GetChartData()
		{
			var rnd = new System.Random();

			var seriesValues1 = new List<Tuple<string, double>>()
			{
				new Tuple<string, double>("Jan",   rnd.Next(0,100)),
				new Tuple<string, double>("Feb",   rnd.Next(0,100)),
				new Tuple<string, double>("March", rnd.Next(0,100))
			};
			var seriesValues2 = new List<Tuple<string, double>>()
			{
				new Tuple<string, double>("Jan",   rnd.Next(0,100)),
				new Tuple<string, double>("Feb",   rnd.Next(0,100)),
				new Tuple<string, double>("March", rnd.Next(0,100))
			};
			var seriesValues3 = new List<Tuple<string, double>>()
			{
				new Tuple<string, double>("Jan",   rnd.Next(0,100)),
				new Tuple<string, double>("Feb",   rnd.Next(0,100)),
				new Tuple<string, double>("March", rnd.Next(0,100))
			};

			return new List<List<Tuple<string, double>>>()
			{
				seriesValues1, seriesValues2, seriesValues3
			};
		}

		/// <summary>
		/// The _color
		/// </summary>
		private Color _color;
		/// <summary>
		/// Gets or sets the color.
		/// </summary>
		/// <value>The color.</value>
		public Color Color
		{
			get
			{
				return _color;
			}
			set
			{
				if (_color != value)
				{
					_color = value;
					((Command)this.ChangeColorYellowCommand).ChangeCanExecute();
					((Command)this.ChangeColorGreenCommand).ChangeCanExecute();
					((Command)this.ChangeColorWhiteCommand).ChangeCanExecute();
					OnPropertyChanged("Color");
				}
			}
		}

		/// <summary>
		/// The _chart data
		/// </summary>
		private List<List<Tuple<string, double>>> _chartData;
		/// <summary>
		/// Gets or sets the chart data.
		/// </summary>
		/// <value>The chart data.</value>
		public List<List<Tuple<string, double>>> ChartData
		{
			get
			{
				return _chartData;
			}
			set
			{
				_chartData = value;
				OnPropertyChanged("ChartData");
			}
		}

		/// <summary>
		/// Occurs when [property changed].
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Called when [property changed].
		/// </summary>
		/// <param name="propertyName">Name of the property.</param>
		protected void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
		}
	}
}
