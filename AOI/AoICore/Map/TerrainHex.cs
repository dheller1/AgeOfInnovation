using AoICore.Buildings;
using AoICore.Player;
using Meeple.HexMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoICore.Map
{
	public class TerrainHex : IHexField, IBuildSlot
	{
		public TerrainHex(Terrain terrain) {
			Terrain = terrain;
		}

		public int Q { get; set; }
		public int R { get; set; }
		public Terrain Terrain { get; }

		public IBuilding? Building { get; } = null;
		public IPlayer? Controller => Building?.Owner;
	}
}
