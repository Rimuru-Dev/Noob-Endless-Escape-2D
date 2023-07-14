// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections.Generic;
using UnityEngine;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class EndlessPathGenerator : MonoBehaviour
    {
        public GameObject[] pathPrefabs; // Массив префабов пути
        private List<GameObject> spawnedPaths = new List<GameObject>(); // Список созданных префабов пути
        private float pathWidth; // Ширина префаба пути

        public float spawnOffsetX = 10f; // Расстояние между путями за экраном
        public int startPathCount = 6; // Количество стартовых путей

        private void Start()
        {
            // Получаем ширину префаба пути из BoxCollider2D первого префаба
            pathWidth = pathPrefabs[0].GetComponentInChildren<BoxCollider2D>().size.x;

            // Создаем стартовые пути
            for (int i = 0; i < startPathCount; i++)
            {
                SpawnPath();
            }
        }

        private void Update()
        {
            // Если первый префаб пути вышел за пределы экрана слева, удаляем его из списка
            if (spawnedPaths.Count > 0 && spawnedPaths[0].transform.position.x + pathWidth / 2f <
                Camera.main.transform.position.x - spawnOffsetX)
            {
                Destroy(spawnedPaths[0]);
                spawnedPaths.RemoveAt(0);
            }

            // Если последний префаб пути вышел за пределы экрана налево, создаем новый путь
            if (spawnedPaths.Count > 0 && spawnedPaths[spawnedPaths.Count - 1].transform.position.x + pathWidth / 2f <
                Camera.main.transform.position.x + spawnOffsetX)
            {
                SpawnPath();
            }
        }

        private void SpawnPath()
        {
            // Случайным образом выбираем префаб пути из массива
            int randomIndex = Random.Range(0, pathPrefabs.Length);

            // Создаем новый префаб пути
            GameObject newPath = Instantiate(pathPrefabs[randomIndex], transform);

            // Позиционируем новый префаб пути относительно предыдущего
            Vector3 spawnPosition = Vector3.zero;
            if (spawnedPaths.Count > 0)
            {
                float spawnOffset = pathWidth / 2f +
                                    spawnedPaths[spawnedPaths.Count - 1].GetComponentInChildren<BoxCollider2D>().size
                                        .x / 2f;
                spawnPosition = spawnedPaths[spawnedPaths.Count - 1].transform.position -
                                new Vector3(spawnOffset, 0f, 0f);
            }

            newPath.transform.position = spawnPosition;

            // Добавляем новый префаб пути в список
            spawnedPaths.Add(newPath);
        }
    }
}