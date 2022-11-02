using System.Collections.Generic;
using MiniKnight.Player;
using RangerRPG.Core;
using UnityEngine;

namespace MiniKnight {
    public class GameManager : SingletonBehaviour<GameManager> {
        public Player.CharacterController2D playerRef;
        public CharacterInputHandler inputHandler;

        public GenericDictionary<Player.CharacterController2D.PlayerSkill, List<GameObject>> GatesToOpenOnSkill = new();
        
        public void AcquireDoubleJump() { // Acquire Double Jump
            var skill = Player.CharacterController2D.PlayerSkill.DOUBLE_JUMP;
            playerRef.UnlockSkill(skill);
            if (GatesToOpenOnSkill.ContainsKey(skill)) {
                foreach (var gameObj in GatesToOpenOnSkill[skill]) {
                    Destroy(gameObj);
                }
            }
        }

        public void AcquireWallClimb() {
            var skill = Player.CharacterController2D.PlayerSkill.WALL_CLIMB;
            playerRef.UnlockSkill(skill);
            if (GatesToOpenOnSkill.ContainsKey(skill)) {
                foreach (var gameObj in GatesToOpenOnSkill[skill]) {
                    Destroy(gameObj);
                }
            }
        }

        public void AcquireDashAbility() {
            var skill = Player.CharacterController2D.PlayerSkill.DASHING;
            playerRef.UnlockSkill(skill);
            if (GatesToOpenOnSkill.ContainsKey(skill)) {
                foreach (var gameObj in GatesToOpenOnSkill[skill]) {
                    Destroy(gameObj);
                }
            }
        }
    }
}