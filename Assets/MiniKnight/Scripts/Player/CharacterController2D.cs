using RangerRPG.Core;
using UnityEngine;

namespace MiniKnight.Player {
    public class CharacterController2D : MonoBehaviour {
        private CharacterStateBase currentState;
        private CharacterInputHandler input;
        public CharacterStates AllStates;
        
        private void Awake() {
            AllStates = new CharacterStates {
                IdleState = new IdleState(this),
                MovingState = new MovingState(this),
                JumpingState = new JumpingState(this),
                FallingState = new FallingState(this)
            };
            currentState = AllStates.IdleState;
        }

        private void Start() {
            input = DI.Get<CharacterInputHandler>();
        }

        void HandleInput(InputCommandData data) {
            currentState.HandleInput(data);
        }

        private void Update() {
            var state = currentState.Update(Time.deltaTime);
            if (state != null) {
                GoToState(state);
            }
        }

        private void GoToState(CharacterStateBase newState) {
            currentState.EndState();
            currentState = newState;
            currentState.BeginState();
        }

        public struct CharacterStates {
            public IdleState IdleState;
            public MovingState MovingState;
            public JumpingState JumpingState;
            public FallingState FallingState;
        }
        
        public abstract class CharacterStateBase {
            protected CharacterController2D controller;

            public CharacterStateBase(CharacterController2D controller) {
                this.controller = controller;
            }
            
            public bool BeginState() {
                return true;
            }

            public bool EndState() {
                return true;
            }

            public CharacterStateBase Update(float deltaTime) {
                return null;
            }

            public abstract void HandleInput(InputCommandData inputCommandData);
        }
    }
}