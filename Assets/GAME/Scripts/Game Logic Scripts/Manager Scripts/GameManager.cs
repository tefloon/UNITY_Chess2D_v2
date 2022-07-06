using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;
using ChessGUI;
using static PrecomputeTest;

public class GameManager : Singleton<GameManager>
{
    [Button]
    public void StartGame()
    {
        BoardManager.Instance.CreateBoard();
    }

    private void Start()
    {
        StartGame();
        print(knightMoves[4][2]);
    }
}
