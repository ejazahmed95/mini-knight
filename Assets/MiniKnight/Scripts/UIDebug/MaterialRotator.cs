using UnityEngine;

namespace MiniKnight.Debug {
    public class MaterialRotator : MonoBehaviour {
        SpriteRenderer _renderer;
        public float rotation = 0;
        public float rotationSpeed = 5;
        private static readonly int Rotation = Shader.PropertyToID("Rotation");

        void Start() {
            _renderer = GetComponent<SpriteRenderer> ();

            // // Use the Specular shader on the material
            // _renderer.material.shader = Shader.Find("Specular");
        }

        void Update()
        {
            // Animate the rotation value
            rotation += Time.deltaTime * rotationSpeed;
            if (rotation > 1000) rotation = 0;
            _renderer.material.SetFloat("_Rotation", rotation);
        }
    }
}