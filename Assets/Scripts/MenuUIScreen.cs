using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIScreen : UIScreen
{
    [SerializeField] private Button _freeModeBtn;
    [SerializeField] private Button _scoreModeBtn;

    public override void Init(GameCanvas gameCanvas)
    {
        base.Init(gameCanvas);
        
        _freeModeBtn.onClick.AddListener(FreeModeSwitch);
        _scoreModeBtn.onClick.AddListener(ScoreModeSwitch);
    }

    private void ScoreModeSwitch()
    {
        SceneManager.LoadScene("Score Mode");
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