// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Collections;
using Cinemachine;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.Helpers
{
    [RequireComponent(typeof(CinemachineVirtualCamera))]
    public sealed class FindHero : MonoBehaviour
    {
        private const float Cooldown = 0.3f;
        private const string Player = nameof(Player);

        private GameObject hero;
        private CinemachineVirtualCamera cinemachineVirtualCamera;

        private void Start()
        {
            cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();

            if (cinemachineVirtualCamera.Follow != null)
                return;
            
            StartCoroutine(TryFindHero());
        }

        private IEnumerator TryFindHero()
        {
            if (cinemachineVirtualCamera.Follow != null)
                yield break;

            while (hero == null)
            {
                Debug.Log("FIND");
                hero = GameObject.FindGameObjectWithTag(Player);

                yield return new WaitForSeconds(Cooldown);
            }

            if (cinemachineVirtualCamera != null)
                cinemachineVirtualCamera.Follow = hero.transform;
        }
    }
}