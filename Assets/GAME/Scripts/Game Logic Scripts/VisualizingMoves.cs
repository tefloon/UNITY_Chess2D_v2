using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bitboard = System.UInt64;


namespace ChessGUI
{
    public class VisualizingMoves : Singleton<VisualizingMoves>
    {
        Bitboard colorizedPreviously;

        EventsManager em;

        //private void OnEnable()
        //{
        //	em = EventsManager.Instance;
        //	em.StartPieceDraggingEvent += MovesVisualTest;
        //	em.FinishPieceDraggingEvent += UnColorizeSquares;
        //}

        //private void OnDisable()
        //{
        //	em.StartPieceDraggingEvent -= MovesVisualTest;
        //	em.FinishPieceDraggingEvent -= UnColorizeSquares;
        //}

        public void MovesVisualTest(Vector3 dummy)
        {
            Bitboard result = 0;
            int digit;

            for (int shift = 0; shift < 64; shift++)
            {
                digit = Random.Range(0, 2);
                if (digit == 1)
                {
                    result = ((Bitboard)1 << shift) | result;
                }
            }

            ColorizeSquares(result);
        }


        public void ColorizeSquares(Bitboard squares, bool putColor = true)
        {
            colorizedPreviously = squares;

            for (int shift = 0; shift < 64; shift++)
            {
                Bitboard squareB = (Bitboard)1 << shift;

                if ((squares & squareB) != 0)
                {
                    if (putColor) BoardManager.Instance.SquareScripts[squareB].GetComponent<SquareColoring>().HighlightSquare();
                    else BoardManager.Instance.SquareScripts[squareB].GetComponent<SquareColoring>().UnHighlightSquare();
                }
            }
        }

        public void UnColorizeSquares(Vector3 dummy)
        {
            ColorizeSquares(colorizedPreviously, false);
        }
    }
}
