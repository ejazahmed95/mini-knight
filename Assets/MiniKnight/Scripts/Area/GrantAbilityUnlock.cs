using RangerRPG.Core;
using RangerRPG.EventSystem;
using UnityEngine;

namespace MiniKnight.Area {
	public class GrantAbilityUnlock : AreaUnlockBase {
		public GameEvent abilityEvent;

		public override void Unlock() {
			base.Unlock();
			Log.Info("Granting Ability!");
			abilityEvent?.Raise();
		}
	}
}