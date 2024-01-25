﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class SpawnerDTO : AbstractDTO
    {
        public float conveyorId { get; set; }
        public float spawnFrequency { get; set; }

        public SpawnerDTO(float x, float y, float rotation, float conveyorId, float spawnFrequency) : base(x, y, rotation)
        {
            this.conveyorId = conveyorId;
            this.spawnFrequency = spawnFrequency;
        }
    }
}
