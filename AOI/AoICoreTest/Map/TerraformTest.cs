using AoICore.Map;

namespace AoICoreTest.Map
{
	[TestClass]
	public class TerraformTest
	{
		[TestMethod]
		public void TestGetEffectiveTerrainOffset() {
			Assert.AreEqual( 1, Terraform.GetEffectiveTerrainOffset(Terrain.Mountain, Terrain.Wasteland));
			Assert.AreEqual(-1, Terraform.GetEffectiveTerrainOffset(Terrain.Mountain, Terrain.Forest));

			Assert.AreEqual(-3, Terraform.GetEffectiveTerrainOffset(Terrain.Desert, Terrain.Forest));
			Assert.AreEqual( 2, Terraform.GetEffectiveTerrainOffset(Terrain.Plains, Terrain.Lake));

			Assert.AreEqual(+3, Terraform.GetEffectiveTerrainOffset(Terrain.Swamp, Terrain.Mountain));
			Assert.AreEqual(-3, Terraform.GetEffectiveTerrainOffset(Terrain.Forest, Terrain.Plains));
		}
	}
}
