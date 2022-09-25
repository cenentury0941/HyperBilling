namespace HyperBilling;

public partial class CreateBill : ContentPage
{

	public static OrderDetailsItem ORDER;

	public CreateBill()
	{
		InitializeComponent();
		ORDER = new OrderDetailsItem();
	}

	private void createorderbutton_Clicked(object sender, EventArgs e)
	{
		Navigation.PushAsync( new AddItems() );
	}
}