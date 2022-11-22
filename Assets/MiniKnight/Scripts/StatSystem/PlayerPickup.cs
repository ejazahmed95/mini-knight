using RangerRPG.Core;
using UnityEngine;
using UnityEngine.Events;

namespace MiniKnight.StatSystem {
    public class PlayerPickup: MonoBehaviour {
        public UnityEvent onItemPicked;
        public AudioClip clip;

        private bool _picked = false;

        public void PickedItem() {
            if (_picked) return;
            AudioManager.Instance.Play(clip);
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