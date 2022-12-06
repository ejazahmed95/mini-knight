using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace MiniKnight.StatSystem {
    public class HealthComponent : MonoBehaviour {
        public float baseHealth = 20f;
        public float currentHealth;
        public bool destroyOnZeroHealth;

        public bool specialDamageOnly = false;
        public bool invincible = false;
        
        public UnityEvent OnZeroHealth = new();
        public UnityEvent OnHit = new();

        public Image healthBarImage;

        private void Start() {
            if(currentHealth <= 0) currentHealth = baseHealth;
            if (healthBarImage != null) {
                healthBarImage.fillAmount = GetHealthPercentage();
            }
        }
        
        public void ApplyDamage(float damage, DamageType type = DamageType.NORMAL) {
            if (invincible || currentHealth <= 0) return;
            if (specialDamageOnly && type != DamageType.SPECIAL) {
                return;
            }
            
            currentHealth -= damage;
            OnHit.Invoke();
            if (currentHealth <= 0) {
                currentHealth = 0;
                OnZeroHealth.Invoke();
                if (destroyOnZeroHealth) {
                    Destroy(gameObject, 1f);
                }
            }
            
            if (healthBarImage != null) {
                healthBarImage.fillAmount = GetHealthPercentage();
            }
        }

        public float GetHealthPercentage() {
            return currentHealth / baseHealth;
        }

        public enum DamageType {
            NORMAL,
            SPECIAL
        }
        
        public void Heal(float healAmount) {
            if (currentHealth <= 0) return;
            currentHealth += healAmount;
            if (currentHealth > baseHealth) currentHealth = baseHealth;
            
            if (healthBarImage != null) {
                healthBarImage.fillAmount = GetHealthPercentage();
            }
        }
    }
}