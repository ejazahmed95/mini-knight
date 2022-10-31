using System.Collections;
using RangerRPG.Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace MiniKnight.Player {
    public class CharacterInputHandler : SingletonBehaviour<CharacterInputHandler> {
        [field:SerializeField] public InputActionReference MoveAction;
        [field:SerializeField] public InputActionReference JumpAction;
        [field:SerializeField] public InputActionReference AttackAction;
        [field:SerializeField] public InputActionReference ShootAction;
        [field:SerializeField] public InputActionReference DashAction;

        [field:SerializeField] public UnityEvent<InputCommandData> inputEvent = new();

        private void Start() {
            MoveAction.action.performed += OnMoveAction;
            MoveAction.action.canceled += OnMoveAction;
            
            JumpAction.action.performed += OnJump;
            AttackAction.action.performed += OnAttack;
            ShootAction.action.performed += OnShoot;
            DashAction.action.performed += OnDash;
            
            ShootAction.action.Disable();
            DashAction.action.Disable();
        }

        private void OnMoveAction(InputAction.CallbackContext obj) {
            var moveValue = obj.ReadValue<float>();
            
            inputEvent.Invoke(new InputCommandData{Type = InputCommandType.MOVE, ValueFloat = moveValue});
        }
        
        private void OnJump(InputAction.CallbackContext obj) {
            var jumpVal = obj.ReadValueAsButton();
            Log.Trace($"Jump Value = {jumpVal.ToString()}, Value Type = {obj.valueType}");
            
            inputEvent.Invoke(new InputCommandData{Type = InputCommandType.JUMP, ValueBool = jumpVal});
        }
        
        private void OnAttack(InputAction.CallbackContext obj) {
            var jumpVal = obj.ReadValueAsButton();
            Log.Trace($"Attack Value = {jumpVal.ToString()}, Value Type = {obj.valueType}");
            
            inputEvent.Invoke(new InputCommandData{Type = InputCommandType.ATTACK, ValueBool = jumpVal});
        }
        
        private void OnShoot(InputAction.CallbackContext obj) {
            var jumpVal = obj.ReadValueAsButton();
            Log.Trace($"Shoot Value = {jumpVal.ToString()}, Value Type = {obj.valueType}");
            
            inputEvent.Invoke(new InputCommandData{Type = InputCommandType.SHOOT, ValueBool = jumpVal});
        }
        
        private void OnDash(InputAction.CallbackContext obj) {
            var jumpVal = obj.ReadValueAsButton();
            Log.Trace($"Dash Value = {jumpVal.ToString()}, Value Type = {obj.valueType}");
            
            inputEvent.Invoke(new InputCommandData{Type = InputCommandType.DASH, ValueBool = jumpVal});
        }
    }
}