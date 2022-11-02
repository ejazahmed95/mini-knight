
using System;
using RangerRPG.Core;
using UnityEngine;

namespace MiniKnight.Player {
    public partial class CharacterController2D {
        private static readonly int Speed = Animator.StringToHash("Speed");
        
        public class MovingState: CharacterStateBase {
            
            public MovingState(CharacterController2D characterController2D) : base(characterController2D) {
            
            }

            public override bool BeginState(out CharacterStateBase alternateState) {
                //controller._animator.SetBool();
                controller.stateData.ActiveStateText = "Moving";
                controller._animator.SetFloat(Speed, Math.Abs(controller.stateData.Movement));
                return base.BeginState(out alternateState);
            }

            public override CharacterStateBase FixedUpdate() {
                UpdateChecks();
                
                // if (controller.stateData.IsGrounded == false) {
                //     return controller.AllStates.FallingState;
                // } 
                
                var velocity = controller._rigidbody.velocity;
                ref var data = ref controller.stateData;

                if (data.Movement == 0) {
                    //Log.Debug($"Data Velocity = {data.velocity}; RigidBody Velocity = {velocity}; Magnitude = {data.velocity.x.ToString()}");
                    if (velocity.magnitude < 0.01) {
                        StopMovement();
                        return controller.AllStates.IdleState;
                    }
                }

                MoveCharacterHorizontal();
                return null;
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
                    case InputCommandType.MOVE:
                        SetPlayerMovement(inputCommandData.ValueFloat);
                        return null;
                    case InputCommandType.JUMP:
                        return controller.AllStates.JumpingState;
                    case InputCommandType.ATTACK:
                        return controller.AllStates.AttackingState;
                    case InputCommandType.SHOOT:
                        break;
                    case InputCommandType.DASH:
                        break;
                    default:
                        break;
                }
                return null;
            }
        }
    }
   
}