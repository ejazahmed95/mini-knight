using UnityEngine;
using System.Collections;
using MiniKnight.StatSystem;

namespace MiniKnight.Enemies {
    public class EnemyBase: MonoBehaviour {

        private bool isPlat;
        private bool isObstacle;
        public Transform fallCheck;
        public Transform wallCheck;
        public LayerMask turnLayerMask;
        public Rigidbody2D rb;

        public bool facingRight = true;

        public float speed = 5f;

        public bool isInvincible = false;
        public bool isHitted = false;
        protected bool isDead = false;

        public float damage = 10f;
        
        private Animator _animator;
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        private static readonly int Hit = Animator.StringToHash("Hit");

        void Awake() {
            fallCheck = transform.Find("FallCheck");
            wallCheck = transform.Find("WallCheck");
            rb = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void FixedUpdate() {
            if (isDead) return;
            
            isPlat = Physics2D.OverlapCircle(fallCheck.position, .2f, 1 << LayerMask.NameToLayer("Default"));
            isObstacle = Physics2D.OverlapCircle(wallCheck.position, .2f, turnLayerMask);

            // if (!isHitted && Mathf.Abs(rb.velocity.y) < 0.5f) {
            //     if (isPlat && !isObstacle && !isHitted) {
            //         if (facingRight) {
            //             rb.velocity = new Vector2(-speed, rb.velocity.y);
            //         }
            //         else {
            //             rb.velocity = new Vector2(speed, rb.velocity.y);
            //         }
            //     }
            //     else {
            //         Flip();
            //     }
            // }
        }

        public void OnDead() {
            if(_animator) _animator.SetBool(IsDead, true);
            isDead = true;
            var sprite = GetComponent<SpriteRenderer>();
            var color = sprite.color;
            color.a = 0.3f;
            sprite.color = color;
            StartCoroutine(DestroyEnemy());
        }

        public void OnDamage() {
            if (isDead) return;
            if(_animator) _animator.SetBool(Hit, true);
            rb.velocity = Vector2.zero;
            float direction = facingRight ? 1 : -1;
            rb.AddForce(new Vector2(direction * 500f, 100f));
            StartCoroutine(HitTime());
        }

        public void Flip() {
            facingRight = !facingRight;

            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        void OnCollisionStay2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player") && isDead == false) {
                FaceTowards(collision.gameObject.transform);
                collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(damage);
            }
        }
        private void FaceTowards(Transform otherTransform) {
            var isObjectToRight = (otherTransform.position.x - transform.position.x) > 0;

            if (facingRight != isObjectToRight) {
                Vector3 newScale = transform.localScale;
                newScale.x *= -1;
                transform.localScale = newScale;
            }
        }

        IEnumerator HitTime() {
            isHitted = true;
            isInvincible = true;
            yield return new WaitForSeconds(0.1f);
            isHitted = false;
            isInvincible = false;
        }

        IEnumerator DestroyEnemy() {
            //CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
            
            // capsule.size = new Vector2(1f, 0.25f);
            // capsule.offset = new Vector2(0f, -0.8f);
            // capsule.direction = CapsuleDirection2D.Horizontal;
            yield return new WaitForSeconds(0.25f);
            rb.velocity = new Vector2(0, rb.velocity.y);
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
        
        public bool Dead() {
            return isDead;
        }
    }
}