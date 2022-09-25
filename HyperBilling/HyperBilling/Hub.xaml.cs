
using Microsoft.Maui.Controls;
using Application = Microsoft.Maui.Controls.Application;

namespace HyperBilling;

public partial class Hub : global::Microsoft.Maui.Controls.FlyoutPage
{
    public static Hub hub;
    public static OrderDetailsItem[] Staged;
    public static int Staged_Count;

	public Hub()
	{
        NavigationPage.SetHasNavigationBar(this, false);
        InitializeComponent();
        Flyoutpage.collectionView.SelectionChanged += OnSelectionChanged;
        hub = this;
        Staged = new OrderDetailsItem[0];
        Staged_Count = 0;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        if (item != null)
        {
            Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));
            try
            {
                IsPresented = false;
            }
            catch (Exception)
            { 
            }

        }
    }


    public void setFlyoutSelected( int index )
    { 
        Flyoutpage.collectionView.SelectedItem = Flyoutpage.GetItem(index);
    }




}