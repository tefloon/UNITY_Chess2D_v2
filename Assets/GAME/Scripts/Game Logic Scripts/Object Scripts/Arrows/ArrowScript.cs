using UnityEngine;
using ChessGUI;


[RequireComponent(typeof(LineRenderer))]
public class ArrowScript : Singleton<ArrowsManager>
{
	private Vector3 ArrowOrigin;
	private Vector3 ArrowTarget;

	public float PercentBody = 0.4f;
	public float HeadLength = 6f;

	private LineRenderer lr;

	private void OnEnable()
	{
		lr = GetComponent<LineRenderer>();
	}

	public void CreateArrow(Vector3 startPosition, Vector3 endPosition, Color color, float ArrowHeadSize, float ArrowMaxWidth, float ArrowBodyPercentage)
	{
		lr.widthMultiplier = ArrowMaxWidth;
		PercentBody = ArrowBodyPercentage;
		HeadLength = ArrowHeadSize;


		lr.material.color = color;
		ArrowOrigin = startPosition;
		ArrowTarget = endPosition;

		UpdateArrow();

	}

	void UpdateArrow()
	{

		Vector3 dir = (ArrowTarget - ArrowOrigin).normalized;
		Vector3 bodyEnd = dir * HeadLength;
		Vector3 headStart = dir * (HeadLength - 0.01f);

		float percentBodyEnd = Vector3.Distance(ArrowOrigin, ArrowTarget - bodyEnd) / Vector3.Distance(ArrowOrigin, ArrowTarget);
		float percentHeadStart = Vector3.Distance(ArrowOrigin, ArrowTarget - headStart) / Vector3.Distance(ArrowOrigin, ArrowTarget);


		if (lr == null)
			lr = this.GetComponent<LineRenderer>();

		lr.SetPositions(new Vector3[]
		{
			ArrowOrigin,
			ArrowTarget - bodyEnd,
			ArrowTarget - headStart,
			ArrowTarget
		});

		lr.widthCurve = new AnimationCurve(
			new Keyframe(0, PercentBody),
			new Keyframe(percentBodyEnd, PercentBody),
			new Keyframe(percentHeadStart, 1f),
			new Keyframe(1f, 0f)
			);
	}
}