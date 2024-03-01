namespace AoIWPFGui.Resources
{
	internal static class AppResources
	{
		private static readonly ResourceDictionary _dict = new() {
			Source = new Uri("/AoiWPFGui;component/Resources/Resources.xaml", UriKind.RelativeOrAbsolute)
		};

		public static T Get<T>(string resourceKey) where T : class {
			return _dict[resourceKey] as T ?? throw new InvalidCastException($"Resource {resourceKey} has unexpected type");
		}
	}

}
