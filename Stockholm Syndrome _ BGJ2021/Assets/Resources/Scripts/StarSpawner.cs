using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] starHolder;
    [SerializeField] private GameObject starSprite;
    [SerializeField] private AnimationClip clip;
    private float animDuration;

    private void Start()
    {
        StartCoroutine(StarSpawn());
        animDuration = clip.length;
    }

    IEnumerator StarSpawn()
    {
        while (true)
        {
            int rand = Random.Range(0, starHolder.Length - 1);
            Animator anim = starHolder[rand].GetComponent<Animator>();
            anim.SetBool("isSpawn", true);
            Debug.Log("Is play");
            yield return new WaitForSeconds(animDuration);
            anim.SetBool("isSpawn", false);
            yield return new WaitForSeconds(1f);
        }
    }
}
