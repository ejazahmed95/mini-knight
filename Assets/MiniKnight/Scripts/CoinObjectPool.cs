using System.Collections.Generic;
using MiniKnight.StatSystem;
using RangerRPG.Core;
using UnityEngine;

namespace Othello.Scripts {
    public class CoinObjectPool : MonoBehaviour {
        [SerializeField] private PlayerPickup coinPrefab;
        private List<PlayerPickup> _freeCoins;

        private void Awake() {
            _freeCoins = new List<PlayerPickup>();
            DI.Register(this);
        }
        
        public PlayerPickup GetNewCoin(Transform newTransform) {
            if (_freeCoins.Count > 0) {
                PlayerPickup coin = _freeCoins[0];
                _freeCoins.RemoveAt(0);
                coin.Init();
                coin.gameObject.SetActive(true);
                return coin;
            };
            PlayerPickup freeCoin = Instantiate(coinPrefab, gameObject.transform);
            freeCoin.transform.position = newTransform.position;
            freeCoin.Init();
            return freeCoin;
        }

        public void DisableCoin(PlayerPickup coin) {
            coin.gameObject.SetActive(false);
            _freeCoins.Add(coin);
        }
    }
}