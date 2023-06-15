using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAction : MonoBehaviour, ActorAction
{
    [SerializeField] private int cost = 2;
    [SerializeField] private CursorType cursor;

    public void StoreCursor(CursorType cursor) {
        this.cursor = cursor;
    }

    public void RunAction(Transform actor, float duration) {
        //TODO: RunAction will likely pull data from a ScriptableObject
    }

    public int GetCost() {
        return cost;
    }
}
