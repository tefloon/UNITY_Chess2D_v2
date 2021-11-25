using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Bitboard = System.UInt64;

public class Position
{
	public Bitboard B_Pawns, B_Knights, B_Bishops, B_Rooks, B_Queens, B_King,
					W_Pawns, W_Knights, W_Bishops, W_Rooks, W_Queens, W_King;

	public Bitboard EnPassant;

	public bool CastlingWK; 
	public bool CastlingWQ; 
	public bool CastlingBK; 
	public bool CastlingBQ; 

	public Bitboard OccupiedB
	{
		get
		{
			return B_Pawns | B_Knights | B_Bishops | B_Rooks | B_Queens | B_King;
		}
	}
	public Bitboard OccupiedW
	{
		get
		{
			return W_Pawns | W_Knights | W_Bishops | W_Rooks | W_Queens | W_King;
		}
	}
	public Bitboard Occupied
	{
		get
		{
			return OccupiedB | OccupiedW;
		}
	}

	public Bitboard[] Pieces = new Bitboard[64];

	public Position()
	{
		for (Piece p = Piece.W_PAWN; p < Piece.PIECE_NB; p++)
		{
			Pieces[p.ToInt()] = 0;
		}
	}
}
