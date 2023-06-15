using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ActorAction {
    public void RunAction(Transform actor, float duration);
    public int GetCost();
}
