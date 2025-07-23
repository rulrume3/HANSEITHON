using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject gameOverPanel;

    [Header("UI Component")]
    public Image fadeCover;
    public Text levelTitleText;
    public float fadeDuration;

    [Header("BGM")]
    public AudioSource bgmSource;
    public AudioClip studentBGM;
    public AudioClip workerBGM;
    public AudioClip elderBGM;

    private string nextSceneName;
    private string nextLevelTitle;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        fadeCover.color = new Color(0, 0, 0, 0);
        levelTitleText.gameObject.SetActive(false);
    }
    private void Start()
    {
        PlayerBGMForScene(SceneManager.GetActiveScene().name);
        if(gameOverPanel != null)
            gameOverPanel.SetActive(false);
    }

    public void TransitionToLevel(string sceneName, string levelTitle)
    {
        nextSceneName = sceneName;
        nextLevelTitle = levelTitle;
        StartCoroutine(TransitionRoutine());
    }

    private IEnumerator TransitionRoutine()
    {
        yield return StartCoroutine(Fade(0, 1));

        yield return SceneManager.LoadSceneAsync(nextSceneName);
    }


    private IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float timer = 0;
         
        Color c = fadeCover.color;
        while (timer < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, endAlpha, timer
                / fadeDuration);
            fadeCover.color = new Color(c.r, c.g, c.b, alpha);
            timer += Time.unscaledDeltaTime;
            yield return null;

        }
        fadeCover.color = new Color(c.r, c.g, c.b, endAlpha);
    }
    void PlayerBGMForScene(string sceneName)
    {
        if (bgmSource == null) return;

        switch (sceneName)
        {
            case "StudentScene":
                bgmSource.clip = studentBGM; 
                break;
            case "WorkerScene":
                bgmSource.clip = workerBGM;
                break;
            case "ElderScene":
                bgmSource.clip = elderBGM;
                break;
            default:
                bgmSource.clip = null;
                break;


        }
        if(bgmSource.clip != null)
        {
            bgmSource.Play();
        }
        else
        {
            bgmSource.Stop();   
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RetOnRetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Student");
    }
    public void OnExitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("TitleScene");
    }
}
