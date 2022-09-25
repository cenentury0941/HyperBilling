using Google.Apis.Util;
using Google.Cloud.Firestore;

namespace HyperBilling;

public partial class Staging : ContentPage
{
	public OrderDetailsItem[] Items { get; set; }

	public Staging()
	{
		InitializeComponent();

		Items = new OrderDetailsItem[1];
		Items[0] = new OrderDetailsItem();
		Items[0].OrderID = "YF9zrrF9t5ugzGmrtv1t";
		Items[0].CustomerID = "9481103051";
		orders.ItemsSource = Hub.Staged;

		orders.SelectionChanged += ImageButton_Clicked;

    }

	private async void ImageButton_Clicked(object sender, SelectionChangedEventArgs e)
	{
        Loading.IsVisible = true;

        OrderDetailsItem order = orders.SelectedItem as OrderDetailsItem;
        System.Diagnostics.Debug.WriteLine("clicked " + order.OrderID);

        Customer customer = order.Customer;

        FirestoreDb db = FirestoreDb.Create("xiaomihyperbilling");

        CollectionReference collectioncustomer = db.Collection("customers");
        Query query = collectioncustomer.WhereEqualTo("PhoneNo", customer.PhoneNo);
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();

        if (querySnapshot.Count == 0)
        {

            DocumentReference documentcustomer = await collectioncustomer.AddAsync(new
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                PhoneNo = customer.PhoneNo,
                Address = customer.Address,
                EmailID = customer.EmailID

            });
        }

        CollectionReference collectionorders = db.Collection("orders");
        DocumentReference documentorder = await collectionorders.AddAsync(new
        {
            CustomerID = customer.PhoneNo,
            Cost = order.Cost,
            Delivery = order.Delivery,
            Payment = order.Payment,
            Count = order.Count
        });

        CollectionReference itemscollection = documentorder.Collection("items");

        for (int i = 0; i < order.Count; i++)
        {
            ItemDetails item = order.Items[i];
            await itemscollection.AddAsync(new
            {
                Item = ItemDetails.asString(item)
            });
        }

        OrderDetailsItem[] New_Staged = new OrderDetailsItem[Hub.Staged_Count-1];
        int index = 0;
        foreach( OrderDetailsItem _order in Hub.Staged )
        {
            if (_order.OrderID == order.OrderID)
            {
                continue;
            }
            New_Staged[index++] = _order;
        }
        orders.ItemsSource = New_Staged;
        Hub.Staged = New_Staged;
        Hub.Staged_Count -= 1;
        Loading.IsVisible = false;


    }
}