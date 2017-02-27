
using Xamarin.Forms;
using System.ComponentModel;

namespace tabgrid
{
    public class tabgridview : ContentPage, INotifyPropertyChanged
    {
        StackLayout stack;

        public tabgridview()
        {
            var masterGrid = new Grid
            {
                WidthRequest = App.ScreenSize.Width,
                HeightRequest = App.ScreenSize.Height,
                ColumnDefinitions = new ColumnDefinitionCollection
                {
                    new ColumnDefinition {Width = App.ScreenSize.Width * .3},
                    new ColumnDefinition {Width = App.ScreenSize.Width * .3},
                    new ColumnDefinition {Width = App.ScreenSize.Width * .3}
                },
                ColumnSpacing = 2,
                RowDefinitions = new RowDefinitionCollection
                {
                    new RowDefinition {Height = 22},
                    new RowDefinition {Height = GridLength.Star}
                }
            };

            var lblTop = new Label
            {
                Text = "Tab one",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            var lblNext = new Label
            {
                Text = "Tab two",
                TextColor = Color.Red,
                HorizontalTextAlignment = TextAlignment.Center
            };

            stack = new StackLayout
            {
                WidthRequest = App.ScreenSize.Width,
                Orientation = StackOrientation.Vertical,
                Children = { SwapView(0) }
            };

            var lblTopTap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        stack.Children.RemoveAt(0);
                        stack.Children.Add(SwapView(0));
                        lblTop.BackgroundColor = Color.Red;
                        lblNext.BackgroundColor = Color.White;
                        lblTop.TextColor = Color.White;
                        lblNext.TextColor = Color.Red;
                    });
                })
            };

            var lblNextTap = new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        stack.Children.RemoveAt(0);
                        stack.Children.Add(SwapView(1));
                        lblTop.BackgroundColor = Color.White;
                        lblNext.BackgroundColor = Color.Red;
                        lblTop.TextColor = Color.Red;
                        lblNext.TextColor = Color.White;
                    });
                })
            };

            lblTop.GestureRecognizers.Add(lblTopTap);
            lblNext.GestureRecognizers.Add(lblNextTap);

            masterGrid.Children.Add(lblTop, 0, 0);
            masterGrid.Children.Add(lblNext, 1, 0);
            masterGrid.Children.Add(stack, 0, 1);
            Grid.SetColumnSpan(stack, 3);

            Content = masterGrid;
        }

        StackLayout SwapView(int view)
        {
            return new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Children =
                {
                    new Label
                    {
                        Text = string.Format("View = {0}", view),
                        FontSize = 16
                    }
                }
            };
        }
    }
}

