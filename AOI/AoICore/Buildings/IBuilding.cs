using AoICore.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoICore.Buildings
{
	public interface IBuilding
	{
		IPlayer Owner { get; }
	}
}
