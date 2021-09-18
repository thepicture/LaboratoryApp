using System.Linq;

namespace LaboratoryApp.Models
{
    public partial class Order
    {
        public string GetPriceSum => ServiceOfOrder.ToList().Sum(s => s.Service.Price).ToString("N2");
    }
}
