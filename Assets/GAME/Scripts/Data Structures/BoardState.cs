using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardState : MonoBehaviour
{
	public Position Position;
	public List<ArrowScript> Arrows;
	public SquareBackgroundColor[] SquareColors = new SquareBackgroundColor[64];
	public float[] Time = new float[2];
}
