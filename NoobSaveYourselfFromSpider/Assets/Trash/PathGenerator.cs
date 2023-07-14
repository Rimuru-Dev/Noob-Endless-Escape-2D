// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Trash
{
    public sealed class PathGenerator : MonoBehaviour
    {
        public Transform[] pathPrefabs;
        public Transform pathHolderLeft;
        public Transform pathHolderRight;
        public float pathSpeed = 1f;
        public float deleteXPosition = -20f;

        private Transform lastPathLeft;
        private Transform lastPathRight;

        private void Start()
        {
            GeneratePath();
        }

        private void Update()
        {
            MovePath();
        }

        private void GeneratePath()
        {
            int randomIndex = Random.Range(0, pathPrefabs.Length);
            Transform newPathLeft = Instantiate(pathPrefabs[randomIndex], pathHolderLeft);
            Transform newPathRight = Instantiate(pathPrefabs[randomIndex], pathHolderRight);

            // Позиционируем путь слева и справа от камеры
            if (lastPathLeft == null)
            {
                newPathLeft.position = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));
                newPathRight.position = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));
            }
            else
            {
                SpriteRenderer spriteRenderer = newPathLeft.GetComponentInChildren<SpriteRenderer>();
                float pathWidth = spriteRenderer.bounds.size.x;

                newPathLeft.position = lastPathLeft.position + new Vector3(pathWidth, 0, 0);
                newPathRight.position = lastPathRight.position + new Vector3(pathWidth, 0, 0);
            }

            lastPathLeft = newPathLeft;
            lastPathRight = newPathRight;
        }

        private void MovePath()
        {
            pathHolderLeft.position -= new Vector3(pathSpeed * Time.deltaTime, 0, 0);
            pathHolderRight.position -= new Vector3(pathSpeed * Time.deltaTime, 0, 0);

            if (lastPathLeft.position.x <= deleteXPosition)
            {
                DestroyLastPath();
                GeneratePath();
            }
        }

        private void DestroyLastPath()
        {
            Destroy(lastPathLeft.gameObject);
            Destroy(lastPathRight.gameObject);
        }
    }
}