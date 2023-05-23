using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathHandler : MonoBehaviour
{
    private IEnumerator activeAction;

    [SerializeField] private float waypointCreationDelay = 0.5f; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckForRaycast(CharacterActor actor) {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 100)) {
            AddWaypoint(actor, hit.point);
        }
    }

    public void AddWaypoint(CharacterActor actor, Vector3 cursorPosition) {
        if (activeAction == null) {
            activeAction = WaypointOperation(actor, cursorPosition);
            StartCoroutine(WaypointOperation(actor, cursorPosition));
        }
    }

    private IEnumerator WaypointOperation(CharacterActor actor, Vector3 cursorPosition) {
        if (actor != null) {
            //get the vector3 of mouse position and store in the queue
            actor.path.AddLast(cursorPosition);

            yield return new WaitForSecondsRealtime(waypointCreationDelay);

            activeAction = null;
            yield return null;
        } else {
            Debug.LogError("Actor has not properly updated within CharacterPathHandler.");
        }
    }

    public void UndoWaypoint(CharacterActor actor) {
        if (activeAction == null) {
            activeAction = UndoWaypointOperation(actor);
            StartCoroutine(UndoWaypointOperation(actor));
        }
    }

    private IEnumerator UndoWaypointOperation(CharacterActor actor) {
        if (actor != null) {
            if (actor.path.Count > 0) {
                actor.path.RemoveLast();

                yield return new WaitForSecondsRealtime(waypointCreationDelay);

                activeAction = null;
                yield return null;
            } 
        }
    }

    public void DisplayWaypoints(CharacterActor actor) {
        if (actor.path.Count > 0) {
            for (LinkedListNode<Vector3> node = actor.path.First; node != null; node = node.Next) {
                Gizmos.color = Color.blue;
                Gizmos.DrawSphere(node.Value, 2f);
            }
        }
    }
}
