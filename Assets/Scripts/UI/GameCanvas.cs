using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvas : MonoBehaviourSingleton<GameCanvas>
{
	[SerializeField] private UIScreenType[] startScreens;
	[SerializeField] private CanvasScaler canvasScaler;
	[SerializeField] private RectTransform canvasRect;
	[SerializeField] private Canvas _canvas;

	private Dictionary<UIScreenType, UIScreen> uiScreens = new();

	public Canvas Canvas => _canvas;
	public CanvasScaler CanvasScaler => canvasScaler;
	public RectTransform CanvasRect => canvasRect;

	//private IGameDataContainer _loader;

	private void Start()
	{
		Init();
		
		foreach (var screen in startScreens)
		{
			Open(screen);
		}

		_canvas ??= GetComponent<Canvas>();
	}

	/*[Inject]
	public void Construct(IGameDataContainer loader)
	{
		_loader = loader;
		
		HapticOnButtons();
		
		Init();
	}*/


	public void Init()
	{
		InitScreens();
	}
	
	private void InitScreens()
	{
		var screens = GetComponentsInChildren<UIScreen>();

		foreach (var item in screens)
		{
			uiScreens.Add(item.GetUIType(), item);
			item.Init(this);
		}
	}

	public T GetScreen<T>(UIScreenType type) where T : UIScreen
	{
		return uiScreens[type] as T;
	}
	public void Open(UIScreenType type)
	{
		if (uiScreens.ContainsKey(type))
		{
			uiScreens[type].Open();
			
			return;
		}

		throw new Exception($"There's no screen for type {type.GetType().Name}");
	}
	public void Close(UIScreenType type)
	{
		if (uiScreens.ContainsKey(type))
		{
			uiScreens[type].Close();
			
			return;
		}

		throw new Exception($"There's no screen for type {type.GetType()}");
	}
}