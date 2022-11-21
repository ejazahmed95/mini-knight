using UnityEngine;
using UnityEngine.Events;

namespace MiniKnight.StatSystem {
    public class PlayerPickup: MonoBehaviour {
        public UnityEvent onItemPicked;

        private bool _picked = false;

        public void PickedItem() {
            if (_picked) return;
            _picked = true;
            onItemPicked.Invoke();
            GameManager.Instance.AddCoins(1);
        }
        
        public void Init() {
            _picked = false;
            onItemPicked = new UnityEvent();
        }
    }
}