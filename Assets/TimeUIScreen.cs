using TMPro;
using UnityEngine;

public class TimeUIScreen : UIScreen
{
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _scoreText;

    private void Update()
    {
        float time = GameManager.Instance.PlayTime;
        _timeText.text = $"<color=#FA7D7E>{Mathf.FloorToInt(time / 60):00}:{time % 60:00}</color>";
    }

    public override UIScreenType GetUIType()
    {
        return UIScreenType.TIME;
    }

    public void SetScores(int totalCount, int target)
    {
        _scoreText.text = $"{totalCount} / {target}";
    }
}