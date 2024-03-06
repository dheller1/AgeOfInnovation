namespace AoICore.AoIResources
{
	public class Supply
	{
		public Supply() { }

		public void Add(Coins coins) => Coins += coins;
		public void Add(Tools tools) => Tools += tools;

		public bool CanPay(Cost cost) {
			return Coins >= cost.Coins && Tools >= cost.Tools;
		}

		public bool TryPay(Cost cost) {
			if(!CanPay(cost)) { return false; }
			Coins -= cost.Coins;
			Tools -= cost.Tools;
			return true;
		}

		public Coins Coins { get; private set; }
		public Tools Tools { get; private set; }
	}
}
