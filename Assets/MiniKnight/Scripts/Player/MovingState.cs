
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
                var velocity = controller._rigidbody.velocity;
                var data = controller.stateData;

                if (data.Movement == 0) {
                    //Log.Debug($"Data Velocity = {data.velocity}; RigidBody Velocity = {velocity}; Magnitude = {data.velocity.x.ToString()}");
                    if (velocity.magnitude < 0.01) {
                        controller._animator.SetFloat(Speed, 0);
                        controller._rigidbody.velocity = Vector2.zero;
                        return controller.AllStates.IdleState;
                    }
                }
                
                var targetVel = new Vector2(controller.stateData.Movement * controller.stateData.MoveSpeed, velocity.y);
                controller._rigidbody.velocity = Vector2.SmoothDamp(velocity, targetVel, ref controller.stateData.velocity, data.MovementSmoothing);
                return null;
            }

            public override CharacterStateBase HandleInput(InputCommandData inputCommandData) {
                switch(inputCommandData.Type) {
                    case InputCommandType.MOVE:
                        SetPlayerMovement(inputCommandData.ValueFloat);
                        return null;
                        break;
                    case InputCommandType.JUMP:
                        break;
                    case InputCommandType.ATTACK:
                        break;
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