using AoICore.AoIResources;

namespace AoICore.Buildings
{
	public static class BuildingTypes {
		public class BuildingType
		{
			internal BuildingType(Cost cost, params BuildingType[] upgradeOptions) { 
				Cost = cost;
				_upgradeOptions = new List<BuildingType>(upgradeOptions);
			}

			public Cost Cost { get; }
			public IEnumerable<BuildingType> UpgradeOptions => _upgradeOptions;
			private readonly List<BuildingType> _upgradeOptions;
		}

		public static readonly BuildingType Palace = new(new((Coins)6, (Tools)4));
		public static readonly BuildingType University = new(new((Coins)8, (Tools)5));
		public static readonly BuildingType School = new(new((Coins)5, (Tools)3), University);
		public static readonly BuildingType Guild = new(new((Coins)3, (Tools)2), Palace, School);
		public static readonly BuildingType Workshop = new(new((Coins)2, (Tools)1), Guild);
	}
}
