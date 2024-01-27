using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class ComponentsDTO
    { 
        public Dictionary<string, ConveyorDTO> conveyorDTOs { get; set; }
        public Dictionary<string, ConnectionPointDTO> connectionPointDTOs { get; set; }
        public Dictionary<string, WTDTO> WTDTOs { get; set; }
        public Dictionary<string, SpawnerDTO> SpawnerDTOs { get; set; }
        public Dictionary<string, RemovalDTO> RemovalDTOs { get; set; }
        public Dictionary<string, WaitDTO> WaitDTOs { get; set; }
    }
}
