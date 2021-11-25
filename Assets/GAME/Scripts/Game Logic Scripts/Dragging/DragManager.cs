using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGUI
{
	public class DragManager : MonoBehaviour
	{
		public bool enableHover;

		private Transform draggedPiece;
		private Transform startingSquare;
		private Transform hoveredSquare;
		private Transform endSquare;

		private bool isDraggingPiece;
		private bool isDraggingArrow;

		EventsManager em;
		ArrowsManager am;

		private void OnEnable()
		{
			em = EventsManager.Instance;
			am = ArrowsManager.Instance;

			em.StartPieceDraggingEvent += StartPieceDrag;
			em.FinishPieceDraggingEvent += FinishPieceDrag;
			em.StartArrowDraggingEvent += StartArrowDrag;
			em.FinishArrowDraggingEvent += FinishArrowDrag;
		}

		private void OnDisable()
		{
			em.StartPieceDraggingEvent -= StartPieceDrag;
			em.FinishPieceDraggingEvent -= FinishPieceDrag;
			em.StartArrowDraggingEvent -= StartArrowDrag;
			em.FinishArrowDraggingEvent -= FinishArrowDrag;
		}

		public void StartPieceDrag(Vector3 mousePosition)
		{
			AbortArrowDrag();

			startingSquare = FindSquare(mousePosition);
			if (!startingSquare) return;

			draggedPiece = FindPiece(mousePosition);
			if (!draggedPiece) return;

			isDraggingPiece = true;
		}



		public void FinishPieceDrag(Vector3 mousePosition)
		{
			if (!isDraggingPiece) return;   // We were not dragging
			isDraggingPiece = false;

			endSquare = FindSquare(mousePosition);

			// We dragged the piece to the same square or outside the board - reverting
			if (startingSquare == endSquare || !endSquare)
			{
				AbortPieceDrag();
				return;
			}

			draggedPiece.position = endSquare.position;

			ResetDragState();
		}

		private void AbortPieceDrag()
		{
			isDraggingPiece = false;
			if (draggedPiece) draggedPiece.position = startingSquare.position;

			ResetDragState();
		}

		public void StartArrowDrag(Vector3 mousePosition)
		{
			// If we were in the process of dragging a piece, abort it
			AbortPieceDrag();

			// Find the square we start our annotation drawing on
			startingSquare = FindSquare(mousePosition);
			hoveredSquare = startingSquare;

			// If this square is out of bounds or otherwise invalid, abort
			if (!startingSquare) return;

			// Highlight the clicked square
			startingSquare.GetComponent<SquareColoring>().HighlightSquare();

			isDraggingArrow = true;
		}

		public void FinishArrowDrag(Vector3 mousePosition)
		{
			isDraggingArrow = false;
			hoveredSquare = null;
			endSquare = FindSquare(mousePosition);

			if (!startingSquare) return;
			// UnHighlight the square we started on and the end square
			startingSquare.GetComponent<SquareColoring>().UnHighlightSquare();

			if (!endSquare) return;
			endSquare.GetComponent<SquareColoring>().UnHighlightSquare();

			if (startingSquare == endSquare)
			{
				endSquare.GetComponent<SquareColoring>().ColorizeSquare();
			}
			else
			{
				am.CreateArrow(startingSquare.position, endSquare.position);
			}

			ResetDragState();
		}

		private void AbortArrowDrag()
		{
			isDraggingArrow = false;
			am.DeleteArrows();
			ResetDragState();
		}

		private void ResetDragState()
		{
			startingSquare = null;
			hoveredSquare = null;
			endSquare = null;
			draggedPiece = null;
		}

		private void Update()
		{
			ManageArrowDragging();
			ManagePieceDragging();
		}

		private void ManagePieceDragging()
		{
			if (isDraggingPiece)
			{
				Vector3 newMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				newMousePosition.z = 0;
				draggedPiece.position = newMousePosition;
			}
		}

		void ManageArrowDragging()
		{
			if (isDraggingArrow)
			{
				Transform currentHoveredSquare = FindSquare(Input.mousePosition);

				if (!currentHoveredSquare) return;

				if (currentHoveredSquare != hoveredSquare)
				{
					if (hoveredSquare != startingSquare)
					{
						hoveredSquare.GetComponent<SquareColoring>().UnHighlightSquare();
					}

					currentHoveredSquare.GetComponent<SquareColoring>().HighlightSquare();
					hoveredSquare = currentHoveredSquare;
				}
			}
		}

		private Transform[] MouseRaycast(LayerMask _mask, Vector3 mousePosition)
		{
			Ray ray = Camera.main.ScreenPointToRay(mousePosition);

			RaycastHit2D[] results = Physics2D.GetRayIntersectionAll(ray, float.MaxValue, _mask);
			int numberOfHits = results.Length;

			Transform[] resultsTransforms = new Transform[numberOfHits];

			for (int i = 0; i < numberOfHits; i++)
			{
				resultsTransforms[i] = results[i].transform;

			}

			return resultsTransforms;
		}

		private Transform FindPiece(Vector3 mousePosition)
		{
			Transform[] piecesTransforms = MouseRaycast(LayerMask.GetMask("Pieces"), mousePosition);

			if (piecesTransforms.Length > 0) return piecesTransforms[0];
			return null;
		}

		private Transform FindSquare(Vector3 mousePosition)
		{
			Transform[] squareTransforms = MouseRaycast(LayerMask.GetMask("Squares"), mousePosition);

			if (squareTransforms.Length > 0) return squareTransforms[0];
			return null;
		}
	}
}
