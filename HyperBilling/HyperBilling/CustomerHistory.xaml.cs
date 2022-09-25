using Google.Cloud.Firestore;
using static Google.Cloud.Firestore.V1.StructuredQuery.Types;

namespace HyperBilling;

public partial class CustomerHistory : ContentPage
{
    public OrderDetailsItem[] orderlist;


	public CustomerHistory()
	{
		InitializeComponent();
        ordersview.SelectionChanged += View_Order;
	}


    private async void View_Order(object sender, SelectionChangedEventArgs e)
    {
        System.Diagnostics.Debug.WriteLine("Clicked");
        OrderDetailsItem arg = null;
        foreach( OrderDetailsItem item in orderlist )
        {
            if( item.OrderID == (ordersview.SelectedItem as OrderDetailsItem).OrderID )
            {
                arg = item;
                break;
            }
        }
        await Navigation.PushAsync( new OrderDetails( arg ) );

    }



        private async void fetchorderbutton_Clicked(object sender, EventArgs e)
	{
		string customerid = CustomerID.Text;

        FirestoreDb db = FirestoreDb.Create("xiaomihyperbilling");

        CollectionReference orderscollection = db.Collection("orders");
        Query query = orderscollection.WhereEqualTo("CustomerID", customerid);

		QuerySnapshot orders = await query.GetSnapshotAsync();


        CollectionReference collection = db.Collection("customers");
        Query query2 = collection.WhereEqualTo("PhoneNo", customerid);
        QuerySnapshot querySnapshot = await query2.GetSnapshotAsync();


        Customer customer = new Customer();
        DocumentSnapshot customersnap = querySnapshot.First();
        customer.FirstName = customersnap.GetValue<string>("FirstName");
        customer.LastName = customersnap.GetValue<string>("LastName");
        customer.EmailID = customersnap.GetValue<string>("EmailID");
        customer.PhoneNo = customersnap.GetValue<string>("PhoneNo");
        customer.Address = customersnap.GetValue<string>("Address");



        OrderDetailsItem[] retrieved_orders = new OrderDetailsItem[orders.Count];

        int index = 0;
        foreach ( DocumentSnapshot ord in orders )
        {


            OrderDetailsItem order = new OrderDetailsItem();

            order.Customer = customer;
            order.OrderID = ord.Id;
            order.CustomerID = ord.GetValue<string>("CustomerID");
            order.Cost = ord.GetValue<int>("Cost");
            order.Payment = ord.GetValue<string>("Payment");
            order.Delivery = ord.GetValue<string>("Delivery");
            order.Count = ord.GetValue<int>("Count");
            order.Items = new ItemDetails[order.Count];

            CollectionReference itemscollection = db.Collection("orders/" + ord.Id + "/items");
            QuerySnapshot itemsquery = await itemscollection.GetSnapshotAsync();

            int index2 = 0;
            foreach (DocumentSnapshot data in itemsquery)
            {
                order.Items[index2++] = ItemDetails.fromString(data.GetValue<string>("Item"));
            }

            retrieved_orders[index++] = order;

        }


        ordersview.ItemsSource = retrieved_orders;
        orderlist = retrieved_orders;


    }
}