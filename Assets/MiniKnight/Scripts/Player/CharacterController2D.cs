using MiniKnight.Debug;
using RangerRPG.Core;
using RangerRPG.Utility;
using UnityEngine;

namespace MiniKnight.Player {
    public partial class CharacterController2D : MonoBehaviour {

        [SerializeField] private PlayerStateData stateData;

        private Rigidbody2D _rigidbody;
        
        private Animator _animator;
        private CharacterStateBase currentState;
        private CharacterInputHandler input;
        private CharacterStates AllStates;
        private UIDebugger _debugger;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
        }

        private void Start() {

            input = DI.Get<CharacterInputHandler>();
            _debugger = DI.Get<UIDebugger>();
            
            AllStates = new CharacterStates {
                IdleState = new IdleState(this),
                MovingState = new MovingState(this),
                JumpingState = new JumpingState(this),
                FallingState = new FallingState(this)
            };
            currentState = AllStates.IdleState;
            currentState.BeginState(out _);
            
            input.inputEvent.AddListener(HandleInput);
        }

        void HandleInput(InputCommandData data) {
            var state = currentState.HandleInput(data);
            if (state != null) {
                GoToState(state);
            }
        }

        private void FixedUpdate() {
            var state = currentState.FixedUpdate();
            if (state != null) {
                GoToState(state);
            }
        }

        private void Update() {
            var state = currentState.Update(Time.deltaTime);
            if (state != null) {
                GoToState(state);
            }
        }

        private void GoToState(CharacterStateBase newState) {
            Log.Info($"Ending State = {stateData.ActiveStateText}".Color("red"));
            currentState.EndState();
            
            CharacterStateBase nextState = newState;
            for (int i = 0; i < 5; i++) { // Max Chaining for beginning states
                currentState = nextState;
                if (currentState.BeginState(out nextState)) {
                    _debugger.SetStateText(stateData.ActiveStateText);
                    Log.Info($"Starting State = {stateData.ActiveStateText}".Color("green"));
                    return;
                }
            }
            currentState = AllStates.IdleState;
            currentState.BeginState(out _);
            Log.Info($"Starting State = {stateData.ActiveStateText}".Color("green"));
        }

        public struct CharacterStates {
            public IdleState IdleState;
            public MovingState MovingState;
            public JumpingState JumpingState;
            public FallingState FallingState;
        }
        
        
    }
}