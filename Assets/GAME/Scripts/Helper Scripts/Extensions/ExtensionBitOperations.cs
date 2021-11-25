using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using Bitboard = System.UInt64;

namespace Extensions
{
	public static class ExtensionBitOperations
	{
		private const int INT_SIZE = 64;
		private static readonly int[] RandIntsArray = new int[INT_SIZE];
		private static readonly bool[] BitsArray = new bool[INT_SIZE];
		private static readonly StringBuilder StringBuilder = new StringBuilder(INT_SIZE);

		public static ulong StringToBinary(string _bits)
		{
			ulong result = 0;

			StringBuilder flipped = new StringBuilder();

			for (int i = _bits.Length - 1; i >= 0; i--)
			{
				flipped.Append(_bits[i]);
			}

			for (int i = 0; i < _bits.Length; i++)
			{
				if (flipped[i] == '1')
				{
					result |= (ulong)1 << i;
				}
			}

			return result;
		}

		/// <summary>
		/// Changes a decimal 1-s and 0-s number into a binary one
		/// e.g. 11101 (eleven thousand one) into 11101 in binary (forty-five)
		/// </summary>
		/// <param name="_input"></param>
		/// <returns></returns>
		public static ulong DecimalToBinary(ulong _input)
		{
			ulong result = 0;
			int length = _input.ToString().Length;

			ulong trimmedInt = _input;

			for (int i = 0; i < length; i++)
			{
				if (trimmedInt % 10 == 1)
				{
					var mask = (ulong)1 << i;
					result |= mask;
				}

				else if (trimmedInt % 10 > 1)
				{
					throw new System.ArgumentException("Argument contains other digits than 1 and 0!", nameof(_input));
				}

				trimmedInt /= 10;
			}
			return result;
		}

		/// <summary>
		/// Changes a series of decimal numbers containing 1-s and 0-s 
		/// into a binary one e.g. 111, 101, 100 into 111101100 in binary
		/// </summary>
		/// <param name="_input"></param>
		/// <returns></returns>
		public static ulong DecimalToBinary(params ulong[] _input)
		{
			int numberOfArgs = _input.Length;
			int combinedLengthOfArgs = 0;
			int[] lengthsOfArgs = new int[numberOfArgs];

			for (int i = 0; i < numberOfArgs; i++)
			{
				lengthsOfArgs[i] = _input[i].ToString().Length;
				combinedLengthOfArgs += lengthsOfArgs[i];
			}

			if (combinedLengthOfArgs > 64)
			{
				throw new System.ArgumentOutOfRangeException(nameof(_input), "Arguments combined length exceeds ulong length (64)");
			}

			ulong finalResult = 0;

			for (int j = 0; j < numberOfArgs; j++)
			{
				ulong partialResult = 0;
				int length = lengthsOfArgs[j];

				ulong trimmedInt = _input[j];
				ulong mask;

				for (int i = 0; i < length; i++)
				{
					if (trimmedInt % 10 == 1)
					{
						mask = (ulong)1 << i;
						partialResult |= mask;
					}

					else if (trimmedInt % 10 > 1)
					{
						throw new System.ArgumentException("Argument contains other digits than 1 and 0!", nameof(_input));
					}

					trimmedInt /= 10;
				}

				int amountToShiftLeft = 0;

				for (int i = j + 1; i < numberOfArgs; i++)
				{
					amountToShiftLeft += lengthsOfArgs[i];
				}

				finalResult |= (partialResult << amountToShiftLeft);
			}

			return finalResult;
		}

		/// <summary>
		/// Prettily formats a ulong's bits.
		/// </summary>
		/// <param name="_input"></param>
		/// <returns></returns>
		public static string BinaryToString(this ulong _input)
		{
			StringBuilder.Length = 0;

			for (int i = 0; i < INT_SIZE; i++)
			{
				ulong mask = (ulong)1 << i;
				BitsArray[i] = (_input & mask) != 0;
			}

			for (int i = INT_SIZE - 1; i >= 0; i--)
			{
				StringBuilder.Append(BitsArray[i] ? '1' : '0');
			}

			return StringBuilder.ToString();
		}

