using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class ConnectionPointDTO : AbstractDTO
    {
        public float conveyor1Id { get; set; }
        public float conveyor2Id { get; set; }

        public ConnectionPointDTO(float x, float y, float rotation, float conveyor1Id, float conveyor2Id) : base(x, y, rotation)
        {
            this.conveyor1Id = conveyor1Id;
            this.conveyor2Id = conveyor2Id;
        }
    }
}
