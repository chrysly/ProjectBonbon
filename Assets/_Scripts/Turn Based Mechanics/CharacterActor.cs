using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterActor : MonoBehaviour
{
    #region Attributes
    [SerializeField] private int health = 100;
    [SerializeField] private float durationBetweenWaypoints = 2f;
    private int currentHealth;

    [SerializeField] private int baseAttackPower = 10;

    public LinkedList<Vector3> path;
    #endregion Attributes

    Sequence movePath;

    // Start is called before the first frame update
    void Start()
    {
        InitializeAttributes();
    }

    void InitializeAttributes() {
        currentHealth = health;
        path = new LinkedList<Vector3>();
        DOTween.Init();
    }

    public void Damage(int damage) {
        currentHealth -= damage;
    }

    public void Restore(int health) {
        currentHealth += health;
    }

    public void Attack(Transform target) {
        CharacterActor actor = target.GetComponent<CharacterActor>();
        actor.Damage(baseAttackPower);
    }

    public void RunMoveSequence() {
        if (path.Count > 0) {

            //TEMPORARY FIX, PREVENT WAYPOINT CREATION WHEN CLICKING "EXECUTE"
            if (path.Count < 6) {
                path.RemoveLast();
            }

            movePath = DOTween.Sequence();
            int i = 0;
            while (path.Count > 0) {
                Debug.Log("Running waypoint " + i);
                movePath.Append(transform.DOMove(path.First.Value, durationBetweenWaypoints));
                path.RemoveFirst();
                i++;
            }
            movePath = null;
        }
    }
}
