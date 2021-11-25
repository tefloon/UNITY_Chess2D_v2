using UnityEngine;
using ChessGUI;
using System.Collections.Generic;

public class ArrowsManager : Singleton<ArrowsManager>
{
    #region Arrow Settings
    [Header("Arrows Objects")]
    [SerializeField] private Transform ArrowPrefab;
    [SerializeField] private Transform ArrowParent;


    [Header("Arrows Appearance")]
    [Range(3, 7)]
    [Tooltip("Numer of PIXELS from the tip to the base of the arrow head.")]
    [SerializeField] private int ArrowHeadLength;

    [Range(2, 6)]
    [Tooltip("Width of the arrow head in the widest place. Arrow body width is calculated as a fraction of this value.")]
    [SerializeField] private int ArrowMaxWidth;

    [Range(0.1f, 0.6f)]
    [Tooltip("Width of the arrow body as a FRACTION of the maximum arrow head width.")]
    [SerializeField] private float ArrowBodyPercentage;

    [Range(0.1f, 1.0f)]
    [Tooltip("Opacity of the arrow. Color itself is determined globally by Colour Manager.")]
    [SerializeField] private float ArrowOpacity;
    #endregion
    
    private List<ArrowScript> Arrows = new List<ArrowScript>();

    private ArrowScript currentArrow;
    private ColoursManager palette;

    void Start()
    {
        palette = ColoursManager.Instance;
    }

    public void CreateArrow(Vector3 startPos, Vector3 endPos)
    {
        var arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity, ArrowParent);
        ArrowScript arrowScript = arrow.GetComponent<ArrowScript>();

        if (!arrowScript) return;

        Arrows.Add(arrowScript);
        currentArrow = arrowScript;

        Color color = palette.CurrentColor;
        currentArrow.CreateArrow(startPos, endPos, color, ArrowHeadLength, ArrowMaxWidth, ArrowBodyPercentage, ArrowOpacity);
    }

    public void CreateArrow_new(Vector3 startPos, Vector3 endPos)
    {
        var arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity, ArrowParent);
        ArrowScript arrowScript = arrow.GetComponent<ArrowScript>();

        if (!arrowScript) return;

        Arrows.Add(arrowScript);
        currentArrow = arrowScript;

        Color color = palette.CurrentColor;
        currentArrow.CreateArrow(startPos, endPos, color, ArrowHeadLength, ArrowMaxWidth, ArrowBodyPercentage, ArrowOpacity);
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