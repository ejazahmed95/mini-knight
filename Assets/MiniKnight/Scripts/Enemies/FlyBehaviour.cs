using System;
using RangerRPG.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace MiniKnight.Enemies {
    [RequireComponent(typeof(EnemyBase))]
    public class FlyBehaviour : MonoBehaviour {
        public float halfDistance = 3;
        public Vector3[] patrolPoints;
        
        private EnemyBase enemyRef;

        private Vector3 startPosition;
        public Vector2 dir = new Vector2(0.5f, 0.5f);
        private float timeLapsed = 1f;

        private void Awake() {
            enemyRef = GetComponent<EnemyBase>();
            startPosition = gameObject.transform.position;
            Random.InitState(System.DateTime.Now.Millisecond);
        }
        
        private void Start() {
            //dir = new Vector2(Random.Range(-1, 1f), Random.Range(-1,1f)).normalized;
            enemyRef.rb.velocity = dir * enemyRef.speed;
        }

        private void OnCollisionEnter2D(Collision2D col) {
           // if (timeLapsed > 0) return;
            var contactPoint = col.contacts[0].point;
            Vector2 location = transform.position;
            var inNormal = (location - contactPoint).normalized;
            dir = Vector2.Reflect(dir, inNormal);
            enemyRef.rb.velocity = dir * enemyRef.speed;
            //transform.position += new Vector3(dir.x, dir.y, 0)*0.1f;
            Log.Info($"Collision happened! {dir}; Normal = {inNormal}");
            timeLapsed = 1f;
        }
        
        private void FixedUpdate() {
            if (enemyRef.Dead()) {
                enemyRef.rb.gravityScale = 1;
                return;
            }
            if (timeLapsed > 0) timeLapsed -= Time.deltaTime;
            enemyRef.rb.velocity = dir * enemyRef.speed;

            //isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
            // bool isObstacle = Vector3.Distance(enemyRef.wallCheck.position, startPosition) > halfDistance;
            //
            // if (!enemyRef.isHitted && Mathf.Abs(enemyRef.rb.velocity.y) < 0.5f) {
            //     if (!isObstacle && !enemyRef.isHitted) {
            //         if (enemyRef.facingRight) {
            //             enemyRef.rb.velocity = new Vector2(-enemyRef.speed, enemyRef.rb.velocity.y);
            //         }
            //         else {
            //             enemyRef.rb.velocity = new Vector2(enemyRef.speed, enemyRef.rb.velocity.y);
            //         }
            //     }
            //     else {
            //         enemyRef.Flip();
            //     }
            // }
        }
    }
}