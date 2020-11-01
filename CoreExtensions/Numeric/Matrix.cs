using System;
using System.Collections.Generic;
using System.Text;

namespace CoreExtensions.Numeric
{
	public static class Matrix
	{
		internal static string ToString(IMatrix matrix, string format)
		{
			/*
			* Supported formats are:
			* null/String.Empty = regular matrix format
			* "-" = all entries are inlined and separated with -
			* " " = all entries are inlined and separated with whitespace
			*/

			var sb = new StringBuilder(matrix.Entries * 3);

			switch (format)
			{
				case null:
				case "":
					for (int i = 1; i <= matrix.Rows; ++i)
					{

						for (int k = 1; k <= matrix.Columns; ++k) sb.Append(matrix[i, k]);
						sb.AppendLine();

					}
					return sb.ToString();

				case " ":
				case "-":
					for (int i = 0; i < matrix.Entries; ++i) { sb.Append(matrix[i]); sb.Append(format); }
					return sb.ToString();

				default:
					goto case null;

			}
		}


		/*
		 * 1x1 * 1x1
		 * 1x1 * 1x2
		 * 1x1 * 1x3
		 * 1x1 * 1x4
		 * 
		 * 1x2 * 2x1
		 * 1x2 * 2x2
		 * 1x2 * 2x3
		 * 1x2 * 2x4
		 * 
		 * 1x3 * 3x1
		 * 1x3 * 3x2
		 * 1x3 * 3x3
		 * 1x3 * 3x4
		 * 
		 * 1x4 * 4x1
		 * 1x4 * 4x2
		 * 1x4 * 4x3
		 * 1x4 * 4x4
		 * 
		 * 2x1 * 1x1
		 * 2x1 * 1x2
		 * 2x1 * 1x3
		 * 2x1 * 1x4
		 * 
		 * 
		 * 
		 * 
		 */
	}
}
