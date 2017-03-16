using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XLabs.Samples.Pages.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExtendedDatePickerPage : ContentPage
    {
        public ExtendedDatePickerPage()
        {
            InitializeComponent();
            ButtonNull.Clicked += delegate {
                DatePicker.Date = null;
            };
        }
    }
}
