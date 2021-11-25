using System;

namespace Extensions
{
	/// /////////////////////////////////////////////////////////////////// ///
	/// General usage:														///
	///		public enum SomeEnum: { dog = 1, cat, tree };					///
	///																		///
	///		SomeEnum myEnum  = SomeEnum.cat;								///
	///		SomeEnum myEnum2 = SomeEnum.tree;								///
	///																		///
	///		myEnum.ToInt();					-> 2							///
	///		myEnum.Count();					-> 3							///
	///		myEnum3 = myEnum.Add(myEnum2);	-> (SomeEnum)5 -> SomeEnum.tree	///
	/// /////////////////////////////////////////////////////////////////// ///


	/// <summary>
	/// Extension methods for enums.
	/// </summary>
	public static class ExtensionMethodsEnum
	{
		/// <summary>
		/// Converts enum to int
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static int ToInt<T>(this T source) where T : IConvertible //enum
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			return (int)(IConvertible)source;
		}

		/// <summary>
		/// Converts enum to float
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <returns></returns>
		public static float ToFloat<T>(this T source) where T : IConvertible //enum
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			return (float)(IConvertible)source;
		}

		/// <summary>
		/// Returns the number of enum members
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public static int Count<T>() where T : IConvertible //enum
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			return Enum.GetNames(typeof(T)).Length;
		}

		/// <summary>
		/// Adds enum to another enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="toAdd"></param>
		/// <returns></returns>
		public static T Add<T>(this T source, T toAdd) where T : IConvertible //enum
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			T output = (T)(IConvertible)((int)(IConvertible)source + (int)(IConvertible)toAdd);

			return output;
		}

		/// <summary>
		/// Subtracts enum from another enum
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="source"></param>
		/// <param name="toAdd"></param>
		/// <returns></returns>
		public static T Subtract<T>(this T source, T toAdd) where T : IConvertible //enum
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			T output = (T)(IConvertible)((int)(IConvertible)source - (int)(IConvertible)toAdd);

			return output;
		}


		public static T Next<T>(this T source)
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			T output = (T)(IConvertible)((int)(IConvertible)source + 1);

			return output;
		}

		public static T Previous<T>(this T source)
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException("T must be an enumerated type");

			T output = (T)(IConvertible)((int)(IConvertible)source - 1);

			return output;
		}

	}
}
