namespace AoICore.AoIResources
{
	public struct Cost
	{
		public Cost(Coins coins) { Coins = coins; }
		public Cost(Tools tools) { Tools = tools; }
		public Cost(Coins coins, Tools tools) : this(coins) {
			Tools = tools;
		}

		public Coins Coins { get; private set; } = new();
		public Tools Tools { get; private set; } = new();

		public void Add(Coins coins) => Coins += coins;
		public void Add(Tools tools) => Tools += tools;
		public void Add(Cost cost) {
			Coins += cost.Coins;
			Tools += cost.Tools;
		}
	}
}
