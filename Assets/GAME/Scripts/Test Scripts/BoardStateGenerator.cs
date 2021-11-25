using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitboard = System.UInt64;

namespace ChessDebug
{
    public class BoardStateGenerator : Singleton<BoardStateGenerator>
    {
        public BoardState GenerateRandomBoardState()
        {
            return new BoardState();
        }

        private Position GenerateRandomPosition()
        {
            Position result = new Position();

            for (Square s = Square.SQ_A1; s < Square.SQUARE_NB; s++)
            {

            }

            return result;
        }
    }
}
