using TMPro;
using UnityEngine;

public class UIGameManager : MonoBehaviour
{
    [Header("UI Panel Elements"), Space]
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject tapToPlay;
    [Header("Others")]

    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private GameObject _particles;

    public static UIGameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        tapToPlay.SetActive(true);
        gameUI.SetActive(false);
        winPanel.SetActive(false);
        losePanel.SetActive(false);
    }

    public void SetLevelText(int level)
    {
        _levelText.text = "Level " + level;
    }

    public void ShowWinPanel(bool state)
    {
        this.winPanel.SetActive(state);
    }

    public void ShowLosePanel(bool state)
    {
        this.losePanel.SetActive(state);
    }

    public void ShowGameUI(bool state)
    {
        this.gameUI.SetActive(state);
    }

    public void PlayEndParticles()
    {
        this._particles.SetActive(true);
    }

    public void StartTheGame()
    {
        Debug.Log("StartTheGame");
        GameSceneManager.Instance.SetTimeScale(1);
    }

    public void ResetTheGame()
    {
        GameSceneManager.Instance.LoadGamePlayScene();
    }

    public void RestartLevel()
    {
        GameSceneManager.Instance.LoadGamePlayScene();
    }

    public void NextLevel()
    {
        StaticData.selectGameLevel++;
        GameSceneManager.Instance.LoadGamePlayScene();
    }
    public void GoToMenu() 
    {
        GameSceneManager.Instance.LoadMainMenuScene();
    }
    public void PauseGame()
    {
        int x = Time.timeScale == 0 ? 1 : 0;
        GameSceneManager.Instance.SetTimeScale(x);
    }
}
