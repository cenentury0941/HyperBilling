using Google.Cloud.Firestore;

namespace HyperBilling;

public partial class OrderCompletion : ContentPage
{
	public OrderCompletion()
	{
		InitializeComponent();

		postBill();

	}

	private void stagedbutton_Clicked(object sender, EventArgs e)
	{

	}

    protected override bool OnBackButtonPressed()
    {
		Hub.hub.setFlyoutSelected(1);
        return true;
    }

	public async void postBill()
	{
        OrderDetailsItem order = CreateBill.ORDER;
        Customer customer = order.Customer;

        try
        {
            
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


            Loading.IsVisible = false;
            Staged.IsVisible = false;
            Success.IsVisible = true;
        }
        catch(Exception)
        {

            OrderDetailsItem[] New_Items = new OrderDetailsItem[Hub.Staged_Count+1];
            for (int i = 0; i < Hub.Staged_Count; i++)
            {
                New_Items[i] = Hub.Staged[i];
            }
            New_Items[Hub.Staged_Count++] = order;
            Hub.Staged = New_Items;


            Loading.IsVisible = false;
            Staged.IsVisible = true;
            Success.IsVisible = false;

        }

    }

}