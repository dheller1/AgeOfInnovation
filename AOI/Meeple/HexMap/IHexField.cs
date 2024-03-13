using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Meeple.HexMap
{
	public interface IHexField
	{
		/// <summary>
		/// Primary coordinate (following the main orientation of the hex map)
		/// </summary>
		int Q { get; set; }
		
		/// <summary>
		/// Secondary coordinate (rotated by 60° to the main orientation of the hex map)
		/// </summary>
		int R { get; set; }


		// FIXME: Remove Q, R?! This should only be known in the map containing the hex.
	}
}
