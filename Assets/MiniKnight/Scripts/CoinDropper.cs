using System;
using MiniKnight.StatSystem;
using Othello.Scripts;
using RangerRPG.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniKnight.Scripts {
    public class CoinDropper : MonoBehaviour {

        public Vector2Int coinRange = new Vector2Int(0, 2);
        public int maxCoins = 2;
        public CoinObjectPool _objectPool;
        public Transform throwTransform;
        private int coinsDropped = 0;

        private void Start() {
            _objectPool = DI.Get<CoinObjectPool>();
        }
        
        public void DropCoin() {
            DropOneCoin();
        }

        private void OnDestroy() {
            while (coinsDropped++ < maxCoins) {
                DropOneCoin();
            }
        }
        
        private void DropOneCoin() {
            var coin = _objectPool.GetNewCoin(throwTransform);
            coin.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(0f, 3f));
            
            coinsDropped++;
        }
    }
}