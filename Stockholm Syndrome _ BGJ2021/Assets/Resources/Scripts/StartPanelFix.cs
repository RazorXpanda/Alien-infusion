using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanelFix : MonoBehaviour
{
    void OnFunctionTrigger()
    {
        RectTransform rect = GetComponent<RectTransform>();
        rect.localScale = new Vector3(0,0,0);
    }
}
