// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.General.SpriteTextNumberCounterLogic;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Timer
{
    [DisallowMultipleComponent]
    public sealed class GameTimer : MonoBehaviour
    {
        public event Action OnTimerOn;
        public event Action OnTimerOff;

        [SerializeField] private bool isCountdownStarted;
        [SerializeField] private float initialCooldownTimer = 3f;
        [SerializeField] private float countdownTimer = 3f;
        [SerializeField] private NumberVisualizer numberVisualizer;

        private void Update()
        {
            if (!isCountdownStarted)
                return;

            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0f)
            {
                OnTimerOff?.Invoke();
                
                isCountdownStarted = false;
                gameObject.SetActive(false);
                numberVisualizer.gameObject.SetActive(false);
            }

            var displayTimer = (int)Mathf.Ceil(countdownTimer);

            numberVisualizer.ShowNumber(displayTimer);
        }

        [ContextMenu("Start Countdown")]
        public void StartCountdown()
        {
            if (isCountdownStarted)
                return;

            OnTimerOn?.Invoke();
            
            countdownTimer = initialCooldownTimer;

            isCountdownStarted = true;

            gameObject.SetActive(true);

            numberVisualizer.gameObject.SetActive(true);
            numberVisualizer.ShowNumber((int)countdownTimer);
        }
    }
}