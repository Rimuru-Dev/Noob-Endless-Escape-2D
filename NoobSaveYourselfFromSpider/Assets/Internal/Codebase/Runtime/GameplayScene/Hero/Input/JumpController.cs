// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Hero.Input
{
    [SelectionBase]
    [DisallowMultipleComponent]
    public sealed class JumpController : MonoBehaviour
    {
        public Transform heroTransform;
        public float jumpForce = 5f;
        public float rotationAngle = 90f;
        public float rotationSpeed = 5f;
        public bool useMouseClick = true;
        public bool IsCanJump { get; set; } = true;

        public Rigidbody2D rb;
        private bool isJumping;
        private Quaternion targetRotation;
        private Vector3 initialPosition;
        private bool IsCanReturnToInitialPosition { get; set; } = true;

        private void Start() =>
            initialPosition = transform.position;

        private void Update()
        {
            if (!IsCanJump)
                return;

            if (useMouseClick && UnityEngine.Input.GetMouseButtonDown(0) && !isJumping ||
                (UnityEngine.Input.GetKeyDown(KeyCode.Space) && !isJumping))
                Jump();

            if (Mathf.Abs(transform.position.x) > 0f)
                ReturnToInitialPosition();
        }

        private void Jump()
        {
            isJumping = true;

            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            targetRotation = Quaternion.Euler(0f, 0f, heroTransform.eulerAngles.z + rotationAngle);
        }

        private void FixedUpdate()
        {
            if (isJumping)
                Rotate();
        }

        private void Rotate()
        {
            heroTransform.rotation =
                Quaternion.Lerp(heroTransform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            if (Quaternion.Angle(heroTransform.rotation, targetRotation) < 0.1f)
                isJumping = false;
        }

        private void ReturnToInitialPosition()
        {
            if (!IsCanReturnToInitialPosition)
                return;

            var position = heroTransform.position;

            var targetPosition = new Vector3(initialPosition.x, position.y, position.z);

            position = Vector3.Lerp(position, targetPosition, rotationSpeed * Time.deltaTime);

            heroTransform.position = position;
        }

        // ReSharper disable once UnusedParameter.Local
        private void OnCollisionEnter2D(Collision2D collision) =>
            isJumping = false;
    }
}