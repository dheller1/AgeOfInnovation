using Meeple.HexMap;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoICore.Map
{
	public class SmallMap : IEnumerable<TerrainHex>
	{
		public SmallMap() {
			var mapdef = new[] {
				(0, 0, Terrain.Forest),
				(1, 0, Terrain.Mountain),
				(2, 0, Terrain.Desert),
				(3, 0, Terrain.Plains),
				(4, 0, Terrain.River),
				(5, 0, Terrain.Lake),
				(6, 0, Terrain.Forest),
				(7, 0, Terrain.Mountain),
				(8, 0, Terrain.Wasteland),

				(-1, 1, Terrain.River),
				(0, 1, Terrain.Swamp),
				(1, 1, Terrain.Lake),
				(2, 1, Terrain.Swamp),
				(3, 1, Terrain.Wasteland),
				(4, 1, Terrain.River),
				(5, 1, Terrain.Swamp),
				(6, 1, Terrain.Plains),
				(7, 1, Terrain.Lake),
				(8, 1, Terrain.River),
				(9, 1, Terrain.Forest),

				(-1, 2, Terrain.River),
				(0, 2, Terrain.Plains),
				(1, 2, Terrain.Mountain),
				(2, 2, Terrain.Forest),
				(3, 2, Terrain.River),
				(4, 2, Terrain.River),
				(5, 2, Terrain.Desert),
				(6, 2, Terrain.River),
				(7, 2, Terrain.River),
				(8, 2, Terrain.Desert),

				(-1, 3, Terrain.River),
				(0, 3, Terrain.Wasteland),
				(1, 3, Terrain.Desert),
				(2, 3, Terrain.River),
				(3, 3, Terrain.Forest),
				(4, 3, Terrain.River),
				(5, 3, Terrain.River),
				(6, 3, Terrain.Mountain),
				(7, 3, Terrain.Plains),
				(8, 3, Terrain.Swamp),

				(-1, 4, Terrain.River),
				(0, 4, Terrain.River),
				(1, 4, Terrain.River),
				(2, 4, Terrain.Swamp),
				(3, 4, Terrain.Plains),
				(4, 4, Terrain.Lake),
				(5, 4, Terrain.Desert),
				(6, 4, Terrain.Wasteland),
				(7, 4, Terrain.Mountain),

				(-2, 5, Terrain.River),
				(-1, 5, Terrain.Lake),
				(0, 5, Terrain.Plains),
				(1, 5, Terrain.River),
				(2, 5, Terrain.Wasteland),
				(3, 5, Terrain.Mountain),
				(4, 5, Terrain.River),
				(5, 5, Terrain.Forest),
				(6, 5, Terrain.Lake),
				(7, 5, Terrain.Desert),

				(-2, 6, Terrain.River),
				(-1, 6, Terrain.Swamp),
				(0, 6, Terrain.Mountain),
				(1, 6, Terrain.River),
				(2, 6, Terrain.River),
				(3, 6, Terrain.River),
				(4, 6, Terrain.River),
				(5, 6, Terrain.River),
				(6, 6, Terrain.River),

				(-3, 7, Terrain.River),
				(-2, 7, Terrain.Desert),
				(-1, 7, Terrain.Forest),
				(0, 7, Terrain.Wasteland),
				(1, 7, Terrain.Desert),
				(2, 7, Terrain.Forest),
				(3, 7, Terrain.Lake),
				(4, 7, Terrain.Swamp),
				(5, 7, Terrain.Forest),
				(6, 7, Terrain.River),

				(-3, 8, Terrain.Wasteland),
				(-2, 8, Terrain.Plains),
				(-1, 8, Terrain.Mountain),
				(0, 8, Terrain.Lake),
				(1, 8, Terrain.Swamp),
				(2, 8, Terrain.Plains),
				(3, 8, Terrain.Desert),
				(4, 8, Terrain.Wasteland),
				(5, 8, Terrain.Mountain)
			};

			foreach(var (q, r, t) in mapdef) {
				_hexMap[q, r] = new TerrainHex(t);
			}
			var test = _hexMap.GroupBy(field => field.Terrain).Select(g => (g.Key, g.Count()));
			;
		}

		private readonly HexMap<TerrainHex> _hexMap = new();

		public IEnumerator<TerrainHex> GetEnumerator() => _hexMap.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => _hexMap.GetEnumerator();
	}
}
