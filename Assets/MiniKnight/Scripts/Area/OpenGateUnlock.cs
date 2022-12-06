using System.Collections.Generic;
using UnityEngine;

namespace MiniKnight.Area {
	public class OpenGateUnlock : AreaUnlockBase {

		public List<GameObject> objectsToDestroy = new();
		
		public override void Unlock() {
			base.Unlock();
			foreach (var obj in objectsToDestroy) {
				Destroy(obj);
			}
		}
	}
}