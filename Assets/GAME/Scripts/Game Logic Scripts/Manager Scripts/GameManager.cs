using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BitStrap;
using ChessGUI;

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
    }
}
