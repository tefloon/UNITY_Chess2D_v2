using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceManager : Singleton<PieceManager>
{
    [Range(1f, 10f)]
    public float AnimationSpeed;
}
