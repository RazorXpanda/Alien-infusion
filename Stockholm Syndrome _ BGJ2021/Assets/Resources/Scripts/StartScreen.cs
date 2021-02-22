using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public Animator start_animator;
    public Animator music_animator;
    public Animator tutorials_animator;
    public Animator credits_animator;
    public GameObject musicPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public PlayerController playerController;
    public AudioClip buttonAudio;
    private AudioSource audioSource;

    private void Start()
    {
        pausePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }
    public void OnStartButtonClick()
    {
        start_animator.SetBool("isClick", true);
        PlayerController.isStart = true;
        playerController.enemySpawner.SetActive(true);
        audioSource.PlayOneShot(buttonAudio);
    }

    public void OnTutorialButtonClick()
    {
        tutorials_animator.SetBool("isOpen", true);
        audioSource.PlayOneShot(buttonAudio);
    }

    public void OnTutorialsBackClick()
    {
        tutorials_animator.SetBool("isOpen", false);
        audioSource.PlayOneShot(buttonAudio);
        //musicPanel.SetActive(false);
    }

    public void OnMusicButtonClick()
    {
        music_animator.SetBool("isOpen", true);
        audioSource.PlayOneShot(buttonAudio);
        //musicPanel.SetActive(true);
    }

    public void OnMusicBackClick()
    {
        music_animator.SetBool("isOpen",false);
        audioSource.PlayOneShot(buttonAudio);
        //musicPanel.SetActive(false);
    }

    public void OnMainMenuButtonClick(int buildIndex)
    {
        Time.timeScale = 1;
        audioSource.PlayOneShot(buttonAudio);
        SceneManager.LoadScene(buildIndex);
    }

    public void OnResumeButtonClick()
    {
        Time.timeScale = 1;
        audioSource.PlayOneShot(buttonAudio);
        pausePanel.SetActive(false);
    }

    public void OnCreditsButtonClick()
    {
        credits_animator.SetBool("isOpen", true);
        audioSource.PlayOneShot(buttonAudio);
        //musicPanel.SetActive(true);
    }

    public void OnCreditsBackClick()
    {
        credits_animator.SetBool("isOpen",false);
        audioSource.PlayOneShot(buttonAudio);
        //musicPanel.SetActive(false);
    }

    public GameObject playerPrefab;
    public GameObject enemySpawner;
    public EnemySpawner spawnerScript;
    private Vector3 initialPosition = new Vector3(0,0,0);
    public GameObject border;
    public Healthbar healthbar;
    public KillCounter killCounter;

    public void OnRestartButtonClick()
    {        
        playerPrefab.SetActive(true);
        playerPrefab.transform.position = initialPosition;
        playerController.health = playerController.maxHealth;
        healthbar.SetHealthUI(playerController.maxHealth);
        PlayerData.enemyKilled = 0;
        killCounter.OnTextUpdate();
        Time.timeScale = 1;
        border.SetActive(false);
        PlayerController.gameOver = false;
        enemySpawner.SetActive(true);
        audioSource.PlayOneShot(buttonAudio);
        StartCoroutine(spawnerScript.Spawner(1/0.3f));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
        }
    }


}