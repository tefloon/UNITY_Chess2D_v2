using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Extensions
{
	public static class ExtensionMethodsUlong
	{
		public static ulong ToBit64(this ulong u)
		{
			return Convert.ToUInt64(u.ToString(), 2);
		}

		public static ulong ToBit64(this string s)
		{
			return Convert.ToUInt64(s, 2);
		}

		public static ulong ToBit64(this uint i)
		{
			return Convert.ToUInt64(i.ToString(), 2);
		}

		public static ulong SwapBits(this ulong num, int indexA, int indexB)
		{
			ulong maskA = (ulong)1 << indexA;
			ulong maskB = (ulong)1 << indexB;

			bool a = (num & maskA) != 0;
			bool b = (num & maskB) != 0;

			num = a ? (num | maskB) : (num & (~maskB));
			num = b ? (num | maskA) : (num & (~maskA));

			return num;
		}

		public static ulong ReverseBits64(this ulong v)
		{
			ulong r = v; // r will be reversed bits of v; first get LSB of v
			int s = 63; // extra shift needed at end
			for (v >>= 1; v != 0; v >>= 1)
			{
				r <<= 1;
				r |= (byte)(v & 1);
				s--;
			}

			r <<= s; // shift when v's highest bits are zero
			return r;

		}
	}
}

