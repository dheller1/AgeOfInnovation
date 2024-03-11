using Meeple.Util;

namespace AoICore.AoIResources
{
	public class Supply : NotificationBase
	{
		public Supply() { }

		public void Add(Coins coins) => Coins += coins;
		public void Add(Tools tools) => Tools += tools;
		public void Add(Cost cost) {
			Coins += cost.Coins;
			Tools += cost.Tools;
		}

		public bool CanPay(Cost cost) {
			return Coins >= cost.Coins && Tools >= cost.Tools;
		}
		public bool CanPay(Tools tools) {
			return Tools >= tools;
		}
		public bool CanPay(Coins coins) {
			return Coins >= coins;
		}

		public bool Pay(Cost cost) {
			if(!CanPay(cost)) { throw new InvalidOperationException("Insufficient resources to pay cost"); }
			Coins -= cost.Coins;
			Tools -= cost.Tools;
			return true;
		}

		public Coins Coins { get => _coins; private set => SetProperty(ref _coins, value); }
		public Tools Tools { get => _tools; private set => SetProperty(ref _tools, value); }

		private Coins _coins = new();
		private Tools _tools = new();
	}
}
