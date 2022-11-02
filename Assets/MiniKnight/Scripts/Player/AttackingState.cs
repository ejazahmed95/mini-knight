using MiniKnight.StatSystem;
using UnityEngine;

namespace MiniKnight.Player {
    public partial class CharacterController2D {
        private static readonly int IsAttacking = Animator.StringToHash("IsAttacking");
        public class AttackingState: CharacterStateBase {
            private float timeElapsed = 0f;
            
            public AttackingState(CharacterController2D controller) : base(controller) {
                    
            }
            
            public override bool BeginState(out CharacterStateBase alternateState) {
                controller.stateData.ActiveStateText = "Attack";
                timeElapsed = 0f;
                controller._animator.SetBool(IsAttacking, true);
                return base.BeginState(out alternateState);
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
                    case InputCommandType.JUMP:
                        if (controller.stateData.CanDoubleJump && controller.stateData.IsDoubleJumping == false) {
                            controller.stateData.IsDoubleJumping = true;
                            return controller.AllStates.JumpingState;
                        }
                        return null;
                    case InputCommandType.ATTACK:
                        break;
                    case InputCommandType.SHOOT:
                        break;
                    case InputCommandType.DASH:
                        break;
                    default:
                        return null;
                }
                return null;
            }

            public override CharacterStateBase Update(float deltaTime) {
                timeElapsed += deltaTime;
                if (timeElapsed > 0.25f) {
                    return controller.AllStates.IdleState;
                }
                return base.Update(deltaTime);
            }

            public override bool EndState() {
                controller._animator.SetBool(IsAttacking, false);
                return base.EndState();
            }
            
            public void DealDamage() {
                ref var data = ref controller.stateData;
                Collider2D[] collidersEnemies = Physics2D.OverlapCircleAll(data.AttackCheck.position, 0.9f);
                foreach (var collider in collidersEnemies) {
                    if (collider.gameObject == controller.gameObject) {
                        continue;
                    }
                    if (collider.gameObject.TryGetComponent(out HealthComponent health)) {
                        // Use TeamIds on Health Component
                        health.ApplyDamage(data.Damage);
                    }
                }
            }
       }
    }
}