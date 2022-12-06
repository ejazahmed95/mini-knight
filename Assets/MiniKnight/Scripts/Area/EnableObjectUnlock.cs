using System.Collections.Generic;
using RangerRPG.EventSystem;
using UnityEngine;

namespace MiniKnight.Area {
	public class EnableObjectUnlock : AreaUnlockBase {
		public List<GameObject> objectsToActivate = new();

		public override void Unlock() {
			base.Unlock();
			foreach (var obj in objectsToActivate) {
				obj.SetActive(true);
			}
		}
	}
}