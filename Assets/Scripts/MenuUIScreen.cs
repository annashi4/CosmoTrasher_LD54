using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIScreen : UIScreen
{
    [SerializeField] private Button _freeModeBtn;
    [SerializeField] private Button _scoreModeBtn;
    [SerializeField] private TextMeshProUGUI _bestScoreText;

    public override void Init(GameCanvas gameCanvas)
    {
        base.Init(gameCanvas);
        
        _freeModeBtn.onClick.AddListener(FreeModeSwitch);
        _scoreModeBtn.onClick.AddListener(ScoreModeSwitch);
        InitScore();
    }

    private void InitScore()
    {
        if (!PlayerPrefs.HasKey("Highscore"))
        {
            _bestScoreText.gameObject.SetActive(false);
            
            return;
        }
        
        float score = PlayerPrefs.GetFloat("Highscore");
        _bestScoreText.text = $"HIGHSCORE: <color=#FA7D7E>{Mathf.FloorToInt(score / 60):00}:{score % 60 :00}</color>";
    }

    private void ScoreModeSwitch()
    {
        SceneManager.LoadScene("Speed Run Mode");
    }

    private void FreeModeSwitch()
    {
        SceneManager.LoadScene("Free Mode");
    }

    public override UIScreenType GetUIType()
    {
        return UIScreenType.MENU;
    }
}