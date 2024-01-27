using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class RemovalDTO : AbstractDTO
    {
        public string conveyorId { get; set; }
        public float removalDuration { get; set; }

        public RemovalDTO(float x, float y, float rotation, string conveyorId, float removalDuration) : base(x, y, rotation)
        {
            this.conveyorId = conveyorId;
            this.removalDuration = removalDuration;
        }
    }
}
