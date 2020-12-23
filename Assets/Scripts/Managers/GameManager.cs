using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public UnityEvent deathEvents, winEvents;
    public AudioSource enemyHit;
    public EnemyWave[] possibleWaves;
    private int currentDifficulty;
    public float timePointBonus = 0.0f;

    public int CurrentDifficulty
    {
        get { return currentDifficulty; }
        set
        {
            currentDifficulty = value;
            foreach (EnemyWave wave in possibleWaves)
            {
                if(wave.difficultyLevel == currentDifficulty)
                {
                    GetComponent<EnemySpawner>().waves.Add(wave);
                }
            }
        }
    }

    public int[] difficultyUpWaves;

    private int wavesDefeated;
    public int WavesDefeated
    {
        get { return wavesDefeated; }
        set 
        {
            wavesDefeated = value;
            foreach (int i in difficultyUpWaves)
            {
                if(wavesDefeated == i) { CurrentDifficulty++; }
            }
        }
    }

    private int currentTimeSpeed;
    private int CurrentTimeSpeed
    {
        get
        {
            return currentTimeSpeed;
        }
        set
        {
            currentTimeSpeed = Mathf.Clamp(value, 0, 6);
            timeSpeedMeter.IndicatorSetting = currentTimeSpeed;
            AudioVolumeManager.Instance?.SetPitch(GetCurrentTimeSpeed());
        }
    }

    public ScoreUI scoreUI, deadScoreUI;

    private int score;
    public int Score
    {
        get { return score; }
        set { score = value; scoreUI.ScoreValue = score; deadScoreUI.ScoreValue = score; }
    }

    float currentTimePoints = 0.5f;
    public float CurrentTimePoints
    {
        get { return currentTimePoints; }
        set 
        {
            if (value > 1.0f) { CurrentTimeSpeed = Mathf.Min(CurrentTimeSpeed, 3); }
            currentTimePoints = Mathf.Clamp(value, 0.0f, 1.0f); 
        }
    }


    public float timePointsPerSecond = 0.1f;
    bool canChangeTime;
    TimeSpeedMeter timeSpeedMeter; 
    public HealthBar timeBar;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentDifficulty = 0;
        Score = 0;
        timeSpeedMeter = FindObjectOfType<TimeSpeedMeter>();
        CurrentTimeSpeed = 3;
        canChangeTime = true;
    }

    private void Update()
    {
        if (canChangeTime)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                CurrentTimeSpeed--;

            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                CurrentTimeSpeed++;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                CurrentTimeSpeed = 3;
            }
        }

        if (CurrentTimePoints <= 0.0f && canChangeTime)
        {
            canChangeTime = false;
            CurrentTimeSpeed = 6;
        }

        if (!canChangeTime && CurrentTimePoints >= 0.5f)
        {
            CurrentTimeSpeed = 3;
            canChangeTime = true;
        }

        if(Input.GetKeyDown(KeyCode.End))
        {
            CameraShake(1.0f);
        }


        CurrentTimePoints += (((GetCurrentTimeSpeed() - 1.0f) * timePointsPerSecond) + timePointBonus) * Time.deltaTime;
        timeBar.HPValue = CurrentTimePoints;

    }
    public float GetCurrentTimeSpeed()
    {
            return currentTimeSpeed / 3.0f;     
    }

    #region ManaBasic Functionality
    public void Die()
    {
        deathEvents.Invoke();
    }

    public void Win()
    {
        winEvents.Invoke();
    }
    #endregion

    public void CameraShake(float time, float intensity = 0.15f)
    {
        StartCoroutine(CameraShakeC(time, intensity));
    }

    IEnumerator CameraShakeC(float time, float intensity)
    {
        float t = time;
        Vector3 orig = Camera.main.transform.position;
        while (t > 0.0f)
        {
            t -= Time.deltaTime;
            Vector3 shakeVector = new Vector3(Random.Range(-intensity, intensity), 0, Random.Range(-intensity, intensity));
            Camera.main.transform.position += shakeVector;
            yield return null;
            Camera.main.transform.position -= shakeVector;
        }
        Camera.main.transform.position = orig;
    }

    public void PlayEnemyHitSound()
    {
        enemyHit.Play();
    }

    public void SaveScore()
    {
        HighscoreManager.Instance.AddScore(score);
        HighscoreManager.Instance.SaveHighscores();
        GetComponent<HighScoreText>()?.SetHighscores();
    }
}
