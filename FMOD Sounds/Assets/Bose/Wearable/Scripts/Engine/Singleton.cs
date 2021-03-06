using UnityEngine;

namespace Bose.Wearable
{
	public class Singleton<T> : MonoBehaviour where T : Component
	{
		private static bool _applicationIsQuitting = false;

		private static T _instance = null;

		public static T Instance
		{
			get
			{
				if (!Exists && !_applicationIsQuitting)
				{
					_instance = FindObjectOfType<T>();

					if (!Exists)
					{
						_instance = new GameObject("_" + typeof(T).Name).AddComponent<T>();
					}

					DontDestroyOnLoad(_instance);
				}

				return _instance;
			}
		}

		/// <summary>
		/// Returns true if this singleton exists, otherwise false.
		/// </summary>
		public static bool Exists
		{
			get { return _instance != null; }
		}

		/// <summary>
		/// Returns true if this instance is the singleton, otherwise false.
		/// </summary>
		/// <returns></returns>
		public bool IsSingletonInstance
		{
			get { return Exists && Instance == this; }
		}

		protected virtual void Awake()
		{
			if (!Exists)
			{
				_instance = this as T;
				DontDestroyOnLoad(gameObject);
			}
			else if (_instance != this)
			{
				Destroy(gameObject);
			}
		}

		protected virtual void OnDestroy()
		{
			// No-Op
		}

		protected virtual void OnApplicationQuit()
		{
			_instance = null;
			Destroy(gameObject);
			_applicationIsQuitting = true;
		}
	}
}
