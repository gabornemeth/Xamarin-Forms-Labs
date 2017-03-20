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
        private TestOrganization _organization = new TestOrganization();

        public WrapGridPage()
        {
            InitializeComponent();

            BindingContext = _organization;

            UpdateButtonOrientationText();
            ButtonOrientation.Clicked += delegate
            {
                MyGrid.Orientation = MyGrid.Orientation == StackOrientation.Horizontal ? StackOrientation.Vertical : StackOrientation.Horizontal;
                UpdateButtonOrientationText();
            };
        }

        void UpdateButtonOrientationText()
        {
            ButtonOrientation.Text = MyGrid.Orientation == StackOrientation.Horizontal ? "To vertical" : "To Horizontal";
        }
    }
}
