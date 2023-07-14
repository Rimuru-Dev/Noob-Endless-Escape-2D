// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Internal.Codebase.Runtime.ProceduralLevelGerenationLogic
{
    public sealed class EndlessPathGenerationController : MonoBehaviour
    {
        public Transform root;
        public Transform spawnPoint;
        public GameObject[] prefabs;
        public float speed = -5;
        public int spawnThreshold = 2; // Количество платформ, прошедших за экран, для спавна новых

        private List<GameObject> pool = new List<GameObject>();
        private float gapThreshold = 40f;

        private void Start()
        {
            float xPos = spawnPoint.position.x;

            foreach (var prefab in prefabs)
            {
                var instance = Instantiate(prefab, root);
                var position = new Vector2(xPos, spawnPoint.position.y);

                instance.transform.position = position;
                pool.Add(instance);

                var collider = instance.GetComponent<BoxCollider2D>();
                xPos += collider.size.x;
            }
        }

        private void Update()
        {
            for (int i = pool.Count - 1; i >= 0; i--)
            {
                if (pool[i] != null)
                {
                    pool[i].transform.position = new Vector2(pool[i].transform.position.x + speed * Time.deltaTime,
                        pool[i].transform.position.y);

                    if (pool[i].transform.position.x < spawnPoint.position.x - gapThreshold)
                    {
                        // Удаление частей уровня, которые вышли за границу экрана
                        Destroy(pool[i]);
                        pool.RemoveAt(i);
                    }
                }
            }

            // Генерация новых частей уровня, если количество платформ, прошедших за экран, достигает порога
            int platformsPassed = 0;
            foreach (var platform in pool)
            {
                if (platform.transform.position.x <= spawnPoint.position.x)
                {
                    platformsPassed++;
                }
            }

            if (platformsPassed <= spawnThreshold)
            {
                float xPos = pool[pool.Count - 1].transform.position.x;
                foreach (var prefab in prefabs)
                {
                    var instance = Instantiate(prefab, root);
                    var position = new Vector2(xPos, spawnPoint.position.y);

                    instance.transform.position = position;
                    pool.Add(instance);

                    var collider = instance.GetComponent<BoxCollider2D>();
                    xPos += collider.size.x;
                }
            }
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

        private float GetSizeX(GameObject target)
        {
            var boxCollider2D = target.GetComponent<BoxCollider2D>();

            return boxCollider2D != null ? boxCollider2D.size.x : 0f;
        }

        private float GeOffsetX(GameObject target)
        {
            var boxCollider2D = target.GetComponent<BoxCollider2D>();

            return boxCollider2D != null ? boxCollider2D.offset.x : 0f;
        }
    }
}