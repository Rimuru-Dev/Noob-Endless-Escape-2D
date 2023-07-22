// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System;
using Internal.Codebase.Runtime.SpriteTextNumberCounterLogic;
using UnityEngine;

namespace Internal.Codebase.Runtime
{
    public sealed class GameTimer : MonoBehaviour
    {
        [SerializeField] private bool isCountdownStarted;
        [SerializeField] private float initialCooldownTimer = 3f;
        [SerializeField] private float countdownTimer = 3f;
        [SerializeField] private NumberVisualizer numberVisualizer;

        public Action OnTimerOn;
        public Action OnTimerOff;
        
        private void Update()
        {
            if (!isCountdownStarted)
                return;

            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0f)
            {
                OnTimerOff?.Invoke();
                // Остановка таймера после завершения отсчета.
                isCountdownStarted = false;
                gameObject.SetActive(false);
                numberVisualizer.gameObject.SetActive(false);
            }

            // Обновление отображения таймера.
            var displayTimer = (int)Mathf.Ceil(countdownTimer);

            numberVisualizer.ShowNumber(displayTimer);
        }

        [ContextMenu("StartCountdown")]
        public void StartCountdown()
        {
            // Проверка, чтобы таймер не запускался повторно, пока не завершится текущий отсчет.
            if (!isCountdownStarted)
            {
                OnTimerOn?.Invoke();
                countdownTimer = initialCooldownTimer;
                
                isCountdownStarted = true;
                
                gameObject.SetActive(true);
                
                numberVisualizer.gameObject.SetActive(true);
                numberVisualizer.ShowNumber((int)countdownTimer);
            }
        }
    }
}