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
			HexGridVM = new HexGridViewModel("Test123");
		}
		
		public HexGridViewModel HexGridVM { get; }

	}
}
