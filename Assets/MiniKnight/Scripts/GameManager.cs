using System.Collections.Generic;
using MiniKnight.Debug;
using MiniKnight.Player;
using RangerRPG.Core;
using UnityEngine;

namespace MiniKnight {
    public class GameManager : SingletonBehaviour<GameManager> {
        public Player.CharacterController2D playerRef;
        public CharacterInputHandler inputHandler;
        public GameObject gameEndText;
        
        public GenericDictionary<Player.CharacterController2D.PlayerSkill, List<GameObject>> GatesToOpenOnSkill = new();

        public int playerCoins = 0;
        
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
        
        public void AddCoins(int i) {
            playerCoins += i;
            UIDebugger.Instance.SetCoinsText(playerCoins);
        }
        
        public void GameWon() {
            gameEndText.SetActive(true);
        }
        
    }
}