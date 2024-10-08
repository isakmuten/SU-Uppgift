using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT_Simulation.Models
{
	public class FanModel
	{
		public bool IsOn { get; set; }
		public double Speed { get; set; }

		public FanModel()
		{
			IsOn = false;
			Speed = 0;
		}
	}

}
