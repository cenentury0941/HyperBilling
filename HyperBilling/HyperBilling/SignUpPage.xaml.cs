namespace HyperBilling;

public partial class SignUpPage : ContentPage
{
	public SignUpPage()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
	}

    private void OnSignUpClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync( new SelectStore() );
    }

}