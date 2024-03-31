using System.Runtime.CompilerServices;

namespace OodleDotNet;

internal static class Util
{
	public static void ThrowIfNull(nint argument, [CallerArgumentExpression(nameof(argument))] string? paramName = null)
	{
		if (argument == nint.Zero)
		{
			throw new ArgumentNullException(paramName);
		}
	}
}
