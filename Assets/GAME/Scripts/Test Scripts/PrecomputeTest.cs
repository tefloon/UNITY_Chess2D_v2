using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrecomputeTest
{
	public static int[] directionOffsets = { 8, -8, -1, 1, 7, -7, 9, -9 };
	public static readonly ulong[] knightAttackBitboards;
	public static int aaa;

	public static readonly byte[][] knightMoves;

	static PrecomputeTest()
	{
		int[] allKnightJumps = { 15, 17, -17, -15, 10, -6, 6, -10 };
		knightAttackBitboards = new ulong[64];
		knightMoves = new byte[64][];

		aaa = 7;

		for (int squareIndex = 0; squareIndex < 64; squareIndex++)
		{

			int y = squareIndex / 8;
			int x = squareIndex - y * 8;

			int north = 7 - y;
			int south = y;
			int west = x;
			int east = 7 - x;

			// Calculate all squares knight can jump to from current square
			var legalKnightJumps = new List<byte>();
			ulong knightBitboard = 0;
			foreach (int knightJumpDelta in allKnightJumps)
			{
				int knightJumpSquare = squareIndex + knightJumpDelta;
				if (knightJumpSquare >= 0 && knightJumpSquare < 64)
				{
					int knightSquareY = knightJumpSquare / 8;
					int knightSquareX = knightJumpSquare - knightSquareY * 8;
					// Ensure knight has moved max of 2 squares on x/y axis (to reject indices that have wrapped around side of board)
					int maxCoordMoveDst = System.Math.Max(System.Math.Abs(x - knightSquareX), System.Math.Abs(y - knightSquareY));
					if (maxCoordMoveDst == 2)
					{
						legalKnightJumps.Add((byte)knightJumpSquare);
						knightBitboard |= 1ul << knightJumpSquare;
					}
				}
			}
			knightMoves[squareIndex] = legalKnightJumps.ToArray();
			knightAttackBitboards[squareIndex] = knightBitboard;
		}
	}
}
