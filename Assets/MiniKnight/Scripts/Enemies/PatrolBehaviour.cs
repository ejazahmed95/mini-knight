using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniKnight.Enemies {
    [RequireComponent(typeof(EnemyBase))]
    public class PatrolBehaviour : MonoBehaviour {
        public float halfDistance = 3;
        public Vector3[] patrolPoints;
        
        private EnemyBase enemyRef;

        private Vector3 startPosition;
        
        private void Awake() {
            enemyRef = GetComponent<EnemyBase>();
            startPosition = gameObject.transform.position;
            
        }

        private void FixedUpdate() {
            if (enemyRef.Dead()) return;
            //isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
            bool isObstacle = Vector3.Distance(enemyRef.wallCheck.position, startPosition) > halfDistance;

            if (!enemyRef.isHitted && Mathf.Abs(enemyRef.rb.velocity.y) < 0.5f) {
                if (!isObstacle && !enemyRef.isHitted) {
                    if (enemyRef.facingRight) {
                        enemyRef.rb.velocity = new Vector2(-enemyRef.speed, enemyRef.rb.velocity.y);
                    }
                    else {
                        enemyRef.rb.velocity = new Vector2(enemyRef.speed, enemyRef.rb.velocity.y);
                    }
                }
                else {
                    enemyRef.Flip();
                }
            }
        }
    }
}