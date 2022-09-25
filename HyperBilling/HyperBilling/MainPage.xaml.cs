namespace HyperBilling;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        updateDBCreds();


    }

    private async void updateDBCreds()
    {
        var localPath = Path.Combine(FileSystem.CacheDirectory, "key.json");

        using var json = await FileSystem.OpenAppPackageFileAsync("Resources/key.json");
        using var dest = File.Create(localPath);
        await json.CopyToAsync(dest);
        dest.Close();

        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", localPath);
    }

	private void OnSignupClicked(object sender, EventArgs e)
	{
        Navigation.PushAsync(new SignUpPage());
    }

    private void OnLoginClicked(object sender, EventArgs e)
    {
		Navigation.PushAsync(new LoginPage());
    }
}

