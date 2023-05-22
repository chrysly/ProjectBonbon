using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WIN, LOSE }

public class BattleStateSystem : MonoBehaviour
{

    public BattleState battleState;
    public Transform activeActor;

    #region Battle Actors
    [SerializeField] private List<CharacterActor> actorList;
    [SerializeField] private List<EnemyActor> enemyList;
    [SerializeField] private List<Transform> actorSpawnPoints;
    [SerializeField] private List<Transform> enemySpawnPoints;
    #endregion Battle Actors

    // Start is called before the first frame update
    void Start()
    {
        battleState = BattleState.START;
        InitializeBattle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator InitializeBattle() {

        //TODO: Initialize actors at actor spawn points
        yield return new WaitForSeconds(3f);
    }

    #region Game States


    #endregion Game States
}
