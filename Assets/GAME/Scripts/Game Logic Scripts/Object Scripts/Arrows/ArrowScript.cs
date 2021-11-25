using UnityEngine;
using ChessGUI;


[RequireComponent(typeof(LineRenderer))]
public class ArrowScript : MonoBehaviour
{
	[SerializeField] private Vector3 ArrowOrigin;
	[SerializeField] private Vector3 ArrowTarget;

	[SerializeField] private Vector3 bodyEnd;
	[SerializeField] private Vector3 headStart;
	[SerializeField] private Vector3 dir;

	public float PercentBody = 0.4f;
	public float HeadLength = 6f;

	private LineRenderer lr;

	private void OnEnable()
	{
		lr = GetComponent<LineRenderer>();
	}

	public void CreateArrow(Vector3 startPosition, Vector3 endPosition, Color color, float ArrowHeadLength, float ArrowMaxWidth, float ArrowBodyPercentage, float ArrowOpacity)
	{
		lr.widthMultiplier = ArrowMaxWidth;
		PercentBody = ArrowBodyPercentage;
		HeadLength = ArrowHeadLength;

		color.a = ArrowOpacity;
		lr.material.color = color;
		ArrowOrigin = startPosition;
		ArrowTarget = endPosition;

		UpdateArrow();
	}	
	public void CreateArrow_new(Vector3 startPosition, Vector3 endPosition, Color color, float ArrowHeadLength, float ArrowMaxWidth, float ArrowBodyPercentage, float ArrowOpacity)
	{
		lr.widthMultiplier = ArrowMaxWidth;
		PercentBody = ArrowBodyPercentage;
		HeadLength = ArrowHeadLength;

		color.a = ArrowOpacity;
		lr.material.color = color;
		ArrowOrigin = startPosition / BoardBuilder.Instance.BoardScaleFactor;
		ArrowTarget = endPosition / BoardBuilder.Instance.BoardScaleFactor; 

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

	void UpdateArrow_new()
	{

		dir = -(ArrowTarget - ArrowOrigin).normalized;
		bodyEnd = ArrowTarget + dir * (HeadLength/12 - 0.01f);
		headStart = ArrowTarget + dir * (HeadLength/12);

		float percentBodyEnd = Vector3.Distance(ArrowOrigin, bodyEnd) / Vector3.Distance(ArrowOrigin, ArrowTarget);
		float percentHeadStart = Vector3.Distance(ArrowOrigin, headStart) / Vector3.Distance(ArrowOrigin, ArrowTarget);


		if (lr == null)
			lr = this.GetComponent<LineRenderer>();

		lr.SetPositions(new Vector3[]
		{
			ArrowOrigin,
			bodyEnd,
			headStart,
			ArrowTarget
		});

		lr.widthCurve = new AnimationCurve(
			new Keyframe(0, PercentBody),
			new Keyframe( percentHeadStart, PercentBody),
			new Keyframe(percentBodyEnd, 1f),
			new Keyframe(1f, 0f)
			);
	}
}