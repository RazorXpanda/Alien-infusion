using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : MonoBehaviour
{
    public void onEventTrigger()
    {
        GameObject parent = this.transform.parent.gameObject;
        Destroy(parent);
        Destroy(gameObject);
    }
}
