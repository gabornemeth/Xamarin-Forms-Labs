using Xamarin.Forms;
using XLabs.Enums;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace XLabs.Samples.Pages.Samples
{
    public class OrientationPage : ContentPage
    {
        public OrientationPage()
        {
            var rl = new RelativeLayout();

            var box1 = new BoxView {BackgroundColor = Color.Aqua};
            var box2 = new BoxView { BackgroundColor = Color.Lime };
            var device = Resolver.Resolve<IDevice>();

            rl.Children.Add(
                box1,
                Constraint.Constant(0),
                Constraint.Constant(0),
                Constraint.RelativeToParent(p => device.Orientation.HasFlag(Orientation.Portrait) ? p.Width / 2 : p.Width),
                Constraint.RelativeToParent(p => device.Orientation.HasFlag(Orientation.Portrait) ? p.Height : p.Height / 2));

            rl.Children.Add(
                box2,
                Constraint.RelativeToParent(p => device.Orientation.HasFlag(Orientation.Portrait) ? p.Width / 2 : 0),
                Constraint.RelativeToParent(p => device.Orientation.HasFlag(Orientation.Portrait) ? 0 : p.Height / 2),
                Constraint.RelativeToParent(p => device.Orientation.HasFlag(Orientation.Portrait) ? p.Width / 2 : p.Width),
                Constraint.RelativeToParent(p => device.Orientation.HasFlag(Orientation.Portrait) ? p.Height : p.Height / 2));

            this.Content = rl;
        }
    }
}