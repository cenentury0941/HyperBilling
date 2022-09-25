using ZXing.Net.Maui;

namespace HyperBilling;

public partial class qrscan : ContentPage
{
    public static string qrread;

	public qrscan()
	{
		InitializeComponent();

        scan();

        System.Diagnostics.Debug.WriteLine( "Reached" );
    }


    public async void scan()
    {

        var scanner = new ZXing.Mobile.MobileBarcodeScanner();
        var result = await scanner.Scan();
        if (result != null) { 
            Console.WriteLine("Scanned Barcode: " + result.Text);
            qrread = result.Text; 
        }
        Navigation.PopAsync();
    }


    public void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        foreach (var barcode in e.Results)
            qrread = "barcode.Value";
        Navigation.PopAsync();
    }

}