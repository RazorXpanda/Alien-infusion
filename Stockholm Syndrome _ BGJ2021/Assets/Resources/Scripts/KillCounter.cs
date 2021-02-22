using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KillCounter : MonoBehaviour
{
    public static KillCounter instance = null;


    public Text killCounter;
    [SerializeField] private Text deadPanelText;
    private void Awake()
    {
        if (instance != null && instance !=this)
        {
            Destroy(gameObject);
        }
        instance = this;
    }

    public void OnTextUpdate()
    {
        killCounter.text = PlayerData.enemyKilled.ToString();
    }

    public void OnUITextUpdate()
    {
        deadPanelText.text = PlayerData.enemyKilled.ToString();
    }
}
