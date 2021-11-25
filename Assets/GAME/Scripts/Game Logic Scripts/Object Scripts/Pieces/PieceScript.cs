using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitboard = System.UInt64;


namespace ChessGUI {
	public class PieceScript : MonoBehaviour
	{
		public List<Bitboard> positionHistory;
		public Bitboard currentPosition;
		public Piece Piece;

		private bool isAnimating;
		private float animationSpeed;
		private float advancement = 0f;
		private Transform startingSquare, targetSquare;

		public void Setup(Bitboard currentPos, Piece p = Piece.NO_PIECE)
		{
			positionHistory = new List<Bitboard>();

			Piece = p;
			currentPosition = currentPos;

			animationSpeed = PieceManager.Instance.AnimationSpeed;
		}

		public void MakeMove(Bitboard newPos)
		{
			// Make a move -> accept new position, physically move the object to a new square

			isAnimating = true;
			startingSquare = BoardManager.Instance.SquareScripts[currentPosition].transform;
			targetSquare   = BoardManager.Instance.SquareScripts[newPos].transform;
	
			positionHistory.Add(currentPosition);
			currentPosition = newPos;
		}

		private void Update()
		{
			if (isAnimating) Animate();
		}

		private void Animate()
		{
			this.transform.position = Vector3.Lerp(startingSquare.position, targetSquare.position, advancement);

			advancement += animationSpeed * Time.deltaTime;

			// When we are done animating
			if (Vector3.Distance(startingSquare.position, targetSquare.position) < Mathf.Epsilon)
			{
				// Make sure the piece arrived EXACTLY at the required position
				this.transform.position = targetSquare.position;

				// stop animation
				isAnimating = false;

				// reset the animation advancement variable
				advancement = 0f;
			}
		}
	}
}
