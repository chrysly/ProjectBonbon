using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, CHARSELECT, PATHSELECT, ENEMYTURN, WIN, LOSE }

public class BattleStateSystem : MonoBehaviour
{

    [SerializeField] private Transform activeCamera;
    private SelectCharacter selector;

    public BattleState battleState;

    #region Battle Actors
    [SerializeField] private List<CharacterActor> actorList;
    [SerializeField] private List<EnemyActor> enemyList;
    [SerializeField] private List<Transform> actorSpawnPoints;
    [SerializeField] private List<Transform> enemySpawnPoints;
    #endregion Battle Actors

    #region Battle Event Timing
    [SerializeField] private float switchToStartDelay = 2f;
    [SerializeField] private float battleStartAnimationDuration = 2f;
    [SerializeField] private float characterSelectTransitionDelay = 1f;
    #endregion Battle Event Timing

    // Start is called before the first frame update
    void Start()
    {
        battleState = BattleState.START;
        StartCoroutine(InitializeBattle());
    }

    // Update is called once per frame
    void Update()
    {
        HandleState();
    }

    private void HandleState() {
        switch (battleState) {
            case BattleState.START:
                BattleStart();
                break;
        }
    }

    private IEnumerator InitializeBattle() {

        selector = activeCamera.GetComponent<SelectCharacter>();

        //TODO: Initialize actors at actor spawn points
        yield return new WaitForSeconds(switchToStartDelay);

        SwitchToStart();
    }

    #region Game States
    private void SwitchToStart() {
        battleState = BattleState.START;
    }

    private IEnumerator BattleStart() {
        //Battle intro animation
        yield return new WaitForSeconds(battleStartAnimationDuration);

        SwitchToCharacterSelect();
    }

    private void SwitchToCharacterSelect() {
        battleState = BattleState.CHARSELECT;
    }

    private void CharacterSelection() {
        //something here
    }

    private CharacterActor GetActiveCharacter() {
        return selector.getSelected();
    }

    private void SwitchToPathSelect() {
        battleState = BattleState.PATHSELECT;
    }

    private void PathSelect() {

    }

    #endregion Game States
}
