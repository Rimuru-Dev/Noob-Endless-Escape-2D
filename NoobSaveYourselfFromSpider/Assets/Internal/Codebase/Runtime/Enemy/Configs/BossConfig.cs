// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using UnityEngine;

namespace Internal.Codebase.Runtime.Enemy.Configs
{
    [CreateAssetMenu(menuName = "Configs/Create " + nameof(BossConfig), fileName = nameof(BossConfig), order = 0)]
    public sealed class BossConfig : ScriptableObject
    {
        [field: SerializeField] public float LifeTime { get; set; } = 60;
        [field: SerializeField] public float WaitTime { get; set; } = 60;
        [field: SerializeField] public float MoveSpeed { get; set; } = 60;
        [field: SerializeField] public Vector3 InitialPosition { get; set; } = new(-15f, 0, 0);
        [field: SerializeField] public Vector3 TargetPosition { get; set; }
        [field: SerializeField] public Vector3 AppearancePosition { get; set; } = new(-7f, 0f, 0f);

        [field: SerializeField] public GameObject Bullet { get; set; }
        [field: SerializeField] public float AttackSpeed { get; set; }
        [field: SerializeField] public float FlyOutDuration { get; set; } = 4f;
        [field: SerializeField] public float FlyOutDistance { get; set; } = 10f;
        [field: SerializeField] public float SyncSpeed { get; set; } = 2f;
        [field: SerializeField] public float AttackTime { get; set; } = 10f;
        [field: SerializeField] public float AttackCooldown { get; set; }=2.4f;
    }
}