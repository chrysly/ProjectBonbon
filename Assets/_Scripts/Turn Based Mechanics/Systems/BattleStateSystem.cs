using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, CHARSELECT, PATHSELECT, SKILLSELECT, ANIMATE, WIN, LOSE }

public class BattleStateSystem : MonoBehaviour
{

    [SerializeField] private SelectCharacter selector;
    [SerializeField] private CharacterPathHandler pathHandler;
    [SerializeField] private ControllerDisabler disabler;
    [SerializeField] private ActorMovementHandler animator;
    [SerializeField] private CursorManager cursorManager;

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

    #region Events
    public delegate void SkillSelected(SkillAction action, CharacterActor actor);
    public event SkillSelected OnSkillSelected;

    public delegate void SkillConfirm(bool canceled);
    public event SkillConfirm OnSkillConfirm;

    public delegate void WaypointAdded(Vector3 location, CharacterActor actor);
    public event WaypointAdded OnWaypointAdded;
    #endregion Events

    private SkillAction activeSkill;

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

    public void SwitchToCharacterSelect() {
        activeActor = null;
        battleState = BattleState.CHARSELECT;
        //pathHandler.DisableWaypoints();
        disabler.EnableControllers();
        Debug.Log("Switching to CHARSELECT");

        //EVENTS
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

    public void SwitchToPathSelect() {
        battleState = BattleState.PATHSELECT;
        Debug.Log("Switching to PATHSELECT");
    }

    private void PathSelect() {

        if (activeActor == null) {
            SwitchToCharacterSelect();
        }

        //pathHandler.DisplayWaypoints(activeActor);
        //TODO: Invoke OnActionUpdate
        if (Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftShift)) {
            Vector3 waypoint = pathHandler.AddWaypoint(activeActor);
            if (waypoint != Vector3.zero) {
                OnWaypointAdded?.Invoke(waypoint, activeActor);
            }
        } else if (Input.GetMouseButtonUp(1)) {
            pathHandler.UndoWaypoint(activeActor);
        }
    }

    public void SwitchToSkillSelect(SkillObject skill) {
        battleState = BattleState.SKILLSELECT;
        activeSkill = new SkillAction(skill);
        OnSkillSelected?.Invoke(activeSkill, activeActor);
        Debug.Log("Switching to SKILLSELECT: " + skill.GetSkillName());
    }

    private void SkillSelect() {

        if (activeActor == null) {
            SwitchToCharacterSelect();
            activeSkill = null;
            OnSkillConfirm?.Invoke(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            activeSkill = null;
            SwitchToCharacterSelect();
            OnSkillConfirm?.Invoke(true);
        }


        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100)) {
                SwitchToPathSelect();
                OnSkillConfirm?.Invoke(false);
                activeSkill.StoreLocation(hit.point);
            }
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