// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class InfinitePathGenerator2 : MonoBehaviour
    {
        public Tilemap pathTilemap;
        public GameObject[] pathPrefabs;
        public float pathSpeed = 5f;

        private List<GameObject> activePaths = new List<GameObject>();
        private float pathLength;
        private float screenLeftEdge;

        private void Start()
        {
            foreach (GameObject pathPrefab in pathPrefabs)
            {
                Tilemap prefabTilemap = pathPrefab.GetComponent<Tilemap>();
                pathLength += prefabTilemap.size.x * prefabTilemap.cellSize.x;
            }

            screenLeftEdge = GetScreenLeftEdge();
            GenerateInitialPaths();

            StartCoroutine(GeneratePaths());
        }

        private void GenerateInitialPaths()
        {
            float currentX = transform.position.x;
            for (int i = 0; i < 4; i++) // Создаем начальные блоки пути
            {
                Vector3 spawnPosition = new Vector3(currentX, transform.position.y, transform.position.z);
                GameObject newPath = Instantiate(GetRandomPathPrefab(), spawnPosition, Quaternion.identity);
                newPath.transform.parent = transform;
                activePaths.Add(newPath);
                currentX += GetPathPrefabWidth(newPath);
                newPath.GetComponent<Tilemap>().color = GetRandomColor();
            }
        }

        private IEnumerator GeneratePaths()
        {
            while (true)
            {
                // Генерируем новый блок пути
                Vector3 spawnPosition =
                    new Vector3(screenLeftEdge + pathLength, transform.position.y, transform.position.z);
                GameObject newPath = Instantiate(GetRandomPathPrefab(), spawnPosition, Quaternion.identity);
                newPath.transform.parent = transform;
                activePaths.Add(newPath);
                newPath.GetComponent<Tilemap>().color = GetRandomColor();

                // Удаляем блоки пути, вышедшие за пределы экрана
                if (activePaths.Count > 1)
                {
                    GameObject firstPath = activePaths[0];
                    float firstPathRightEdge = firstPath.transform.position.x + GetPathPrefabWidth(firstPath);
                    if (firstPathRightEdge < screenLeftEdge)
                    {
                        Destroy(firstPath);
                        activePaths.RemoveAt(0);
                    }
                }

                yield return new WaitForSeconds(pathLength / pathSpeed);
            }
        }

        private float GetScreenLeftEdge()
        {
            float cameraSize = Camera.main.orthographicSize;
            float screenAspect = (float)Screen.width / Screen.height;
            float screenWidth = cameraSize * screenAspect;
            return Camera.main.transform.position.x - screenWidth;
        }

        private GameObject GetRandomPathPrefab()
        {
            int randomIndex = Random.Range(0, pathPrefabs.Length);
            return pathPrefabs[randomIndex];
        }

        private float GetPathPrefabWidth(GameObject pathPrefab)
        {
            Tilemap prefabTilemap = pathPrefab.GetComponent<Tilemap>();
            return prefabTilemap.size.x * prefabTilemap.cellSize.x;
        }

        private Color GetRandomColor()
        {
            float r = Random.Range(0f, 1f);
            float g = Random.Range(0f, 1f);
            float b = Random.Range(0f, 1f);
            return new Color(r, g, b);
        }
    }
}