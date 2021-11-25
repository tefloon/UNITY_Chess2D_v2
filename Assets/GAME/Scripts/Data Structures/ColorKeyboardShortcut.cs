using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.Collections;

namespace ChessGUI
{
	[Serializable]
	public struct ColorKeyboardShutcut
	{
		public KeyCode Key;
		public Color Color;
	}
}
