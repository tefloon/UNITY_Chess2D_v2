using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;

namespace ChessGUI
{
	public class BoardBuilder : Singleton<BoardBuilder>
	{
		public Transform BoardBackground;
		public Transform TileToPlace;
		public float BoardScaleFactor;

		[Space]
		[InspectorButton("SizeUp", ButtonWidth = 200)] public bool IncreaseSize;
		[InspectorButton("SizeDown", ButtonWidth = 200)] public bool DecreaseSize;

		private float sizeStep = .5f;
		private float maxSize;
		private float minSize;

		private float squareDimensions;
		private Sprite boardSprite;
		private Transform boardTransform;




		void OnEnable()
		{
			BoardBackground = ColoursManager.Instance.ColorScheme.BoardBackground;
			boardSprite = BoardBackground.GetComponent<SpriteRenderer>().sprite;
			float screenHeight = 2f * Camera.main.orthographicSize;
			maxSize = screenHeight / 8;
			minSize = screenHeight / 24;

		}

		public void SizeUp()
		{
			if (boardTransform &&  BoardScaleFactor < maxSize - sizeStep)
			{
				BoardScaleFactor += sizeStep;
				transform.localScale = new Vector3(BoardScaleFactor, BoardScaleFactor, 1);
			}
		}

		public void SizeDown()
		{
			if (boardTransform && BoardScaleFactor > minSize)
			{
				BoardScaleFactor -= sizeStep;
				transform.localScale = new Vector3(BoardScaleFactor, BoardScaleFactor, 1);
			}
		}

		public void SetupBoard()
		{
			InitializeBackground();
			BuildBoard();

			transform.localScale = new Vector3(BoardScaleFactor, BoardScaleFactor, 1);
		}

		private void BuildBoard()
		{
			Vector3 startingPoint = transform.position - new Vector3(3.5f * squareDimensions, 3.5f * squareDimensions, 0);
			Square s = Square.SQ_A1;

			for (int y = 0; y < 8; y++)
			{
				for (int x = 0; x < 8; x++)
				{
					var go = Instantiate(TileToPlace, startingPoint + new Vector3(squareDimensions * x, squareDimensions * y, 0), Quaternion.identity, boardTransform);
					go.transform.localScale = Vector3.one * squareDimensions;
					go.name = s.ToString();

					var sqScript = go.GetComponent<SquareScript>();
					sqScript.Setup(s);
					BoardManager.Instance.SquareScripts.Add(sqScript.Bitboard, sqScript);

					s++;
				}
			}
		}

		private void InitializeBackground()
		{
			boardTransform = Instantiate(BoardBackground, transform.position, Quaternion.identity, transform);
			boardTransform.name = "Board";
			squareDimensions = (boardSprite.bounds.size.x * transform.localScale.x) / 8;
		}
	}
}
