using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState { START, CHARSELECT, PATHSELECT, SKILLSELECT, ANIMATE, WIN, LOSE }

public class BattleStateSystem : MonoBehaviour
{

    [SerializeField] private SelectManager selector;
    [SerializeField] private CharacterPathHandler pathHandler;
    [SerializeField] private ControllerDisabler disabler;
    [SerializeField] private ActorMovementHandler animator;
    [SerializeField] private CursorManager cursorManager;

    private IEnumerator activeAnimationCycle;
    private int maxSteps;
    private int animationStep; 

    public BattleState battleState;

    #region Battle Actors
    [SerializeField] private List<CharacterActor> actorList;
    [SerializeField] private List<EnemyActor> enemyList;
    private CharacterActor activeActor = null;
    private List<CharacterActor> actorQueue;

    [SerializeField] private float animationCycleDuration = 0.5f;

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

    public delegate void UndoAction();
    public event UndoAction OnUndoAction;

    public delegate void Selection(bool enable);
    public event Selection OnSwitchState;
    #endregion Events

    private SkillAction activeSkill;

    // Start is called before the first frame update
    void Start()
    {
        battleState = BattleState.START;
        selector.OnSelect += SwitchToPathSelect;
        selector.OnDeselect += SwitchToCharacterSelect;
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
                Animate();
                break;
        }
    }

    private IEnumerator InitializeBattle() {

        maxSteps = actorList.Count;
        animationStep = 0;

        actorQueue = new List<CharacterActor>(actorList);
        actorQueue.Sort();

        //TODO: Initialize actors at actor spawn points
        yield return new WaitForSeconds(switchToStartDelay);

        SwitchToStart();

        yield return null;
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

        yield return null;
    }

    public void SwitchToCharacterSelect() {
        if (battleState != BattleState.ANIMATE) {
            battleState = BattleState.CHARSELECT;
            //pathHandler.DisableWaypoints();
            disabler.EnableControllers();
            Debug.Log("Switching to CHARSELECT");
            OnSwitchState.Invoke(true);
        }

        //EVENTS
    }

    private void CharacterSelection() {
        //something here
    }

    public void SwitchToPathSelect(CharacterActor actor) {
        activeActor = actor;
        battleState = BattleState.PATHSELECT;
        activeSkill = null;
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
            if (waypoint != Vector3.zero && !activeActor.HasExistingMoveAction()) {
                OnWaypointAdded?.Invoke(waypoint, activeActor);
            }
        } else if (Input.GetMouseButtonUp(1)) {
            OnUndoAction?.Invoke();
            pathHandler.UndoWaypoint(activeActor);
        }
    }

    public void SwitchToSkillSelect(SkillObject skill) {
        if (activeActor.HasAvailableActions()) {
            battleState = BattleState.SKILLSELECT;
            activeSkill = new SkillAction(skill, activeActor.transform);
            OnSkillSelected?.Invoke(activeSkill, activeActor);
            Debug.Log("Switching to SKILLSELECT: " + skill.GetSkillName());
        }
    }

    private void SkillSelect() {

        if (activeActor == null) {
            SwitchToCharacterSelect();
            activeSkill = null;
            OnSkillConfirm?.Invoke(true);
        }

        if (Input.GetMouseButtonDown(1)) {
            activeSkill = null;
            SwitchToCharacterSelect();
            OnUndoAction?.Invoke();
        }


        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100)) {
                OnSkillConfirm?.Invoke(false);
                activeSkill.StoreLocation(hit.point);
                activeActor.AppendAction(activeSkill);
                SwitchToPathSelect(activeActor);
            }
        }
    }

    public void SwitchToAnimate() {
        //activeActor = null;
        battleState = BattleState.ANIMATE;
        //pathHandler.DisableWaypoints();
        disabler.DisableControllers();
        OnSwitchState.Invoke(false);
    }

    private void Animate() {
        if (activeAnimationCycle == null) {
            activeAnimationCycle = AnimateBattle();
            StartCoroutine(AnimateBattle());
        }
    }

    private IEnumerator AnimateBattle() {
        //additional actions during animate state

        if (animationStep >= maxSteps) {
            SwitchToCharacterSelect();
        }
        for (int i = 0; i <= animationStep; i++) {
            CharacterActor actor = actorQueue[i];
            if (actor.GetActionList().Count > 0) {
                actor.RunNextAction(animationCycleDuration);
            }
        }
        animationStep++;

        yield return new WaitForSeconds(animationCycleDuration + 0.1f);

        activeAnimationCycle = null;
        yield return null;
    }

    #endregion Game States
}
