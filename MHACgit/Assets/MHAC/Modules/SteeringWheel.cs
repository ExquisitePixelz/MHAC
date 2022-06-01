using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.MHAC.Modules
{
    [System.Serializable]
    public class SteeringWheel : Module
    {
        public string Name = "SteeringWheel";

        public float Sensitivity;
        public int Xpos;
    }

}
