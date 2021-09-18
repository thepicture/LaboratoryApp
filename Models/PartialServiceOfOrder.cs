using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboratoryApp.Models
{
    public partial class ServiceOfOrder
    {
        /// <summary>
        /// Get service statuses from the DbContext.
        /// </summary>
        public List<ServiceStatus> GetServiceStatuses => AppContext.GetInstance().ServiceStatus.ToList();
    }
}
