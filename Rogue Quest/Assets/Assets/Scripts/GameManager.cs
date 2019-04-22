using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager S;

    public GameObject GameOverText;
    public GameObject PauseText;
    public bool IsPaused = false;
    public float LastPausedTime;

    public UnityEngine.Camera cam;

    public float lastTimeDamageEffect;

    void Awake()
    {
        // Singleton
        S = S ?? this;

        // Force 60 FPS
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        if(PauseText)
            PauseText.SetActive(false);
        
        if(GameOverText)
            GameOverText.SetActive(false);
    }

    
    public void GameOver()
    {
        // var audioSource = GetComponent<AudioSource>();
        // audioSource.clip = GameManager.S.sfxGameOver;
        // audioSource.loop = false;
        // audioSource.Play();

        GameOverText.SetActive(true);
        Invoke("Reload", 5f);
    }

    public void Reload()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void PauseUnpause()
    {
        if (IsPaused) UnPause();
        else Pause();
    }

    private void Pause()
    {
        if (Time.realtimeSinceStartup - LastPausedTime < 0.5f) return;
        LastPausedTime = Time.realtimeSinceStartup;
        PauseText.SetActive(true);
        IsPaused = true;
        Time.timeScale = 0f;
    }

    public void UnPause()
    {
        if (Time.realtimeSinceStartup - LastPausedTime < 0.5f) return;
        LastPausedTime = Time.realtimeSinceStartup;
        PauseText.SetActive(false);
        IsPaused = false;
        Time.timeScale = 1f;
    }

    public void CameraDamageEffect()
    {
        if (Time.unscaledTime - lastTimeDamageEffect < 0.5f) return;
        lastTimeDamageEffect = Time.unscaledTime;

        cam.backgroundColor = Color.red;
        cam.cullingMask = 0;

        Invoke("RestoreDamageEffect", 0.05f);

    }

    private void RestoreDamageEffect()
    {
        cam.cullingMask = -1;
        cam.backgroundColor = Color.black;
    }
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
