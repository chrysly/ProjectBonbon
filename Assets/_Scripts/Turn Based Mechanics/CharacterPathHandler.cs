using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPathHandler : MonoBehaviour
{
    private CharacterActor actor;
    private IEnumerator activeWaypoint;

    [SerializeField] private float waypointCreationDelay = 0.5f; 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateActor(CharacterActor activeActor) {
        actor = activeActor;
    }

    public void AddWaypoint() {
        if (activeWaypoint == null) {
            activeWaypoint = WaypointOperation();
            StartCoroutine(WaypointOperation());
        }
    }

    private IEnumerator WaypointOperation() {
        if (actor != null) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //get the vector3 of mouse position and store in the queue

            yield return new WaitForSecondsRealtime(waypointCreationDelay);

            activeWaypoint = null;
            yield return null;
        } else {
            Debug.LogError("Actor has not properly updated within CharacterPathHandler.");
        }
    }
}
