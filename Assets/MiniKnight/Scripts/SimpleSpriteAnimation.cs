using System.Collections.Generic;
using UnityEngine;

namespace MiniKnight {
    public class SimpleSpriteAnimation : MonoBehaviour {
        public List<Sprite> sprites;
        public int fps = 5;
        private float elapsedTime = 0;
        private int frameIndex = 0;

        private bool stopped = false;

        public Color disabledColor;
        
        public void StopAnimation() {
            stopped = true;
            _spriteRenderer.color = disabledColor;
        }

        private SpriteRenderer _spriteRenderer;
        private float timeToNextFrame;

        private void Awake() {
            if (fps == 0) fps = 5;
            timeToNextFrame = 1.0f / fps;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update() {
            if (stopped) return;
            elapsedTime += Time.deltaTime;
            if (elapsedTime > timeToNextFrame) {
                elapsedTime -= timeToNextFrame;
                frameIndex = (frameIndex + 1) % sprites.Count;
                _spriteRenderer.sprite = sprites[frameIndex];
            }
        }

    }
}