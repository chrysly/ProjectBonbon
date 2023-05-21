using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterActor : MonoBehaviour
{
    #region Attributes
    [SerializeField] private int health = 100;
    private int currentHealth;

    [SerializeField] private int baseAttackPower = 10;
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
    }

    void Damage(int damage) {
        currentHealth -= damage;
    }

    void Restore(int health) {
        currentHealth += health;
    }

    void Attack(Transform target) {
        CharacterActor actor = target.GetComponent<CharacterActor>();
        actor.Damage(baseAttackPower);
    }
}
