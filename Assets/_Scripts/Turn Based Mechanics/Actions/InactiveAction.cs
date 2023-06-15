using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveAction : MonoBehaviour, ActorAction
{
    [SerializeField] private int cost = 1;

    public void RunAction(Transform actor, float duration) {
        //Action when actor is inactive
    }

    public int GetCost() {
        return cost;
    }
}
