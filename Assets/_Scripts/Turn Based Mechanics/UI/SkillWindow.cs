using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWindow : MonoBehaviour
{
    [SerializeField] private CanvasGroup panel;
    //temporary skill popup class because im tired and dont wanna actually implement ui rn
    public void ShowPanel() {
        panel.alpha = 1;
    }

    public void HidePanel() {
        panel.alpha = 0;
    }
}
