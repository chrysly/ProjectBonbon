using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE }

public class BattleStateSystem : MonoBehaviour
{

    public BattleState battleState;

    #region Battle Actors
    [SerializeField] private List<CharacterActor> actorList;
    [SerializeField] private List<EnemyActor> enemyList;
    #endregion Battle Actors

    // Start is called before the first frame update
    void Start()
    {
        battleState = BattleState.START;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitializeBattle() {

    }
}
