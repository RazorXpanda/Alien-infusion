using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    public List<AudioClip> m_audioClips = new List<AudioClip>();
    public List<Toggle> _toggles = new List<Toggle>();
    [SerializeField] private AudioSource m_audioSource;
    public ToggleGroup toggleGroup;

    private void Awake()
    {
        m_audioSource = GetComponent<AudioSource>();
        #region outdatedcode
        //random
        // m_audioSource = GetComponent<AudioSource>();
        // AudioClip clip =  m_audioClips[Random.Range(0, m_audioClips.Count)];
        // m_audioSource.clip = clip;
        // m_audioSource.Play();    
        //toggleGroup.SetAllTogglesOff();  
        #endregion
        StartCoroutine(MusicOnStart()); 
    }

    public void OnMusicItemToggleClicked(int toggleID)
    {
        m_audioSource.Stop();
        AudioClip clip =  m_audioClips[toggleID];
        m_audioSource.clip = clip;
        m_audioSource.Play();
    }

    IEnumerator MusicOnStart()
    {
        toggleGroup.SetAllTogglesOff();
        yield return new WaitForSeconds(0.2f);
        OnMusicItemToggleClicked(Random.Range(0, m_audioClips.Count));
    }
}
