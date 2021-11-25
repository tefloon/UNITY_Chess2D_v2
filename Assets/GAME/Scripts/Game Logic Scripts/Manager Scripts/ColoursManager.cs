using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BitStrap;


namespace ChessGUI
{
    public class ColoursManager : Singleton<ColoursManager>
    {
        public ColorScheme ColorScheme;
        
        [Range(0, 1)]
        public float OverlayColorTransparency = 0.5f;
        
        [Header("Color Scheme")]
        public Color BoardTintColor;
        public Color DefaultUserAnnotationColor;  // For things like square coloring, arrows etc.
        public Color SystemAnnotationColor;         // For visualizing possible moves

        [Space(10)]
        public ColorKeyboardShutcut[] ColorModifiers;

        public SquareBackgroundColor currentColorName;
        private Color currentColor;
        public Color CurrentColor
        {
            get
            {
                UpdateCurrentColor();
                return currentColor;
            }
        }

        private void OnValidate()
        {
            ReadColorScheme();
        }

        private void Start()
        {
            ReadColorScheme();
        }

        private void ReadColorScheme()
        {
            DefaultUserAnnotationColor = ColorScheme.DefaultUserAnnotationColor;
            SystemAnnotationColor = ColorScheme.SystemAnnotationColor;
            BoardTintColor = ColorScheme.BoardTintColor;

            ColorModifiers = ColorScheme.ColorModifiers;
        }

        private void UpdateCurrentColor()
        {
            currentColor = DefaultUserAnnotationColor;
            currentColorName = SquareBackgroundColor.COLOR1;

            for (int i = 0; i < ColorModifiers.Length; i++)
			{
                if (Input.GetKey(ColorModifiers[i].Key))
                {
                    currentColor = ColorModifiers[i].Color;
                    currentColorName = (SquareBackgroundColor)i;
                    break;
                }
            }

            currentColor.a = OverlayColorTransparency;
        }
    }
}