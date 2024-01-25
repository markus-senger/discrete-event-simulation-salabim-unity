using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.DTOs
{
    public class AbstractDTO
    {
        public float x { get; set; }
        public float y { get; set; }

        protected AbstractDTO(float x, float y)
        {
            this.x = x;
            this.y = y; 
        }
    }
}
