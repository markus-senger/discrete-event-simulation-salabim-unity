using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class ConnectionPointDTO : AbstractDTO
    {
        public string conveyor1Id { get; set; }
        public string conveyor2Id { get; set; }

        public ConnectionPointDTO(float x, float y, float rotation, string conveyor1Id, string conveyor2Id) : base(x, y, rotation)
        {
            this.conveyor1Id = conveyor1Id;
            this.conveyor2Id = conveyor2Id;
        }
    }
}
