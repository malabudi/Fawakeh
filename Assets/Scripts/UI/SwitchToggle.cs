using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SwitchToggle : MonoBehaviour
{
    [SerializeField] RectTransform uiHandleRectTransform;
	[SerializeField] Color backgroundActiveColor;
	[SerializeField] Color handleActiveColor;

	Image backgroundImage, handleImage;

	Color backgroundDefaultColor;
	Color handleDefaultColor;

	Toggle toggle;

    Vector2 handlePosition;

	private void Awake()
	{
		toggle = GetComponent<Toggle>();
		handlePosition = uiHandleRectTransform.anchoredPosition;

		backgroundImage = uiHandleRectTransform.parent.GetComponent<Image>();
		handleImage = uiHandleRectTransform.GetComponent<Image>();

		backgroundDefaultColor = backgroundImage.color;
		handleDefaultColor = handleImage.color;

		toggle.onValueChanged.AddListener(OnSwitch);

		if (toggle.isOn)
		{
			OnSwitch(true);
		}
	}

	private void OnDestroy()
	{
		toggle.onValueChanged.RemoveListener(OnSwitch);
	}

	private void OnSwitch (bool on)
	{
		// Change colors and position of the toggle here
		uiHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, 0.4f).SetEase(Ease.InOutBack);
		backgroundImage.DOColor(on ? backgroundActiveColor : backgroundDefaultColor, 0.6f);
		handleImage.DOColor(on ? handleActiveColor : handleDefaultColor, 0.4f);
	}
}
