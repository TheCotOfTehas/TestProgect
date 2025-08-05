using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementApplication
{
    public class ResourceShipment
    {
        [Required]
        public Guid Id { get; set; }

        public Guid ResourceId { get; set; }
        public Guid UnitId { get; set; }
        public double Amount { get; set; }

        // Навигационные свойства
        public Resource Resource { get; set; }
        public UnitMeasurement UnitMeasurement { get; set; }
    }
}
