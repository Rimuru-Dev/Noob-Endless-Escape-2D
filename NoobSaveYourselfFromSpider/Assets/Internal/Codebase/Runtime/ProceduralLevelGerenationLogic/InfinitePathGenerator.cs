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
    public sealed class InfinitePathGenerator : MonoBehaviour
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
                pathLength += pathPrefab.GetComponent<Tilemap>().size.x;
            }

            screenLeftEdge = GetScreenLeftEdge();
            StartCoroutine(GeneratePaths());
        }

        private IEnumerator GeneratePaths()
        {
            while (true)
            {
                // Генерация нового участка пути из случайного префаба
                GameObject newPath = Instantiate(GetRandomPathPrefab(), transform.position, Quaternion.identity);
                newPath.transform.parent = transform;

                // Добавление нового участка пути в список активных участков
                activePaths.Add(newPath);

                yield return new WaitForSeconds(pathLength / pathSpeed);
            }
        }

        private void Update()
        {
            // Движение активных участков пути
            foreach (GameObject path in activePaths)
            {
                path.transform.position += Vector3.left * pathSpeed * Time.deltaTime;

                // Удаление участков пути, которые вышли за пределы экрана
                if (path.transform.position.x + pathLength < screenLeftEdge)
                {
                    Destroy(path);
                    activePaths.Remove(path);
                    break; // Выход из цикла, чтобы не изменять коллекцию во время итерации
                }
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
    }
}