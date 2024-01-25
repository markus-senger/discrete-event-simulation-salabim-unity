using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class WTDTO : AbstractDTO
    {
        public float conveyorId { get; set; }

        public WTDTO(float x, float y, float rotation, float conveyorId) : base(x, y, rotation)
        {
            this.conveyorId = conveyorId;
        }
    }
}
