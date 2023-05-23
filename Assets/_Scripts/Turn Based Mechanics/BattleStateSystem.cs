using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, CHARSELECT, PATHSELECT, ENEMYTURN, WIN, LOSE }

public class BattleStateSystem : MonoBehaviour
{

    [SerializeField] private Transform selectorSource;
    [SerializeField] private Transform pathSource;
    private SelectCharacter selector;
    private CharacterPathHandler pathHandler;

    public BattleState battleState;

    #region Battle Actors
    [SerializeField] private List<CharacterActor> actorList;
    [SerializeField] private List<EnemyActor> enemyList;
    [SerializeField] private List<Transform> actorSpawnPoints;
    [SerializeField] private List<Transform> enemySpawnPoints;
    private CharacterActor activeActor;
    #endregion Battle Actors

    #region Battle Event Timing
    [SerializeField] private float switchToStartDelay = 1f;
    [SerializeField] private float battleStartAnimationDuration = 0.5f;
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
        UpdateActiveCharacter();
        HandleState();
    }

    private void HandleState() {
        switch (battleState) {
            case BattleState.START:
                StartCoroutine(BattleStart());
                break;
            case BattleState.CHARSELECT:
                CharacterSelection();
                break;
            case BattleState.PATHSELECT:
                PathSelect();
                break;
        }
    }

    private IEnumerator InitializeBattle() {

        selector = selectorSource.GetComponent<SelectCharacter>();
        pathHandler = pathSource.GetComponent<CharacterPathHandler>();

        //TODO: Initialize actors at actor spawn points
        yield return new WaitForSeconds(switchToStartDelay);

        SwitchToStart();
    }

    #region Game States
    private void SwitchToStart() {
        battleState = BattleState.START;
        Debug.Log("Switching to START");
    }

    private IEnumerator BattleStart() {
        //Battle intro animation
        yield return new WaitForSeconds(battleStartAnimationDuration);

        SwitchToCharacterSelect();
    }

    private void SwitchToCharacterSelect() {
        activeActor = null;
        battleState = BattleState.CHARSELECT;
        pathHandler.DisableWaypoints();
        Debug.Log("Switching to CHARSELECT");
    }

    private void CharacterSelection() {
        //something here

        if (activeActor != null) {
            SwitchToPathSelect();
        }
    }

    private void UpdateActiveCharacter() {
        activeActor = selector.getSelected();
    }

    private void SwitchToPathSelect() {
        battleState = BattleState.PATHSELECT;
        Debug.Log("Switching to PATHSELECT");
    }

    private void PathSelect() {

        if (activeActor == null) {
            SwitchToCharacterSelect();
        }

        pathHandler.DisplayWaypoints(activeActor);

        if (Input.GetMouseButtonDown(0)) {
            pathHandler.AddWaypoint(activeActor);
        } else if (Input.GetMouseButtonUp(1)) {
            pathHandler.UndoWaypoint(activeActor);
        }
    }

    #endregion Game States
}
