using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace UAParser.FormFactor.Utils
{
	internal static class RegexBinderBuilder
	{
		public static Func<Match, IEnumerator<int>, TResult> SelectMany<T1, T2, TResult>(
			this Func<Match, IEnumerator<int>, T1> binder,
			Func<T1, Func<Match, IEnumerator<int>, T2>> continuation,
			Func<T1, T2, TResult> projection
		)
		{
			return (m, num) =>
			{
				var bound = binder(m, num);
				var continued = continuation(bound)(m, num);
				var projected = projection(bound, continued);
				return projected;
			};
		}
	}
}