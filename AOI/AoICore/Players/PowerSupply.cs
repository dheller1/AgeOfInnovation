using System.Diagnostics;

namespace AoICore.Players
{
	public class PowerSupply
	{
		public PowerSupply() : this((PowerTokens)7, (PowerTokens)5) { }

		public PowerSupply(PowerTokens bowl_I, PowerTokens bowl_II) {
			Bowl_I = bowl_I;
			Bowl_II = bowl_II;
		}

		public PowerTokens Bowl_I { get; private set; }
		public PowerTokens Bowl_II { get; private set; }
		public PowerTokens Bowl_III { get; private set; } = (PowerTokens)0;

		public PowerTokens MaxGain => 2 * Bowl_I + 1 * Bowl_II;
		public PowerTokens AvailablePower => Bowl_III;

		public void Gain(int amount) => Gain((PowerTokens)amount);
		public void Gain(PowerTokens tokens) {
			if(tokens < 0 || tokens > MaxGain) { throw new ArgumentOutOfRangeException(nameof(tokens)); }

			var tokensLeft = tokens;
			var ItoII = (PowerTokens)Math.Min((int)tokensLeft, (int)Bowl_I);
			if(ItoII > 0) {
				tokensLeft -= ItoII;
				Bowl_I -= ItoII;
				Bowl_II += ItoII;
			}

			if(tokensLeft > 0) {
				var IItoIII = (PowerTokens)Math.Min((int)tokensLeft, (int)Bowl_II);
				if(IItoIII > 0) {
					tokensLeft -= IItoIII;
					Bowl_II -= IItoIII;
					Bowl_III += IItoIII;
				}
			}
			Debug.Assert(tokensLeft == 0);
		}

		public void Sacrifice(int amount) => Sacrifice((PowerTokens)amount);
		public void Sacrifice(PowerTokens tokens) {
			if(tokens < 0 || 2 * tokens > Bowl_II) { throw new ArgumentOutOfRangeException(nameof(tokens)); }

			Bowl_II -= 2 * tokens;
			Bowl_III += tokens;
		}
	}
}