		/// <summary>
		/// Prettily formats a ulong's bits with newline every 8 characters.
		/// </summary>
		/// <param name="_input"></param>
		/// <returns></returns>
		public static string BinaryToStringNewline(this ulong _input)
		{
			StringBuilder.Length = 0;

			for (int i = 0; i < INT_SIZE; i++)
			{
				ulong mask = (ulong)1 << i;
				BitsArray[i] = (_input & mask) != 0;
			}

			int chNum = 0;
			for (int i = INT_SIZE - 1; i >= 0; i--)
			{
				if (chNum % 8 == 0)
				{
					StringBuilder.Append('\n');
				}

				StringBuilder.Append(BitsArray[i] ? '1' : '0');
				chNum++;
			}

			return StringBuilder.ToString();
		}

		/// <summary>
		/// Swap two bits inside an int.
		/// </summary>
		/// <param name="_num"></param>
		/// <param name="_indexA"></param>
		/// <param name="_indexB"></param>
		/// <returns></returns>
		public static ulong SwapBits(ulong _num, int _indexA, int _indexB)
		{
			ulong maskA = (ulong)1 << _indexA;
			ulong maskB = (ulong)1 << _indexB;

			bool a = (_num & maskA) != 0;
			bool b = (_num & maskB) != 0;

			_num = a ? (_num | maskB) : (_num & (~maskB));
			_num = b ? (_num | maskA) : (_num & (~maskA));

			return _num;
		}

		/// <summary>
		/// Combines two 32-bit uints into one 64-bit ulong
		/// </summary>
		/// <param name="_left"></param>
		/// <param name="_right"></param>
		/// <returns></returns>
		public static ulong Combine(uint _left, uint _right)
		{
			ulong result = (ulong)_left << 32 | (ulong)(uint)_right;
			return result;
		}

		/// <summary>
		/// Combines four 16-bit ushorts into one 64-bit ulong
		/// </summary>
		/// <param name="_lefter"></param>
		/// <param name="_left"></param>
		/// <param name="_right"></param>
		/// <param name="_righter"></param>
		/// <returns></returns>
		public static ulong Combine(ushort _lefter, ushort _left, ushort _right, ushort _righter)
		{
			ulong result = (ulong)_lefter << 48 | (ulong)_left << 32 | (ulong)_right << 16 | (ulong)_righter;
			return result;
		}

		/// <summary>
		/// Combines eight 8-bit bytes int one 64-bit ulong
		/// </summary>
		/// <param name="_left0"></param>
		/// <param name="_left1"></param>
		/// <param name="_left2"></param>
		/// <param name="_left3"></param>
		/// <param name="_left4"></param>
		/// <param name="_left5"></param>
		/// <param name="_left6"></param>
		/// <param name="_left7"></param>
		/// <returns></returns>
		public static ulong Combine(byte _left0, byte _left1, byte _left2, byte _left3, byte _left4, byte _left5, byte _left6, byte _left7)
		{
			ulong result = (ulong)_left0 << 56 | (ulong)_left1 << 48 | (ulong)_left2 << 40 | (ulong)_left3 << 32 |
						   (ulong)_left4 << 24 | (ulong)_left5 << 16 | (ulong)_left6 << 8 | (ulong)_left7;
			return result;
		}

		/// <summary>
		/// Extension method that rises an integer to a power of value
		/// </summary>
		/// <param name="_input"></param>
		/// <param name="_value">power to rise to</param>
		/// <returns></returns>
		public static int Pow(this int _input, int _value)
		{
			int result = 1;

			for (int i = 0; i < _value; i++)
			{
				result *= _input;
			}

			return result;
		}
	}
}
