using UnityEngine;

namespace Bose.Wearable
{
	/// <summary>
	/// Helps to constrain the UI within the safe area determined by <see cref="Screen"/>
	/// </summary>
	internal class SafeAreaHelper : MonoBehaviour
	{
		[SerializeField]
		private Rect _simulatedSafeArea;

		private Canvas _canvas;
		private RectTransform _rectTransform;

		private void Awake()
		{
			_canvas = GetComponentInParent<Canvas>();
			_rectTransform = GetComponent<RectTransform>();
		}

		private void Start()
		{
			ApplySafeArea(Screen.safeArea);
		}

		private void ApplySafeArea(Rect safeArea)
		{
			if (_rectTransform == null)
			{
				return;
			}

			var anchorMin = safeArea.position;
			var anchorMax = safeArea.position + safeArea.size;
			anchorMin.x /= _canvas.pixelRect.width;
			anchorMin.y /= _canvas.pixelRect.height;
			anchorMax.x /= _canvas.pixelRect.width;
			anchorMax.y /= _canvas.pixelRect.height;

			_rectTransform.anchorMin = anchorMin;
			_rectTransform.anchorMax = anchorMax;
		}

		internal void SetSafeAreaAsSimulatedSafeArea()
		{
			if (Application.isEditor) {
				_simulatedSafeArea = Screen.safeArea;
			}
		}

		internal void ApplySimulatedSafeArea()
		{
			if (Application.isEditor) {
				ApplySafeArea(_simulatedSafeArea);
			}
		}
	}
}
