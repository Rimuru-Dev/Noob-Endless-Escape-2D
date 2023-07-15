// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Internal.Codebase.Runtime.EnglessLevelGerenation
{
    /// <summary>
    /// RU: Конфиг необходимый для процедурного генератора мира.
    /// <code></code>>
    /// EN: The config needed for the procedural world generator.
    /// </summary>
    [CreateAssetMenu(menuName = "Configs/Create " + nameof(EndlessPathGenerationConfig), fileName = nameof(EndlessPathGenerationConfig), order = 0)]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public sealed class EndlessPathGenerationConfig : ScriptableObject
    {
        [field: Tooltip("Стартовая платформа. Первая платформа при генерации мира/пути.")]
        [field: SerializeField] public Transform LaunchingPlatform { get; private set; }
        
        [field: Tooltip("Список уникальных префабов *конкретного биома для генерации мира/пути.")]
        [field: SerializeField] public GameObject[] Prefabs { get; private set; }

        [field: Tooltip("Координаты относительно которых начнется генерация мира/пути. ")]
        [field: SerializeField] public Vector2 StartSpawnPoint { get; private set; } = new Vector2(-7.87f, -3f);

        [field: Tooltip("Скорость движения/прокрутки уровня. Отрицательная скорость означает прокрутку уровня в лево. Положительная в право.")]
        [field: SerializeField, Range(-100f, 100f)] public float LevelScrollingSpeed { get; private set; } = -2f;
        
        [field: Tooltip("Позиция в мире по оси X зайдя за пределы которой объекты уровня будут созданы.")]
        [field: SerializeField, Range(-100f, 100f)] public float SpawnOffset { get; private set; } = 40f;
        
        [field: Tooltip("Позиция в мире по оси X зайдя за пределы которой объекты уровня будут уничтожаны.")]
        [field: SerializeField, Range(-100f, 100f)] public float DespawnOffset { get; private set; } = 20f;
        
        [field: Tooltip("Максимально возможное кол-во блоков которое может быть создано в мире.")]
        [field: SerializeField, Range(0, 100)] public int MaxBlockCount { get; private set; } = 20;
    }
}