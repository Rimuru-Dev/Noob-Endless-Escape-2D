// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;
using UnityEngine.Tilemaps;

namespace Trash
{
    public sealed class TilemapSpawner : MonoBehaviour
    {
        public Tilemap[] tilemaps; // Массив префабов Tilemap
        public float moveSpeed = 1f; // Скорость движения

        private BoxCollider2D lastCollider; // Последний созданный коллайдер

        private void Start()
        {
            SpawnTilemap(); // Инициализация первого префаба
        }

        private void Update()
        {
            MoveTilemap(); // Движение префабов
        }

        private void SpawnTilemap()
        {
            int randomIndex = Random.Range(0, tilemaps.Length); // Выбор случайного префаба из массива
            Tilemap newTilemap =
                Instantiate(tilemaps[randomIndex], transform.position, Quaternion.identity); // Создание нового префаба

            BoxCollider2D newCollider = newTilemap.GetComponent<BoxCollider2D>(); // Получение коллайдера нового префаба

            Vector3 offset =
                new Vector3(lastCollider.bounds.max.x - newCollider.bounds.min.x, 0,
                    0); // Вычисление смещения для присоединения

            newTilemap.transform.position += offset; // Смещение нового префаба

            lastCollider = newCollider; // Обновление последнего коллайдера
        }

        private void MoveTilemap()
        {
            transform.Translate(Vector3.left * moveSpeed *
                                Time.deltaTime); // Движение объекта со скоростью moveSpeed влево
        }
    }
}