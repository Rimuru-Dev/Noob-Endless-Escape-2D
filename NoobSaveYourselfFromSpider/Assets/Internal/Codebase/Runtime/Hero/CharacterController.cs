// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.Hero
{
    public sealed class CharacterController : MonoBehaviour
    {
        public float jumpForce = 5f; // Сила прыжка
        public float gravityScale = 1f; // Масштаб гравитации
        public Transform groundCheck; // Позиция объекта, определяющего нахождение на земле
        public LayerMask groundLayer; // Слой, определяющий землю
        public float groundCheckRadius = 0.2f; // Радиус поиска земли
        public Transform cameraTarget; // Цель камеры, следящей за персонажем

        private Rigidbody2D rb; // Компонент Rigidbody2D
        private bool isGrounded = false; // Флаг, отвечающий за нахождение на земле

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            // Проверяем, находится ли персонаж на земле
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

            // Если нажата клавиша прыжка и персонаж находится на земле, делаем прыжок
            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
        }

        private void FixedUpdate()
        {
            // Применяем гравитацию к персонажу
            rb.AddForce(new Vector2(0, Physics2D.gravity.y * gravityScale));

            // Обновляем позицию камеры, чтобы она следила за персонажем
            if (cameraTarget != null)
            {
                Vector3 targetPosition = cameraTarget.position;
                targetPosition.z = -10f; // Позиция камеры по оси Z
                Camera.main.transform.position = targetPosition;
            }
        }

        private void Jump()
        {
            // Придаем персонажу вертикальную силу для прыжка
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            // Переворачиваем персонажа при каждом прыжке
            transform.Rotate(new Vector3(0, 0, 180));
        }
    }
}