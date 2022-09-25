
namespace HyperBilling;

using Google.Cloud.Firestore;
using System.Collections;
using System.Linq;

public partial class AddItems : ContentPage
{
	public ItemDetails[] Cart, Display;
	public int count;
	public ItemDetails[] ItemDB;
	public int cost;

	public AddItems()
	{
		InitializeComponent();
        updateItems();
		Cart = new ItemDetails[100];
		count = 0;
		cost = 0;
		Cost.Text = "Total : " + cost;
		updateDisplay();
    }

	public void updateDisplay()
	{

        Display = new ItemDetails[count];
        for (int i = 0; i < count; i++)
        {
            Display[i] = Cart[i];
        }
		cartitems.ItemsSource = Display;
    
	}

	private void proceedbutton_Clicked(object sender, EventArgs e)
	{
		CreateBill.ORDER.Items = Cart;
		CreateBill.ORDER.Count = count;
		CreateBill.ORDER.Cost = cost;
		Navigation.PushAsync( new CustomerDetails() );
	}

	public async void updateItems()
	{


        FirestoreDb db = FirestoreDb.Create("xiaomihyperbilling");

        QuerySnapshot snapshot = await db.Collection("Items").GetSnapshotAsync();
		ItemDB = new ItemDetails[ snapshot.Count ];
		int index = 0;
		foreach ( DocumentSnapshot snap in snapshot )
		{
			System.Diagnostics.Debug.WriteLine(snap.Id);
			ItemDB[index++] = new ItemDetails( int.Parse(snap.Id) , snap.GetValue<string>("Name") , snap.GetValue<int>("Cost"));
			
		}
    }

	private async void scanqrbutton_Clicked(object sender, EventArgs e)
	{
        var scanner = new ZXing.Mobile.MobileBarcodeScanner();
        var result = await scanner.Scan();
        if (result != null)
        {
            Console.WriteLine("Scanned Barcode: " + result.Text);
            ItemID.Text = result.Text;
			additembutton_Clicked(null,null);
        }
        //Navigation.PushAsync( new qrscan() );
        //Application.Current.MainPage = new NavigationPage( new qrscan() );

    }

	private void additembutton_Clicked(object sender, EventArgs e)
	{

		int id = int.Parse( ItemID.Text )-1;
		Cart[count++] = new ItemDetails( ItemDB[id].ItemID , ItemDB[id].Name , ItemDB[id].Cost );
        cost += ItemDB[id].Cost;
        Cost.Text = "Total : " + cost;
        updateDisplay();
	}
}