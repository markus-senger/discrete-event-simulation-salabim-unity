using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class ComponentsDTO
    { 
        public Dictionary<float, ConveyorDTO> conveyorDTOs { get; set; }
        public Dictionary<float, ConnectionPointDTO> connectionPointDTOs { get; set; }
        public Dictionary<float, WTDTO> WTDTOs { get; set; }
        public Dictionary<float, SpawnerDTO> SpawnerDTOs { get; set; }
        public Dictionary<float, RemovalDTO> RemovalDTOs { get; set; }
        public Dictionary<float, WaitDTO> WaitDTOs { get; set; }
    }
}
