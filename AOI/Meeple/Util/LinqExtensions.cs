namespace Meeple.Util
{
	public static class LinqExtensions
	{
		public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source)
			=> source.Where(x => x != null).Select(x => x!);
	}
}
