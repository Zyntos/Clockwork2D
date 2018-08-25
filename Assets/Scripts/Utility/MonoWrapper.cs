// Kevin Hagen
// 10.08.2018

using System;
using UnityEngine;

namespace Utility
{
	public abstract class MonoWrapper : MonoBehaviour
	{
		private Transform _cachedTransform;

		public Transform CachedTransform {
			get
			{
				if (!_cachedTransform) _cachedTransform = transform;

				return _cachedTransform;
			}
		}

		public static void Log(string message, LogType logType = LogType.Log)
		{
			DateTime date = DateTime.Now;
			message = date.ToString("MM/dd/yyyy hh:mm:ss.fff tt").InColor(Color.gray) + " " + message;
			Debug.unityLogger.Log(logType, message);
		}
	}
}
