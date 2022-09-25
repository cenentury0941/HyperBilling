using Google.Cloud.Firestore;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace HyperBilling;

public partial class OrderHistory : ContentPage
{
	public OrderHistory()
	{
		InitializeComponent();
	}

	private async void fetchorderbutton_Clicked(object sender, EventArgs e)
	{
		string orderid = OrderID.Text;

        FirestoreDb db = FirestoreDb.Create("xiaomihyperbilling");

        DocumentReference orderdb = db.Document( "orders/"+orderid );
        DocumentSnapshot ordersnap = await orderdb.GetSnapshotAsync();


        CollectionReference collection = db.Collection("customers");
        Query query = collection.WhereEqualTo("PhoneNo", ordersnap.GetValue<string>("CustomerID"));
		System.Diagnostics.Debug.WriteLine("---> "+ordersnap.GetValue<string>("CustomerID"));
        QuerySnapshot querySnapshot = await query.GetSnapshotAsync();


		Customer customer = new Customer();
		DocumentSnapshot customersnap = querySnapshot.First();
        customer.FirstName = customersnap.GetValue<string>("FirstName");
        customer.LastName = customersnap.GetValue<string>("LastName");
        customer.EmailID = customersnap.GetValue<string>("EmailID");
        customer.PhoneNo = customersnap.GetValue<string>("PhoneNo");
        customer.Address = customersnap.GetValue<string>("Address");

        OrderDetailsItem order = new OrderDetailsItem();

		order.Customer = customer;
		order.OrderID = orderid;
		order.CustomerID = ordersnap.GetValue<string>("CustomerID");
		order.Cost = ordersnap.GetValue<int>("Cost");
		order.Payment = ordersnap.GetValue<string>("Payment");
		order.Delivery = ordersnap.GetValue<string>("Delivery");
		order.Count = ordersnap.GetValue<int>("Count");

		order.Items = new ItemDetails[order.Count];

        CollectionReference itemscollection = db.Collection("orders/"+orderid+"/items");
		QuerySnapshot itemsquery = await itemscollection.GetSnapshotAsync();

		int index = 0;
		foreach( DocumentSnapshot data in itemsquery )
		{
			order.Items[index++] = ItemDetails.fromString( data.GetValue<string>("Item") );
		}


        await Navigation.PushAsync(new OrderDetails(order));

    }
}