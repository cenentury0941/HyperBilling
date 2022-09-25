using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperBilling;

    public class OrderDetailsItem
    {

    public ItemDetails[] Items { get; set; }
    public int Count { get; set; }
    public string OrderID { get; set; }
    public Customer Customer { get; set; }
    public string CustomerID { get; set; }
    public int Cost { get; set; }
    public string Delivery { get; set; }
    public string Payment { get; set; }

    }

