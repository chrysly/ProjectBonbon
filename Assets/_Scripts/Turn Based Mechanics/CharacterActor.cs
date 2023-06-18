using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterActor : MonoBehaviour
{
    #region Attributes
    [SerializeField] private int health = 100;
    [SerializeField] private int maxActionPoints = 3;
    [SerializeField] private float durationBetweenWaypoints = 2f;
    [SerializeField] private List<SkillObject> skillList;

    //consumed when using attacks or moving
    private int actionPoints;
    
    private int currentHealth;

    [SerializeField] private int baseAttackPower = 10;

    public LinkedList<ActorAction> actionList;
    #endregion Attributes

    // Start is called before the first frame update
    void Start()
    {
        InitializeAttributes();
    }

    void InitializeAttributes() {
        currentHealth = health;
        actionPoints = maxActionPoints;
        actionList = new LinkedList<ActorAction>();
        DOTween.Init();
    }

    public void Damage(int damage) {
        currentHealth -= damage;
    }

    public void Restore(int health) {
        currentHealth += health;
    }

    public void AppendAction(ActorAction action) {
        if (HasRemainingActionPoints(action.GetCost())) {
            actionList.AddLast(action);
            ConsumeActionPoints(action.GetCost());
            if (action.GetCost() > 1) {
                for (int i = 0; i < action.GetCost() - 1; i++) {
                    actionList.AddLast(new InactiveAction());
                }
            }
        }
    }

    public void UndoLastAction() {
        if (actionList.Count > 0) {
            int costToReturn = 1;
            while (actionList.Last.Value is InactiveAction) {
                costToReturn++;
                actionList.RemoveLast();
            }
            actionList.RemoveLast();
        }
    }

    public void RunNextAction(float duration) {
        if (actionList.Count > 0) {
            actionList.First.Value.RunAction(transform, duration);
            actionList.RemoveFirst();
        } else {
            Debug.LogError("Next action called on empty action list");
        }
    }

    public bool ActionQueueIsEmpty() {
        return actionList.Count <= 0;
    }

    public bool HasRemainingActionPoints() {
        return actionPoints > 0;
    }

    public bool HasRemainingActionPoints(int cost) {
        return actionPoints - cost > 0;
    }

    public void ConsumeActionPoints(int points) {
        if (actionPoints - points < 0) {
            Debug.LogError("Prompting action that costs more than the current character's action points");
        }
    }

    public void ReturnActionPoints(int points) {
        actionPoints += points;
    }

    public void ResetActionPoints() {
        actionPoints = maxActionPoints;
    }

    public List<SkillObject> GetSkillList() {
        return skillList;
    }
}
