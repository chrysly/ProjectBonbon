using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActor : MonoBehaviour
{
    #region Attributes
    [SerializeField] private int health = 100;
    private int currentHealth;

    [SerializeField] private int baseAttackPower = 10;

    public LinkedList<Vector3> path;
    #endregion Attributes

    // Start is called before the first frame update
    void Start()
    {
        InitializeAttributes();
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    void InitializeAttributes() {
        currentHealth = health;
        path = new LinkedList<Vector3>();
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
}
