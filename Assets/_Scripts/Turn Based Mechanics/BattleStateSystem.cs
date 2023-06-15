using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, CHARSELECT, PATHSELECT, SKILLSELECT, ANIMATE, WIN, LOSE }

public class BattleStateSystem : MonoBehaviour
{

    [SerializeField] private Transform selectorSource;
    [SerializeField] private Transform pathSource;
    [SerializeField] private Transform disablerSource;
    [SerializeField] private Transform animatorSource;
    private SelectCharacter selector;
    private CharacterPathHandler pathHandler;
    private ControllerDisabler disabler;
    private ActorMovementHandler animator;

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
            case BattleState.SKILLSELECT:
                SkillSelect();
                break;
            case BattleState.ANIMATE:
                StartCoroutine(AnimateBattle());
                break;
        }
    }

    private IEnumerator InitializeBattle() {

        selector = selectorSource.GetComponent<SelectCharacter>();
        pathHandler = pathSource.GetComponent<CharacterPathHandler>();
        disabler = disablerSource.GetComponent<ControllerDisabler>();
        animator = animatorSource.GetComponent<ActorMovementHandler>();

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
        //pathHandler.DisableWaypoints();
        disabler.EnableControllers();
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

        //pathHandler.DisplayWaypoints(activeActor);

        if (Input.GetMouseButtonDown(0)) {
            pathHandler.AddWaypoint(activeActor);
        } else if (Input.GetMouseButtonUp(1)) {
            pathHandler.UndoWaypoint(activeActor);
        }
    }

    private void SwitchToSkillSelect() {
        battleState = BattleState.SKILLSELECT;
        Debug.Log("Switching to SKILLSELECT");
    }

    private void SkillSelect() {

        if (activeActor == null) {
            SwitchToCharacterSelect();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SwitchToCharacterSelect();
        }
    }

    public void SwitchToAnimate() {
        activeActor = null;
        battleState = BattleState.ANIMATE;
        //pathHandler.DisableWaypoints();
        disabler.DisableControllers();
    }

    private IEnumerator AnimateBattle() {
        //additional actions during animate state
        animator.RunAllMoveSequences();

        yield return new WaitForSeconds(animator.getDelay());

        SwitchToCharacterSelect();
    }

    #endregion Game States
}
