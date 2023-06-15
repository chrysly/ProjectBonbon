using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveAction : MonoBehaviour, ActorAction
{
    [SerializeField] private int cost = 1;
    Vector3 location;

    public void StoreLocation(Vector3 location) {
        this.location = location;
    }

    public void RunAction(Transform actor, float duration) {
        actor.DOMove(location, duration);
    }

    public int GetCost() {
        return cost;
    }
}
