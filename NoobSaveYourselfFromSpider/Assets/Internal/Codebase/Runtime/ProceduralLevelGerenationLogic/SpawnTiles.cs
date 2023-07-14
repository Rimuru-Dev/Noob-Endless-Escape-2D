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
    public sealed class SpawnTiles : MonoBehaviour
    {
        public GameObject[] tilePrefabs;
        public float spawnOffset = 2f;

        private List<GameObject> spawnedTiles = new List<GameObject>();
        private float tileWidth;

        void Start()
        {
            // Получаем ширину префаба по BoxCollider2D и сохраняем ее для последующих вычислений
            tileWidth = tilePrefabs[0].GetComponent<BoxCollider2D>().size.x;

            // Спавним несколько префабов, чтобы заполнить начальный экран
            SpawnTile(10);
        }

        void Update()
        {
            // Проверяем, нужно ли добавить новые префабы
            if (Camera.main.transform.position.x > spawnedTiles[spawnedTiles.Count - 3].transform.position.x)
            {
                SpawnTile(1);
            }

            // Удаляем префабы, которые уже прошли игровой экран
            if (Camera.main.transform.position.x > spawnedTiles[0].transform.position.x - tileWidth)
            {
                Destroy(spawnedTiles[0]);
                spawnedTiles.RemoveAt(0);
            }
        }

        void SpawnTile(int amount)
        {
            // Спавним заданное количество префабов
            for (int i = 0; i < amount; i++)
            {
                // Выбираем случайный префаб из массива
                GameObject tilePrefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];

                // Рассчитываем позицию для спавна нового префаба
                Vector3 spawnPos = Vector3.zero;
                if (spawnedTiles.Count > 0)
                {
                    spawnPos = spawnedTiles[spawnedTiles.Count - 1].transform.position +
                               new Vector3(tileWidth - spawnOffset, 0f, 0f);
                }
                else
                {
                    spawnPos = Camera.main.transform.position;
                }

                // Спавним префаб и добавляем его в список спавненных префабов
                GameObject spawnedTile = Instantiate(tilePrefab, spawnPos, Quaternion.identity);
                spawnedTile.transform.SetParent(transform);
                spawnedTiles.Add(spawnedTile);
            }
        }
    }
}