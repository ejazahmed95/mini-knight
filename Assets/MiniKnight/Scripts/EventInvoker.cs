using RangerRPG.EventSystem;
using UnityEngine;

namespace MiniKnight {
    public class EventInvoker : MonoBehaviour {
        public GameEvent eventToInvoke;

        public void InvokeGameEvent() {
            eventToInvoke?.Raise();
        }
    }
}