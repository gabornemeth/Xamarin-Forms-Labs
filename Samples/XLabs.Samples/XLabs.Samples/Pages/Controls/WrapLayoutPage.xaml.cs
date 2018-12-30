using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XLabs.Samples.ViewModel;

namespace XLabs.Samples.Pages.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WrapLayoutPage : ContentPage
    {
        public Forms.Mvvm.ViewModel ViewModel { get; } = new UniversalViewModel();

        public static readonly BindableProperty SpacingProperty = BindableProperty.Create(nameof(Spacing), typeof(int), typeof(WrapLayoutPage), 10);

        public int Spacing
        {
            get => (int)GetValue(SpacingProperty);
            set => SetValue(SpacingProperty, value);
        }

        public WrapLayoutPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;

            for (var i = 0; i < 30; i++)
            {
                var labelToAdd = new Label
                {
                    Text = $"Label #{i + 1}",
                    BackgroundColor = Random.GetColor(),
                    TextColor = Random.GetColor()
                };
                Layout.Children.Add(labelToAdd);
            }
        }
    }
}