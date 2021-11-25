using UnityEngine;
using ChessGUI;
using System.Collections.Generic;

public class ArrowsManager : Singleton<ArrowsManager>
{
    [Range(3, 7)]
    public int ArrowHeadSize;

    [Range(2, 6)]
    public int ArrowMaxWidth;

    [Range(0.1f, 0.6f)]
    public float ArrowBodyPercentage;

    public Transform ArrowPrefab;
    public List<ArrowScript> Arrows = new List<ArrowScript>();

    private ArrowScript currentArrow;
    private ColoursManager palette;

    void Start()
    {
        palette = ColoursManager.Instance;
    }

    public void CreateArrow(Vector3 startPos, Vector3 endPos)
    {
        var arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
        ArrowScript arrowScript = arrow.GetComponent<ArrowScript>();

        if (!arrowScript) return;

        Arrows.Add(arrowScript);
        currentArrow = arrowScript;

        Color color = palette.CurrentColor;
        currentArrow.CreateArrow(startPos, endPos, color, ArrowHeadSize, ArrowMaxWidth, ArrowBodyPercentage);
    }

    public void DeleteArrows()
    {
        foreach (var arrow in Arrows)
        {
            Destroy(arrow.gameObject);
        }

        Arrows.Clear();
    }
}