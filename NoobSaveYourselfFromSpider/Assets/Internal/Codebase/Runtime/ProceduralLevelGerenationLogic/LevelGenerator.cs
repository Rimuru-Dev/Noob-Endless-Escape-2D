// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class LevelGenerator : MonoBehaviour
    {
        public Transform player;
        public Tilemap tilemap;
        public GameObject[] prefabTiles;
        public float spawnDistance = 10f;
        public int maxTiles = 5;

        private List<GameObject> spawnedTiles = new List<GameObject>();
        private float totalWidth = 0f;
        private float tileWidth = 0f;

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
            float moveSpeed = 2f; // Устанавливайте свою скорость движения уровня
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
            Bounds currentBounds = GetTileBounds(currentTile);
            Bounds previousBounds = GetTileBounds(previousTile);

            float offsetX = currentBounds.center.x - currentBounds.min.x + previousBounds.center.x -
                            previousBounds.max.x;
            float offsetY = currentBounds.center.y - currentBounds.min.y + previousBounds.center.y -
                            previousBounds.max.y;
            currentTile.transform.position += new Vector3(-offsetX, -offsetY, 0f);
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
                return collider.size.x;
            }

            return 0f;
        }

        private Bounds GetTileBounds(GameObject tile)
        {
            BoxCollider2D collider = tile.GetComponent<BoxCollider2D>();
            if (collider != null)
            {
                return collider.bounds;
            }

            return new Bounds();
        }
    }
}