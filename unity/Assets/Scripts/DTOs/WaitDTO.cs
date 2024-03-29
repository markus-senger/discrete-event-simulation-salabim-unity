﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class WaitDTO : AbstractDTO
    {
        public string conveyorId { get; set; }
        public float waitDuration { get; set; }

        public WaitDTO(float x, float y, float rotation, string conveyorId, float waitDuration) : base(x, y, rotation)
        {
            this.conveyorId = conveyorId;
            this.waitDuration = waitDuration;
        }
    }
}
