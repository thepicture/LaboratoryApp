using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryApp.Models
{
    public partial class ServiceOfOrder
    {
        public List<ServiceStatus> GetServiceStatuses => AppContext.GetInstance().ServiceStatus.ToList();
    }
}
