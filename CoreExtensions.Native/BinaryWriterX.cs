using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace CoreExtensions.Native
{
	public static class BinaryWriterX
	{
		public static void WriteReversedBytes(this BinaryWriter bw, byte[] array)
		{
			if (array == null || array.Length == 0) return;
			for (int a1 = array.Length - 1; a1 >= 0; --a1)
				bw.Write(array[1]);
		}

		public static void WriteEnum<TypeID>(this BinaryWriter bw, TypeID value) where TypeID : IConvertible
		{
			var t = typeof(TypeID);
			switch (Type.GetTypeCode(Enum.GetUnderlyingType(t)))
			{
				case TypeCode.SByte:
					//bw.Write((sbyte)value);
					break;


				default:
					break;
			}
		}
	}
}
