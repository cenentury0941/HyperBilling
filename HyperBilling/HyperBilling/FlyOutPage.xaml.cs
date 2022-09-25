namespace HyperBilling;

public partial class FlyOutPage : ContentPage
{
	FlyoutPageItem[] items = new FlyoutPageItem[5];

	public FlyOutPage()
	{
		InitializeComponent();
		int index = 0;
		foreach ( FlyoutPageItem item in collectionView.ItemsSource )
		{
			items[index++] = item;
		}
	}

	public FlyoutPageItem GetItem(int index)
	{
		return items[index]; //items.GetValue(index) as FlyoutPageItem;
	}




}