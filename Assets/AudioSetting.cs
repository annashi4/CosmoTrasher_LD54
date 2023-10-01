using UnityEngine;
using UnityEngine.UI;

public class AudioSetting : MonoBehaviour
{
	[SerializeField] private Button _muteButton;
	[SerializeField] private Sprite _mutedSprite;
	[SerializeField] private Sprite _unmutedSprite;
	
	private void Start()
	{
		_muteButton.onClick.AddListener(ToggleVolume);
		SetVolume();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			ToggleVolume();
		}
	}

	private void SetVolume()
	{
		var isMuted = PlayerPrefs.GetInt("Volume", 0) < 0;

		AudioManager.Instance.SetVolume(isMuted ? -80 : 0);
		UpdateButton();
	}

	public void ToggleVolume()
	{
		var isMuted = PlayerPrefs.GetInt("Volume", 0) < 0;

		int value = isMuted ? 0 : -80;
		PlayerPrefs.SetInt("Volume", value);
		AudioManager.Instance.SetVolume(value);
		
		UpdateButton();
	}

	private void UpdateButton()
	{
		var isMuted = PlayerPrefs.GetInt("Volume", 0) < 0;

		_muteButton.image.sprite = isMuted ? _mutedSprite : _unmutedSprite;
	}
}