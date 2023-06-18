using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private LineController line;
    [SerializeField] private BattleStateSystem battleStateSystem;

    #region Booleans
    private bool isActive = false;
    private bool noReticle = false;
    private bool showLine = false;
    #endregion

    private GameObject activeCursor;

    public void CreateCursor(GameObject cursorPrefab) {
        isActive = true;
        activeCursor = Instantiate(cursorPrefab);
        battleStateSystem.OnSkillConfirm += DeleteCursor;
    }

    public void DeleteCursor() {
        if (activeCursor != null) {
            Destroy(activeCursor);
            ResetBooleans();
            battleStateSystem.OnSkillConfirm -= DeleteCursor;
        } else {
            Debug.LogError("Attempting to delete non-existent cursor");
        }
    }

    public void DrawCursor(Transform actor) {
        if (isActive) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100)) {
                UpdateCursor(hit.point, actor);
                
                //BOOLEAN METHODS
                if (showLine) {
                    DrawLine(actor, activeCursor.transform);
                }
            }
        }
    }

    private void UpdateCursor(Vector3 position, Transform actor) {
        position.y = actor.position.y;
        activeCursor.transform.position = position;
    }

    private void DrawLine(Transform source, Transform destination) {
        Transform[] points = { source, destination };
        line.DrawLine(points);
    }

    #region Boolean Logic
    public void EnableLine(bool enable) {
        line.gameObject.SetActive(enable);
        showLine = enable;
    }

    public void EnableNoReticle(bool enable) {
        noReticle = enable;
    }

    private void ResetBooleans() {  //MAKE SURE TO UPDATE WHEN ADDING NEW BOOLEANS
        isActive = false;
        EnableNoReticle(false);
        EnableLine(false);
    }
    #endregion
}