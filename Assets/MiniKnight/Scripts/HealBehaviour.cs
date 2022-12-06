using MiniKnight.StatSystem;
using UnityEngine;

namespace MiniKnight {
    public class HealBehaviour : MonoBehaviour {
        public float healAmount = 10;
        public float maxHealAmount = 20;

        public float totalHealed = 0;
        public void HealOnce(HealthComponent comp) {
            if (comp == null) return;
            comp.Heal(healAmount);
            totalHealed += healAmount;
        }

        public void HealRemaining(HealthComponent comp) {
            if (comp == null) return;
            var amount = maxHealAmount - healAmount;
            if (amount > 0) {
                comp.Heal(amount);
                totalHealed += healAmount;
            }
        }
    }
}