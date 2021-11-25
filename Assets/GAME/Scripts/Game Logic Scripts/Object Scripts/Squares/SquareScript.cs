using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitboard = System.UInt64;

namespace ChessGUI
{
	[RequireComponent(typeof(SquareColoring))]
	public class SquareScript : MonoBehaviour
	{
		public Square Square;		// Do wywalenia, bêdziemy siê pos³ugiwali tylko bitboardami
		public File File;
		public Rank Rank;
		public SquareColor SquareColor;
		public Piece Piece;

		public Bitboard Bitboard { 
			get 
			{
				return (Bitboard)1 << (int)Square;		
			} 
		}

		public SquareScript Setup(Square _Square)		// Zrobiæ now¹ funkcjê Setup przyjmujac¹ jako argument Bitboard
		{
			Square = _Square;
			Rank = (Rank)(int)Mathf.Floor((int)Square / 8);
			File = (File)((int)Square % 8);
			Piece = Piece.NO_PIECE;


			// Set the color of the square
			SquareColor = (SquareColor)(((int)File + (int)Rank) % 2);

			// Set the human-readable name of the square in inspector
			transform.name = _Square.ToString();

			return this;
		}

		public static bool operator &(SquareScript s, Bitboard b)
		{
			return (s.Bitboard & b) != 0;
		}
	}
}

