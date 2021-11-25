using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Extensions;
using Bitboard = System.UInt64;

public class StartingPosition : Singleton<StartingPosition>
{
	public Position CreateStartingPosition()
	{
		Position result = new Position();

		result.Pieces[Piece.W_PAWN.ToInt()] = (Bitboard)0xFF00;
		result.Pieces[Piece.W_ROOK.ToInt()] = (Bitboard)0x81;
		result.Pieces[Piece.W_KNIGHT.ToInt()] = (Bitboard)0x42;
		result.Pieces[Piece.W_BISHOP.ToInt()] = (Bitboard)0x24;
		result.Pieces[Piece.W_QUEEN.ToInt()] = (Bitboard)0x8;
		result.Pieces[Piece.W_KING.ToInt()] = (Bitboard)0x10;

		result.Pieces[Piece.B_PAWN.ToInt()] = result.Pieces[Piece.W_PAWN.ToInt()].ReverseBits64();
		result.Pieces[Piece.B_ROOK.ToInt()] = result.Pieces[Piece.W_ROOK.ToInt()].ReverseBits64();
		result.Pieces[Piece.B_KNIGHT.ToInt()] = result.Pieces[Piece.W_KNIGHT.ToInt()].ReverseBits64();
		result.Pieces[Piece.B_BISHOP.ToInt()] = result.Pieces[Piece.W_BISHOP.ToInt()].ReverseBits64();
		result.Pieces[Piece.B_QUEEN.ToInt()] = result.Pieces[Piece.W_KING.ToInt()].ReverseBits64();   // SIC!
		result.Pieces[Piece.B_KING.ToInt()] = result.Pieces[Piece.W_QUEEN.ToInt()].ReverseBits64();

		return result;
	}
}
