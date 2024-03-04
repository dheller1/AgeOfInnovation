namespace AoICore.AoIResources
{
	public class Cost
	{
		public Cost(Coins coins) { Coins = coins; }
		public Cost(Tools tools) { Tools = tools; }
		public Cost(Coins coins, Tools tools) : this(coins) {
			Tools = tools;
		}

		public Coins Coins { get; }
		public Tools Tools { get; }
	}
}
