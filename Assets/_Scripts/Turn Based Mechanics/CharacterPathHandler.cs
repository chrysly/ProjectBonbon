using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathHandler : MonoBehaviour
{
    private IEnumerator activeAction;

    [SerializeField] private Transform waypointsContainer;
    private int maxWaypoints;

    [SerializeField] private float waypointCreationDelay = 0.5f; 

    void Start()
    {
        maxWaypoints = waypointsContainer.childCount;
    }

    public void AddWaypoint(CharacterActor actor) {
        if (actor.path.Count <= maxWaypoints) {
            Debug.Log("Added");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100)) {
                WaypointAction(actor, hit.point);
            }
        }
    }

    public void WaypointAction(CharacterActor actor, Vector3 cursorPosition) {
        if (activeAction == null) {
            activeAction = WaypointOperation(actor, cursorPosition);
            StartCoroutine(WaypointOperation(actor, cursorPosition));
        }
    }

    private IEnumerator WaypointOperation(CharacterActor actor, Vector3 cursorPosition) {
        if (actor != null) {
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
                waypointsContainer.GetChild(actor.path.Count).gameObject.SetActive(false);

                yield return new WaitForSecondsRealtime(waypointCreationDelay);

                activeAction = null;
                yield return null;
            } 
        }
    }

    public void DisplayWaypoints(CharacterActor actor) {
        if (actor.path.Count > 0) {
            int index = 0;
            for (LinkedListNode<Vector3> node = actor.path.First; node != null; node = node.Next, index++) {
                Debug.Log(index);
                waypointsContainer.GetChild(index).position = node.Value;
                waypointsContainer.GetChild(index).gameObject.SetActive(true);
            }
        }
    }

    public void DisableWaypoints() {
        Debug.Log("Disabled");
        for (int i = 0; i < waypointsContainer.childCount; i++) {
            waypointsContainer.GetChild(i).gameObject.SetActive(false);
        }
    }
}
