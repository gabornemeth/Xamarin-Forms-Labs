
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using XLabs.Data;

namespace XLabs.Samples.ViewModel
{
    /// <summary>
    /// For testing various bindings, scenarios
    /// </summary>
    class UniversalViewModel : Forms.Mvvm.ViewModel
    {
        private int _numberOfItems = 25;

        public int NumberOfItems
        {
            get => _numberOfItems;
            set
            {
                if (SetProperty(ref _numberOfItems, value))
                {
                    BuildItems();
                }
            }
        }


        private ObservableCollection<UniversalItem> _items = new ObservableCollection<UniversalItem>();

        public IList<UniversalItem> Items => _items;

        public UniversalViewModel()
        {
            BuildItems();
        }

        void BuildItems()
        {
            _items.Clear();
            for (var i = 0; i < _numberOfItems; i++)
            {
                Items.Add(new UniversalItem
                {
                    Text = $"Text {i + 1}",
                    BackgroundColor = Random.GetColor(),
                    Color = Random.GetColor(),
                    Size = Random.Base.Next(12, 20)
                });
            }
        }
    }

    class UniversalItem : ObservableObject
    {
        public string Text { get; set; }

        public Color BackgroundColor { get; set; }
        public Color Color { get; set; }

        public int Size { get; set; }
    }
}
