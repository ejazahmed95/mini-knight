using System;
using UnityEngine;

namespace MiniKnight.Player {
    public partial class CharacterController2D {
        public abstract class CharacterStateBase {
            protected CharacterController2D controller;

            public CharacterStateBase(CharacterController2D controller) {
                this.controller = controller;
            }
            
            public virtual bool BeginState(out CharacterStateBase alternateState) {
                alternateState = null;
                return true;
            }

            public virtual bool EndState() {
                return true;
            }

            public virtual CharacterStateBase Update(float deltaTime) {
                return null;
            }

            public virtual CharacterStateBase FixedUpdate() {
                return null;
            }

            public abstract CharacterStateBase HandleInput(InputCommandData inputCommandData);


            #region Sandbox Methods to be used by Subclass States
            protected void MoveCharacterHorizontal(float valueFloat) {
                
            }

            protected void SetPlayerMovement(float valueFloat) {
                controller.stateData.Movement = valueFloat;
                //controller._animator.SetFloat(Speed, Math.Abs(valueFloat));
                switch (valueFloat) {
                    case > 0 when controller.stateData.IsFacingLeft:
                        Flip();
                        break;
                    case < 0 when (controller.stateData.IsFacingLeft == false):
                        Flip();
                        break;
                }
            }
            
            private void Flip() {
                controller.stateData.IsFacingLeft = !controller.stateData.IsFacingLeft;

                // Multiply the player's x local scale by -1.
                Vector3 theScale = controller.transform.localScale;
                theScale.x *= -1;
                controller.transform.localScale = theScale;
            }

            #endregion
            
        }
    }
}