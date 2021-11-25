using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using BitStrap;
using Bitboard = System.UInt64;

namespace ChessGUI
{
	// 0000010001110000100011000011011110000111000000001100000001100000

	public class BoardManager : Singleton<BoardManager>
	{
		public Dictionary<Bitboard, SquareScript> SquareScripts = new Dictionary<Bitboard, SquareScript>();
		public Dictionary<Bitboard, PieceScript> PieceScripts = new Dictionary<Bitboard, PieceScript>();

		public Transform PiecesParent;
		public Transform[] PiecePrefabs;
		public Position CurrentPosition = new Position();

		public void CreateBoard()
		{
			BoardBuilder.Instance.SetupBoard();
			CurrentPosition = StartingPosition.Instance.CreateStartingPosition();
			PopulateBoard(CurrentPosition);
		}

		public void PopulateBoard(Position pos)
		{
			for (Piece p = Piece.W_PAWN; p < Piece.PIECE_NB; p++)
			{
				// Porównaj czy w pozycji na danym miejscu jest dana figura, jeœli tak, to Instantiate
				Bitboard b = pos.Pieces[p.ToInt()];

				foreach (SquareScript square in SquareScripts.Values)
				{
					if (square & b)
					{
						var go = Instantiate(PiecePrefabs[p.ToInt()], square.transform.position, Quaternion.identity, PiecesParent);
						var ps = go.GetComponent<PieceScript>();
						ps.Setup(square.Bitboard, p);
						PieceScripts.Add(square.Bitboard, ps);	

						square.Piece = p;
					}
				}
			}
		}
	}
}