using UnityEngine;
using UnityEngine.Events;

namespace MiniKnight.Area {
	public class AreaUnlockBase : MonoBehaviour {

		public UnityEvent onUnlock = new();
		private bool _locked = true;
		
		public virtual void Unlock() {
			if (_locked == false) return;
			_locked = true;
			onUnlock.Invoke();
		}
	}
}