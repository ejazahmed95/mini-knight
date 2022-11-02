using UnityEngine;
using System.Collections;
using MiniKnight.StatSystem;

namespace MiniKnight.Enemies {
    public class BasicEnemy : MonoBehaviour {

        private bool isPlat;
        private bool isObstacle;
        private Transform fallCheck;
        private Transform wallCheck;
        public LayerMask turnLayerMask;
        private Rigidbody2D rb;

        private bool facingRight = true;

        public float speed = 5f;

        public bool isInvincible = false;
        private bool isHitted = false;
        private bool isDead = false;

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

            if (!isHitted && Mathf.Abs(rb.velocity.y) < 0.5f) {
                if (isPlat && !isObstacle && !isHitted) {
                    if (facingRight) {
                        rb.velocity = new Vector2(-speed, rb.velocity.y);
                    }
                    else {
                        rb.velocity = new Vector2(speed, rb.velocity.y);
                    }
                }
                else {
                    Flip();
                }
            }
        }

        public void OnDead() {
            _animator.SetBool(IsDead, true);
            isDead = true;
            StartCoroutine(DestroyEnemy());
        }

        public void OnDamage() {
            if (isDead) return;
            _animator.SetBool(Hit, true);
            rb.velocity = Vector2.zero;
            float direction = facingRight ? 1 : -1;
            rb.AddForce(new Vector2(direction * 500f, 100f));
            StartCoroutine(HitTime());
        }

        void Flip() {
            facingRight = !facingRight;

            Vector3 newScale = transform.localScale;
            newScale.x *= -1;
            transform.localScale = newScale;
        }

        void OnCollisionStay2D(Collision2D collision) {
            if (collision.gameObject.CompareTag("Player") && isDead == false) {
                FaceTowards(collision.gameObject.transform);
                collision.gameObject.GetComponent<HealthComponent>().ApplyDamage(2f);
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
            CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
            capsule.size = new Vector2(1f, 0.25f);
            capsule.offset = new Vector2(0f, -0.8f);
            capsule.direction = CapsuleDirection2D.Horizontal;
            yield return new WaitForSeconds(0.25f);
            rb.velocity = new Vector2(0, rb.velocity.y);
            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }
    }
}