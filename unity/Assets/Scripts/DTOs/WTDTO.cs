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

        public WTDTO(float x, float y, float conveyorId) : base(x, y)
        {
            this.conveyorId = conveyorId;
        }
    }
}
