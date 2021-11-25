using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ChessGUI
{
    public class MouseInputManager : MonoBehaviour
    {
        EventsManager em;

        // Start is called before the first frame update
        void OnEnable()
        {
            em = EventsManager.Instance;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                em.CallStartPieceDraggingEvent(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                em.CallFinishPieceDraggingEvent(Input.mousePosition);
            }


            if (Input.GetMouseButtonDown(1))
            {
                em.CallStartArrowDraggingEvent(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(1))
            {
                em.CallFinishArrowDraggingEvent(Input.mousePosition);
            }
        }
    }
}
