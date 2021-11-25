using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

namespace ChessGUI
{
	[CreateAssetMenu(menuName = "Game Settings/Color Scheme")]
	public class ColorScheme : ScriptableObject
	{
		public Transform BoardBackground;

		public Color BoardTintColor;
		public Color DefaultUserAnnotationColor;
		public Color SystemAnnotationColor;

		public ColorShutcut[] ColorModifiers;
	}
}