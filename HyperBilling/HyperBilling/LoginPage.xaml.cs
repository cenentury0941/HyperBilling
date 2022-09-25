namespace HyperBilling;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		NavigationPage.SetHasNavigationBar(this, false);
		NavigationPage.SetIconColor(this, new Color(255, 255, 255));
		InitializeComponent();
	}

	private void OnLoginClicked(object sender, EventArgs e)
	{

		Navigation.PushAsync(new SelectStore());

    }

}