using AoICore;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoIWPFGui.ViewModels
{
	public class AppViewModel : ReactiveObject
	{
		public AppViewModel() {
			HexGridVM = new HexGridViewModel(Game.Map);
		}
		
		public HexGridViewModel HexGridVM { get; }

		public AoIGame Game { get; } = new AoIGame();

	}
}
