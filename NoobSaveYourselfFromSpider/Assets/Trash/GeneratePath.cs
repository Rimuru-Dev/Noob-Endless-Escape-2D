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
    public sealed class GeneratePath : MonoBehaviour
    {
        public GameObject[] pathPrefabs; // Массив различных префабов пути
        public float moveSpeed = 1f; // Скорость движения пути

        private List<GameObject> activePaths = new List<GameObject>(); // Список активных префабов пути
        private float pathWidth; // Ширина префаба пути

        private void Start()
        {
            // Получаем ширину префаба пути из BoxCollider2D первого префаба
            BoxCollider2D collider = pathPrefabs[0].GetComponent<BoxCollider2D>();
            pathWidth = collider.bounds.size.x;

            // Создаем изначальные префабы пути
            for (int i = 0; i < 5; i++)
            {
                CreatePath();
            }
        }

        private void Update()
        {
            // Двигаем весь уровень влево
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            // Если первый префаб пути полностью вышел за пределы экрана, удаляем его и создаем новый префаб пути
            if (activePaths.Count > 0 && activePaths[0].transform.position.x + pathWidth < -Screen.width / 2)
            {
                Destroy(activePaths[0]);
                activePaths.RemoveAt(0);
                CreatePath();
            }
        }

        private void CreatePath()
        {
            // Случайным образом выбираем префаб пути из массива
            int randomIndex = Random.Range(0, pathPrefabs.Length);
            GameObject pathPrefab = pathPrefabs[randomIndex];

            // Создаем префаб пути и добавляем его в список активных префабов
            GameObject newPath = Instantiate(pathPrefab, transform.position, Quaternion.identity, transform);
            activePaths.Add(newPath);

            // Перемещаем новый префаб пути рядом с последним путем
            float newX = activePaths.Count * pathWidth;
            newPath.transform.position = new Vector2(newX, transform.position.y);
        }
    }
}