namespace HyperBilling;

public partial class OrderDetails : ContentPage
{
	public OrderDetails( OrderDetailsItem order )
	{
		InitializeComponent();

		CustomerID.Text = order.CustomerID;
		CustomerName.Text = order.Customer.FirstName + " " + order.Customer.LastName;
		CustomerEmail.Text = order.Customer.EmailID;
		CustomerAddress.Text = order.Customer.Address;
		Cost.Text = "Total : " + order.Cost;
		OrderCount.Text = ""+order.Count;
		items.ItemsSource = order.Items;

	}

    protected override bool OnBackButtonPressed()
    {

		Navigation.PopAsync();
        return false;
    }

}