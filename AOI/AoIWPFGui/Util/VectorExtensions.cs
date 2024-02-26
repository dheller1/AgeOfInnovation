using System.Windows;

namespace AoIWPFGui.Util
{
	public static class VectorExtensions
	{
		public static Vector Rotated(this Vector v, double degrees) {
			var rad = degrees * Math.PI / 180.0;
			return new Vector(
				Math.Cos(rad) * v.X - Math.Sin(rad) * v.Y,
				Math.Sin(rad) * v.X + Math.Cos(rad) * v.Y);
		}
	}
}
