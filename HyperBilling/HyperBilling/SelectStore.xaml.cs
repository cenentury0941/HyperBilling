namespace HyperBilling;

public partial class SelectStore : ContentPage
{
	public SelectStore()
	{
		InitializeComponent();
	}

	private async void qrbutton_Clicked(object sender, EventArgs e)
	{
		Dispatcher.Dispatch(async () => {
            var scanner = new ZXing.Mobile.MobileBarcodeScanner();
            var result = await scanner.Scan();
            if (result != null)
            {
                Console.WriteLine("Scanned Barcode: " + result.Text);
                StoreField.Text = result.Text;
                //GetParentWindow().Page = new Hub();
            }
        
        });

        
    }

	private void storebutton_Clicked(object sender, EventArgs e)
	{
        GetParentWindow().Page = new Hub();
    }
}