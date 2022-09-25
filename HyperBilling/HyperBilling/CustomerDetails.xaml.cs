using System.Net.Mail;

namespace HyperBilling;

public partial class CustomerDetails : ContentPage
{
	public Customer customer;

	public CustomerDetails()
	{
		InitializeComponent();
		qty.SelectedIndex = 0;
		customer = new Customer();
	}

	private void proceedbutton_Clicked(object sender, EventArgs e)
	{

		int datapoints = 0;
		if ( PhoneNo.Text.Length > 0 )
		{ 
			customer.PhoneNo = PhoneNo.Text;
			datapoints += 1;
		}

		if( FirstName.Text.Length > 0 )
		{
			customer.FirstName = FirstName.Text;
			datapoints += 1;
		}

		if( LastName.Text.Length > 0 )
		{
			customer.LastName = LastName.Text;
			datapoints += 1;
		}

		if ( Address.Text.Length > 0 )
		{ 
			customer.Address = Address.Text;
			datapoints += 1;
		}

		if (EmailID.Text.Length > 0)
		{ 
			customer.EmailID = EmailID.Text;
			datapoints += 1;
		}

		CreateBill.ORDER.Delivery = qty.SelectedItem as string;
		CreateBill.ORDER.Customer = customer;

		if (datapoints == 5)
		{
			Navigation.PushAsync(new PaymentInformation());
		}
	}
}