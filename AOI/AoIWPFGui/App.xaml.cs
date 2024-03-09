using Splat;
using System.Reflection;

namespace AoIWPFGui
{
	public partial class App : Application
	{
		public App() {
			var assembly = Assembly.GetCallingAssembly();
			Locator.CurrentMutable.RegisterViewsForViewModels(assembly);
		}
	}
}
