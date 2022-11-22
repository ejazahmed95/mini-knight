using RangerRPG.Core;
using TMPro;
using UnityEngine.UI;

namespace MiniKnight.Debug {
    public class UIDebugger : SingletonBehaviour<UIDebugger> {
        public GenericDictionary<string, Button> buttons = new();
        public TMP_Text stateText;
        public TMP_Text coinsText;

        public void ActivateButtons(params string[] keys) {
            foreach (var btn in buttons.Values) {
                btn.interactable = false;
            }
            
            foreach (var key in keys) {
                if(buttons.ContainsKey(key) == false) continue;

                buttons[key].interactable = true;
            }
        }

        public void SetStateText(string state) {
            stateText.text = state;
        }

        public void SetCoinsText(int coins) {
            coinsText.SetText($"x {coins}");
        }
    }
}