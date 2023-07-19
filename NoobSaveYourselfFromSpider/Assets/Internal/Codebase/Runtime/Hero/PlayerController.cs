// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.Hero
{
    public class PlayerController : MonoBehaviour
    {
        public float jumpForce = 5f;
        private bool isJumping = false;
        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private int jumpCount = 0;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
            {
                Jump();
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                isJumping = false;
            }
        }

        private void Jump()
        {
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;

            // Поворот на +90 градусов
            transform.rotation = Quaternion.Euler(0, 0, 90 * jumpCount);
            jumpCount++;
        }
    }

}