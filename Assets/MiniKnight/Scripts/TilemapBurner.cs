using UnityEngine;
using UnityEngine.Tilemaps;

namespace MiniKnight {
    public class TilemapBurner : MonoBehaviour {
        public Tilemap map;
        public Vector2Int size = Vector2Int.one;

        public void BurnTiles() {
            Vector3 pos = transform.position;

            for (int x = 0; x < size.x; x++) {
                for (int y = 0; y < size.y; y++) {
                    var cellPosition = map.WorldToCell(pos + new Vector3(x, y));
                    map.SetTile(cellPosition, null);
                }
            }
           
        }
    }
}