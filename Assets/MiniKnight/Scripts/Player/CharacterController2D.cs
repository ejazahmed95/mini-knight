using System.Collections;
using MiniKnight.Debug;
using MiniKnight.StatSystem;
using Othello.Scripts;
using RangerRPG.Core;
using RangerRPG.Utility;
using UnityEngine;

namespace MiniKnight.Player {
    public partial class CharacterController2D : MonoBehaviour {

        [SerializeField] private PlayerStateData stateData;
        public CoinObjectPool coinObjectPool;

        private Rigidbody2D _rigidbody;
        
        private Animator _animator;
        private CharacterStateBase currentState;
        private CharacterInputHandler input;
        private CharacterStates AllStates;
        private UIDebugger _debugger;
        private HealthComponent _health;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody2D>();
            _animator = GetComponent<Animator>();
            _health = GetComponent<HealthComponent>();
        }

        private void Start() {

            input = DI.Get<CharacterInputHandler>();
            _debugger = DI.Get<UIDebugger>();

            AllStates = new CharacterStates {
                IdleState = new IdleState(this),
                MovingState = new MovingState(this),
                JumpingState = new JumpingState(this),
                FallingState = new FallingState(this),
                DashingState = new DashingState(this),
                WallGrabState = new WallGrabState(this),
                AttackingState = new AttackingState(this),
            };
            currentState = AllStates.IdleState;
            currentState.BeginState(out _);
            
            input.inputEvent.AddListener(HandleInput);

            if (stateData.CanDash) {
                input.EnableAction(input.DashAction);
            }
        }

        public void DealMeleeDamage() {
            if (currentState is AttackingState attackingState) {
                attackingState.DealDamage();
            }
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

        private void OnTriggerEnter2D(Collider2D col) {
            if (col.gameObject.GetComponent<PlayerPickup>()) {
                Log.Info("Item Picked!");
                var pickup = col.gameObject.GetComponent<PlayerPickup>();
                pickup.PickedItem();
                coinObjectPool.DisableCoin(pickup);
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
            public DashingState DashingState;
            public WallGrabState WallGrabState;
            public AttackingState AttackingState;
        }

        public enum PlayerSkill {
            DOUBLE_JUMP,
            WALL_CLIMB,
            DASHING,
        }

        public void UnlockSkill(PlayerSkill skill) {
            switch (skill) {
                case PlayerSkill.DOUBLE_JUMP:
                    stateData.CanDoubleJump = true;
                    break;
                case PlayerSkill.WALL_CLIMB:
                    stateData.CanWallGrab = true;
                    break;
                case PlayerSkill.DASHING:
                    stateData.CanDash = true;
                    input.EnableAction(input.DashAction);
                    break;
                default:
                    break;
            }
        }

        public void OnHit() {
            _health.invincible = true;
            StartCoroutine(InvincibleRoutine());
        }
        
        private IEnumerator InvincibleRoutine() {
            yield return new WaitForSeconds(2f);
            _health.invincible = false;
        }
    }
}