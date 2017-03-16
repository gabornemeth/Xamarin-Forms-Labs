// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="CarouselSample.xaml.cs" company="XLabs Team">
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
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XLabs.Samples.ViewModel;

namespace XLabs.Samples.Pages.Controls
{
    public partial class CarouselSample  : ContentPage
    {
        public CarouselSample()
        {
            InitializeComponent();
            BindingContext = new CarouselVm();
        }
    }


    //Bad practice, only doing this for quick sample....

    public class CarouselVm : BaseViewModel
    {
        public CarouselVm()
        {
            Models=new ObservableCollection<PageWidget>
                {
                    new WorkOrder
                        {
                            Client = "John Smith",
                            Description = "Plumbing"
                        },
                    new Message
                        {
                            Sender = "Cindy",
                            Received = DateTime.Now,
                            Content = "John is getting flooded!!!"
                        },
                    new WorkOrder
                        {
                            Client = "Jane Jones",
                            Description = "Shoveling"
                        },
                    new Message
                        {
                            Sender = "Cindy",
                            Received = DateTime.Now,
                            Content = "Jane is snowed in"
                        },
                                            new WorkOrder
                        {
                            Client = "Dave Roberts",
                            Description = "Ghost writing"
                        },
                    new Message
                        {
                            Sender = "Cindy",
                            Received = DateTime.Now,
                            Content = "Dave is out of ideas"
                        },
                    new WorkOrder
                        {
                            Client = "George Henry",
                            Description = "Life advice"
                        },
                    new Message
                        {
                            Sender = "Cindy",
                            Received = DateTime.Now,
                            Content = "George isn't sure who he  is"
                        }



                };
        }

        public ObservableCollection<PageWidget> Models { get; set; }

        private PageWidget _SelectedModel;
        public PageWidget SelectedModel 
        {
            get { return _SelectedModel; }  
            set { SetField (ref _SelectedModel, value);  }
        }
    }

    public class PageWidget
    {
        public PageWidget(string title) { Title = title; }
        public string Title { get; private set; }
    }

    public class WorkOrder : PageWidget
    {
        public WorkOrder() : base("Work Order") { }
        public string Client { get; set; }
        public string Description { get; set; }
    }

    public class Message : PageWidget
    {
        public Message() : base("Message"){}
        public string Sender { get; set; }
        public DateTime Received { get; set; }
        public string Content { get; set; }
    }
}
