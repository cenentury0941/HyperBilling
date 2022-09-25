
namespace HyperBilling;

public partial class DashboardPage : ContentPage
{

	public DashboardPage()
	{
		InitializeComponent();
	}

    private void createbillbutton_Clicked(object sender, EventArgs e)
    {
        Hub.hub.setFlyoutSelected(1);
        System.Diagnostics.Debug.WriteLine("Clicked");
    }

    private void staginganddispatchbutton_Clicked(object sender, EventArgs e)
    {
        Hub.hub.setFlyoutSelected(2);

    }

    private void orderhistorybutton_Clicked(object sender, EventArgs e)
    {
        Hub.hub.setFlyoutSelected(3);

    }

    private void customerhistorybutton_Clicked(object sender, EventArgs e)
    {
        Hub.hub.setFlyoutSelected(4);

    }
}