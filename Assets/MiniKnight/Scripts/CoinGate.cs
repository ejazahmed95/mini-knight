using MiniKnight.StatSystem;
using TMPro;
using UnityEngine;

namespace MiniKnight {
    public class CoinGate : MonoBehaviour {
        public TilemapBurner gateBurner;
        public int requiredCoins;
        public TMP_Text coinText;
        public HealthComponent healthComponent;
        
        private GameManager gm;
        private void Start() {
            gm = GameManager.Instance;
            coinText.text = $"{requiredCoins}";
        }

        public void OnHit() {
            if (gm.playerCoins < requiredCoins) {
                healthComponent.Heal(10);
            }
        }

        public void DestroyGate() {
            gm.AddCoins(-requiredCoins);
            gateBurner.BurnTiles();
        }
    }
}