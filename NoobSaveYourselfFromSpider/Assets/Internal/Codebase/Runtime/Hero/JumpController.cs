// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.Hero
{
    public sealed class JumpController : MonoBehaviour
    {
        public float jumpForce = 5f;
        public float rotationAngle = 90f;
        public float rotationSpeed = 5f;

        private Rigidbody2D rb;
        private bool isJumping;
        private Quaternion targetRotation;
        private Vector3 initialPosition;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            initialPosition = transform.position;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) && !isJumping)
                Jump();

            if (Mathf.Abs(transform.position.x) > 0f)
                ReturnToInitialPosition();
        }

        private void Jump()
        {
            isJumping = true;

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            targetRotation = Quaternion.Euler(0f, 0f, transform.eulerAngles.z + rotationAngle);
        }

        private void FixedUpdate()
        {
            if (isJumping)
                Rotate();
        }

        private void Rotate()
        {
            transform.rotation =
                Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
                isJumping = false;
        }

        private void ReturnToInitialPosition()
        {
            var targetPosition = new Vector3(initialPosition.x, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, rotationSpeed * Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D collision) =>
            isJumping = false;
    }
}