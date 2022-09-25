using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperBilling;
    public class ItemDetails
    {
    public int ItemID { get; set; }
    public string Name { get; set; }
    public int Cost { get; set; }

    public ItemDetails( int itemid , string name , int cost )
    {
        ItemID = itemid;
        Name = name;
        Cost = cost;
    }

    public static string asString(ItemDetails item)
    {
        string output = "";
        output = item.ItemID + "," + item.Name + "," + item.Cost;
        return output;
    }

    public static ItemDetails fromString(string input)
    {
        string[] arr = input.Split(",");
        int itemid = int.Parse(arr[0]);
        string name = arr[1];
        int cost = int.Parse(arr[2]);
        return new ItemDetails( itemid , name , cost );
  
    }


    }
