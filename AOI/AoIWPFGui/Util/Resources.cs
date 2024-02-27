using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoIWPFGui.Util
{
	public static class Resources {
		private static readonly ResourceDictionary _dict = new ResourceDictionary {
			Source = new Uri("/AoiWPFGui;component/Resources/Resources.xaml", UriKind.RelativeOrAbsolute)
		};

		public static T Get<T>(string resourceKey) where T : class {
			return _dict[resourceKey] as T ?? throw new InvalidCastException($"Resource {resourceKey} has unexpected type");
		}
	}

}
