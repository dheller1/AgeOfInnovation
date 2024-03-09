using AoICore.AoIResources;
using AoICore.Players;
using System.Windows.Media;

namespace AoIWPFGui.ViewModels
{
	public class ResourceCostViewModel(Cost cost, IPlayer player) : ReactiveObject
	{
		public Cost Cost { get; } = cost;
		public IPlayer Player { get; } = player;

		public string CoinsText => $"{Cost.Coins}C";
		public string ToolsText => $"{Cost.Tools}T";
		public Brush CoinsBrush => new SolidColorBrush(Player.Resources.CanPay(Cost.Coins) ? Colors.Black : Colors.Red);
		public Brush ToolsBrush => new SolidColorBrush(Player.Resources.CanPay(Cost.Tools) ? Colors.Black : Colors.Red);
	}
}
