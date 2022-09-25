namespace HyperBilling;

public partial class PaymentInformation : ContentPage
{
	public PaymentInformation()
	{
		InitializeComponent();
	}

	private void card_Clicked(object sender, EventArgs e)
	{
		CreateBill.ORDER.Payment = "CARD";
		completepayment();
	}

	private void online_Clicked(object sender, EventArgs e)
    {
        CreateBill.ORDER.Payment = "ONLINE";
        completepayment();
    }

	private void gpay_Clicked(object sender, EventArgs e)
    {
        CreateBill.ORDER.Payment = "GPAY";
        completepayment();
    }

	private void paypal_Clicked(object sender, EventArgs e)
	{
        CreateBill.ORDER.Payment = "PAYPAL";
        completepayment();
    }

	private void cash_Clicked(object sender, EventArgs e)
    {
        CreateBill.ORDER.Payment = "CASH";
        completepayment();
    }

	private void completepayment()
	{
		Navigation.PushAsync(new OrderCompletion());
	}

}