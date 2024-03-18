global using PowerTokens = Meeple.Resources.Resource<AoICore.AoIResources.IPowerToken>;
using AoICore.Players;

namespace AoICoreTest.Player
{
	[TestClass]
	public class PowerSupplyTest
	{
		[TestMethod]
		public void TestCtors() {
			var ps = new PowerSupply();
			Assert.AreEqual(7, (int)ps.Bowl_I);
			Assert.AreEqual(5, (int)ps.Bowl_II);
			Assert.AreEqual(0, (int)ps.Bowl_III);


			var ps2 = new PowerSupply((PowerTokens)8, (PowerTokens)4);
			Assert.AreEqual(8, (int)ps2.Bowl_I);
			Assert.AreEqual(4, (int)ps2.Bowl_II);
			Assert.AreEqual(0, (int)ps2.Bowl_III);
		}

		[TestMethod]
		public void TestGain() {
			var ps = new PowerSupply();
			ps.Gain(2);
			Assert.AreEqual(5, (int)ps.Bowl_I);
			Assert.AreEqual(7, (int)ps.Bowl_II);
			Assert.AreEqual(0, (int)ps.Bowl_III);
			
			Assert.AreEqual(0, (int)ps.AvailablePower);
			Assert.AreEqual(17, (int)ps.MaxGain);

			ps.Gain(0);
			Assert.AreEqual(5, (int)ps.Bowl_I);
			Assert.AreEqual(7, (int)ps.Bowl_II);
			Assert.AreEqual(0, (int)ps.Bowl_III);

			Assert.AreEqual(0, (int)ps.AvailablePower);
			Assert.AreEqual(17, (int)ps.MaxGain);

			ps.Gain((PowerTokens)6);
			Assert.AreEqual(0, (int)ps.Bowl_I);
			Assert.AreEqual(11, (int)ps.Bowl_II);
			Assert.AreEqual(1, (int)ps.Bowl_III);
			
			Assert.AreEqual(1, (int)ps.AvailablePower);
			Assert.AreEqual(11, (int)ps.MaxGain);

			ps.Gain((PowerTokens)3);
			Assert.AreEqual(0, (int)ps.Bowl_I);
			Assert.AreEqual(8, (int)ps.Bowl_II);
			Assert.AreEqual(4, (int)ps.Bowl_III);
			
			Assert.AreEqual(4, (int)ps.AvailablePower);
			Assert.AreEqual(8, (int)ps.MaxGain);

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => ps.Gain(-1));
			Assert.ThrowsException<ArgumentOutOfRangeException>(() => ps.Gain(10));
		}

		[TestMethod]
		public void TestSacrifice() {
			var ps = new PowerSupply((PowerTokens)5, (PowerTokens)7);
			Assert.AreEqual(0, (int)ps.AvailablePower);

			ps.Sacrifice((PowerTokens)2);
			Assert.AreEqual(5, (int)ps.Bowl_I);
			Assert.AreEqual(3, (int)ps.Bowl_II);
			Assert.AreEqual(2, (int)ps.Bowl_III);

			Assert.ThrowsException<ArgumentOutOfRangeException>(() => ps.Sacrifice((PowerTokens)2));
			ps.Sacrifice((PowerTokens)1);
			Assert.AreEqual(3, (int)ps.AvailablePower);
		}
	}
}
