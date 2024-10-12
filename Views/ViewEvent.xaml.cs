using EScanner.Models;
namespace EScanner.Views;

public partial class ViewEvent : ContentPage
{
    Event _event = new Event();
	public ViewEvent()
	{
		InitializeComponent();
       
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await FillList();
    }
    private async Task FillList()
    {
        ListofEvent.ItemsSource = await _event.GetEvent();
    }


    private async void itemedititstudent_Invoked_1(object sender, EventArgs e)
    {
        var item = sender as SwipeItemView;
        if (item == null) return;
        if (item.BindingContext is Event details)
        {

            var a = await _event.GetEvents(details.EventCode);
            await Navigation.PushAsync(new ViewEDetail());

        }
    }

    private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        var item = sender as Border;
        if (item == null) return;
        if (item.BindingContext is Event details)
        {

            var a = await _event.GetEvents(details.EventCode);

            Application.Current!.MainPage = new ScanPage();
           

        }
    }
}