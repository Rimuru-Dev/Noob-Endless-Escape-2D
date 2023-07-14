// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using UnityEngine;

namespace Trash
{
    public sealed class LevelGenerator : MonoBehaviour
    {
        public GameObject[] prefabTiles;
        public int maxTiles = 50;

        private List<GameObject> spawnedTiles = new List<GameObject>();
        private float totalWidth;
        private float tileWidth;

        private void Start()
        {
            tileWidth = GetTileWidth(prefabTiles[0]);
            GenerateInitialTiles();
        }

        private void Update()
        {
            MoveLevel();
            CheckTiles();
        }

        private void GenerateInitialTiles()
        {
            float xPosition = 0f;
            for (int i = 0; i < maxTiles; i++)
            {
                GameObject tile = SpawnRandomTile();
                tile.transform.position = new Vector3(xPosition, 0f, 0f);
                spawnedTiles.Add(tile);
                xPosition += tileWidth;
                totalWidth += tileWidth;

                if (i > 0)
                {
                    AttachTileToPrevious(tile, spawnedTiles[i - 1]);
                }
            }
        }

        private void MoveLevel()
        {
            float moveSpeed = 2f;
            float newXPosition = transform.position.x - moveSpeed * Time.deltaTime;
            transform.position = new Vector3(newXPosition, transform.position.y, transform.position.z);
        }

        private void CheckTiles()
        {
            float despawnX = transform.position.x - (tileWidth * 2f);

            foreach (GameObject tile in spawnedTiles)
            {
                float tileX = tile.transform.position.x;
                if (tileX < despawnX)
                {
                    spawnedTiles.Remove(tile);
                    Destroy(tile);
                    SpawnNextTile(despawnX);
                    break;
                }
            }
        }

        private void SpawnNextTile(float despawnX)
        {
            GameObject tile = SpawnRandomTile();
            tile.transform.position = new Vector3(despawnX + tileWidth, 0f, 0f);
            spawnedTiles.Add(tile);
            totalWidth += tileWidth;

            AttachTileToPrevious(tile, spawnedTiles[spawnedTiles.Count - 2]);
        }

        private void AttachTileToPrevious(GameObject currentTile, GameObject previousTile)
        {
            Vector2 currentOffset = GetTileSize(currentTile) / 2f;
            Vector2 previousOffset = GetTileSize(previousTile) / 2f;
            float offsetX = tileWidth / 2f + currentOffset.x + previousOffset.x;
            currentTile.transform.position = new Vector3(previousTile.transform.position.x + offsetX, 0f, 0f);
        }

        private GameObject SpawnRandomTile()
        {
            int randomIndex = Random.Range(0, prefabTiles.Length);
            return Instantiate(prefabTiles[randomIndex], transform);
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

        private Vector2 GetTileSize(GameObject tile)
        {
            BoxCollider2D collider = tile.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                return collider.bounds.size;
            }

            return Vector2.zero;
        }
    }
}