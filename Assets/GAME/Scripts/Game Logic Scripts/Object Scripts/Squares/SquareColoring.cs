using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGUI
{
	public class SquareColoring : MonoBehaviour
	{
		[SerializeField] private SpriteRenderer overlay;
		[SerializeField] private SpriteRenderer highlight;

		private SquareBackgroundColor currentColor = SquareBackgroundColor.NONE;

		private ColoursManager palette;
		private EventsManager em;

		private void OnEnable()
		{
			palette = ColoursManager.Instance;
			em = EventsManager.Instance;
			em.StartPieceDraggingEvent += ClearSquareColor;
		}

		private void OnDisable()
		{
			em.StartPieceDraggingEvent -= ClearSquareColor;
		}

		private void Start()
		{
			UnHighlightSquare();
			ClearSquareColor(Vector3.zero);
		}

		/// <summary>
		/// Put a frame around the square (to indicate when clicking)
		/// </summary>
		public void HighlightSquare()
		{
			highlight.color = palette.CurrentColor;
		}

		/// <summary>
		/// Remove the frame around the square
		/// </summary>
		public void UnHighlightSquare()
		{
			Color colorToApply = highlight.color;
			colorToApply.a = 0;
			highlight.color = colorToApply;
		}


		/// <summary>
		/// Colors/UnColors the square when marked by the player
		/// </summary>
		/// <param name="unColorize">Force color removal?</param>
		public void ColorizeSquare()
		{
			Color colorToApply = palette.CurrentColor;
			SquareBackgroundColor colorToApplyName = palette.currentColorName;

			if (currentColor == colorToApplyName)
			{
				colorToApply.a = 0;
				colorToApplyName = SquareBackgroundColor.NONE;
			}

			overlay.color = colorToApply;
			currentColor = colorToApplyName;
		}


		/// <summary>
		/// Callback for UnColorizing square. Used by events.
		/// </summary>
		/// <param name="dummy"></param>
		public void ClearSquareColor(Vector3 dummy)
		{
			Color colorToApply = overlay.color;
			colorToApply.a = 0;
			overlay.color = colorToApply;
			currentColor = SquareBackgroundColor.NONE;
		}
	}
}
