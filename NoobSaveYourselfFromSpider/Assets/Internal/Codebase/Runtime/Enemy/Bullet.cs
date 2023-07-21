// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.Enemy
{
    public sealed class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float destroyDelay = 10f;

        private void Start() => Invoke(nameof(DestroyBullet), destroyDelay);

        private void Update() => 
            transform.Translate(Vector2.right * (speed * Time.deltaTime));

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log("Hit"); // Выводим "Hit" в консоль при столкновении с игроком
                DestroyBullet();
            }
        }

        private void DestroyBullet() => 
            Destroy(gameObject);
    }
}