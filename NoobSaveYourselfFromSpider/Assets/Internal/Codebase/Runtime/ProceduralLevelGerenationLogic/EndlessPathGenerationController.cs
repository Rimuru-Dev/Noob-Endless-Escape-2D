// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class EndlessPathGenerationController : MonoBehaviour
    {
        public Transform root;
        public GameObject[] prefabs;
        public Vector3 spawnPosition = Vector3.zero;
        public float tileWidth;

        private void Start()
        {
            tileWidth = GetTileWidth(prefabs[0]);
            var i0 = Instantiate(prefabs[0], Vector3.zero, Quaternion.identity, root);

            var i1 = Instantiate(prefabs[1], root);
            var pos = new Vector2(i0.GetComponent<BoxCollider2D>().size.x, i0.GetComponent<BoxCollider2D>().size.y) - new Vector2(i1.GetComponent<BoxCollider2D>().size.x, i1.GetComponent<BoxCollider2D>().size.y);
            i1.transform.position = pos;

            i1.transform.position =
                new Vector2(i0.GetComponent<BoxCollider2D>().offset.y + i1.GetComponent<BoxCollider2D>().offset.y,
                    i1.transform.position.y);
        }

        private float GetTileWidth(GameObject tile)
        {
            BoxCollider2D collider = tile.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                return collider.bounds.size.x;
            }

            return 0f;
        }

        // private Vector2 GetTileSize(GameObject tile)
        // {
        //     BoxCollider2D collider = tile.GetComponent<BoxCollider2D>();
        //     if (collider != null)
        //     {
        //         return collider.bounds.size;
        //     }
        //
        //     return Vector2.zero;
        // }
        //
        // private void AttachTileToPrevious(GameObject currentTile, GameObject previousTile)
        // {
        //     Vector2 currentOffset = GetTileSize(currentTile) / 2f;
        //     Vector2 previousOffset = GetTileSize(previousTile) / 2f;
        //     float offsetX = tileWidth / 2f + currentOffset.x + previousOffset.x;
        //     currentTile.transform.position = new Vector3(previousTile.transform.position.x + offsetX, 0f, 0f);
        // }

        // private void Start()
        // {
        //     GameObject lastSpawnedPrefab = null;
        //
        //     foreach (GameObject prefab in prefabs)
        //     {
        //         // Получаем BoxCollider2D префаба
        //         BoxCollider2D prefabCollider = prefab.GetComponent<BoxCollider2D>();
        //
        //         // Получаем размеры и смещение коллайдера
        //         Vector2 size = prefabCollider.size;
        //         Vector2 offset = prefabCollider.offset;
        //
        //         // Вычисляем позицию для нового префаба
        //         Vector3 newPosition = spawnPosition + new Vector3(offset.x * prefab.transform.localScale.x,
        //             offset.y * prefab.transform.localScale.y, 0);
        //
        //         if (lastSpawnedPrefab != null)
        //         {
        //             // Получаем BoxCollider2D предыдущего префаба
        //             BoxCollider2D lastPrefabCollider = lastSpawnedPrefab.GetComponent<BoxCollider2D>();
        //
        //             // Получаем размеры и смещение коллайдера предыдущего префаба
        //             Vector2 lastSize = lastPrefabCollider.size;
        //             Vector2 lastOffset = lastPrefabCollider.offset;
        //
        //             // Прибавляем ширину предыдущего префаба для правильного выравнивания
        //             newPosition.x += (lastSize.x / 2f + size.x / 2f) * lastSpawnedPrefab.transform.localScale.x;
        //         }
        //
        //         // Спавним префаб
        //         GameObject spawnedPrefab = Instantiate(prefab, newPosition, Quaternion.identity, root);
        //
        //         // Обновляем spawnPosition на позицию последнего заспавненного префаба
        //         spawnPosition = spawnedPrefab.transform.position +
        //                         new Vector3(size.x * spawnedPrefab.transform.localScale.x, 0, 0);
        //
        //         // Сохраняем последний заспавненный префаб
        //         lastSpawnedPrefab = spawnedPrefab;
        //     }
        // }
    }
}