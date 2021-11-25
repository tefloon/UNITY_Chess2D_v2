using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChessGUI
{
    public class EventsManager : Singleton<EventsManager>
    {
        public delegate void DragEventHandler(Vector3 mousePosition);

        public event DragEventHandler StartPieceDraggingEvent;
        public event DragEventHandler FinishPieceDraggingEvent;
        public event DragEventHandler StartArrowDraggingEvent;
        public event DragEventHandler FinishArrowDraggingEvent;


        public void CallStartPieceDraggingEvent(Vector3 mousePosition)
        {
            if (StartPieceDraggingEvent != null)
            {
                StartPieceDraggingEvent(mousePosition);
            }
        }

        public void CallFinishPieceDraggingEvent(Vector3 mousePosition)
        {
            if (FinishPieceDraggingEvent != null)
            {
                FinishPieceDraggingEvent(mousePosition);
            }
        }

        public void CallStartArrowDraggingEvent(Vector3 mousePosition)
        {
            if (StartArrowDraggingEvent != null)
            {
                StartArrowDraggingEvent(mousePosition);
            }
        }

        public void CallFinishArrowDraggingEvent(Vector3 mousePosition)
        {
            if (FinishArrowDraggingEvent != null)
            {
                FinishArrowDraggingEvent(mousePosition);
            }
        }
    }
}
