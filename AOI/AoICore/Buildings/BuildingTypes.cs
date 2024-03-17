using AoICore.AoIResources;

namespace AoICore.Buildings
{
	public static class BuildingTypes {
		public class BuildingType
		{
			internal BuildingType(string name, Cost cost, params BuildingType[] upgradeOptions) {
				Name = name;
				Cost = cost;
				_upgradeOptions = new List<BuildingType>(upgradeOptions);
			}

			public string Name { get; }
			public Cost Cost { get; }
			public IEnumerable<BuildingType> UpgradeOptions => _upgradeOptions;
			public override string ToString() => Name;
			private readonly List<BuildingType> _upgradeOptions;

		}

		public static readonly BuildingType Palace = new("Palace", new((Coins)6, (Tools)4));
		public static readonly BuildingType University = new("University", new((Coins)8, (Tools)5));
		public static readonly BuildingType School = new("School", new((Coins)5, (Tools)3), University);
		public static readonly BuildingType Guild = new("Guild", new((Coins)3, (Tools)2), Palace, School);
		public static readonly BuildingType Workshop = new("Workshop", new((Coins)2, (Tools)1), Guild);
	}
}
