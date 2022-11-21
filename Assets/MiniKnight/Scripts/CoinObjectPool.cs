using System.Collections.Generic;
using MiniKnight.StatSystem;
using UnityEngine;

namespace Othello.Scripts {
    public class CoinObjectPool : MonoBehaviour {
        [SerializeField] private PlayerPickup coinPrefab;
        private List<PlayerPickup> _freeCoins;

        private void Awake() {
            _freeCoins = new List<PlayerPickup>();
        }
        
        public PlayerPickup GetNewCoin() {
            if (_freeCoins.Count > 0) {
                PlayerPickup coin = _freeCoins[0];
                _freeCoins.RemoveAt(0);
                coin.Init();
                coin.gameObject.SetActive(true);
                return coin;
            };
            PlayerPickup freeCoin = Instantiate(coinPrefab, gameObject.transform);
            freeCoin.Init();
            return freeCoin;
        }

        public void DisableCoin(PlayerPickup coin) {
            coin.gameObject.SetActive(false);
            _freeCoins.Add(coin);
        }
    }
}