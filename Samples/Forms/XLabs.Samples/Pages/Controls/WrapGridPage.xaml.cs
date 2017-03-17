using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Samples.Model;

namespace XLabs.Samples.Pages.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WrapGridPage : ContentPage
    {
        private IList<TestPerson> _persons;
        private int _counter = 1;
        private Random _random = new Random();

        public WrapGridPage()
        {
            InitializeComponent();

            _persons = new ObservableCollection<TestPerson>();
            BindingContext = _persons;

            ButtonAdd.Clicked += delegate
            {
                _persons.Add(new TestPerson
                {
                    Age = _random.Next(20, 80),
                    FirstName = $"Joe #{_counter++}",
                    LastName = "Average"
                });
            };
            ButtonRemove.Clicked += delegate
            {
                if (_persons.Count > 0)
                    _persons.RemoveAt(0);
            };
        }
    }
}
