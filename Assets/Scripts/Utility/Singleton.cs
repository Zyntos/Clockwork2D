// Kevin Hagen
// 10.08.2018

namespace Utility
{
	public abstract class Singleton<T> : MonoWrapper where T : Singleton<T>
	{
		public static T Instance => _instance as T;
		protected static Singleton<T> _instance;

		protected virtual void Awake()
		{
			if ((_instance != null) && (_instance != this))
			{
				Log("Multiple Instances of " + GetType() + " in " + gameObject.name + ".\nGoing to delete this one.");
				Destroy(_instance);
			}
			else
			{
				_instance = this;
			}
		}
	}
}
