// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Diagnostics.CodeAnalysis;
using Internal.Codebase.Runtime.GameplayScene.LevelGeneration.PrefabHelper;
using UnityEngine;

namespace Internal.Codebase.Runtime.GameplayScene.LevelGeneration.Configs
{
    /// <summary>
    /// RU: Конфиг необходимый для процедурного генератора мира.
    /// <code></code>>
    /// EN: The config needed for the procedural world generator.
    /// </summary>
    [CreateAssetMenu(menuName = "Configs/Create " + nameof(EndlessLevelGenerationConfig), fileName = nameof(EndlessLevelGenerationConfig), order = 0)]
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    public sealed class EndlessLevelGenerationConfig : ScriptableObject
    {
        public Sprite background;
        [field: SerializeField]
        public BiomeTypeID BiomeTypeID { get; private set; }
        
        [field: Header("Settings")]
        [field: Tooltip("Стартовая платформа. Первая платформа при генерации мира/пути.")]
        [field: SerializeField]
        public Prefab LaunchingPlatform { get; private set; }

        [field: Tooltip("Список уникальных префабов *конкретного биома для генерации мира/пути.")]
        [field: SerializeField]
        public Prefab[] Prefabs { get; private set; }

        [field: Header("Advanced Settings")]
        [field: Tooltip("Координаты относительно которых начнется генерация мира/пути. ")]
        [field: SerializeField]
        public Vector2 StartSpawnPoint { get; private set; } = new(-7.87f, -3f);

        [field: Tooltip("Скорость движения/прокрутки уровня. Отрицательная скорость означает прокрутку уровня в лево. Положительная в право.")]
        [field: SerializeField]
        public float LevelScrollingSpeed { get; private set; } = 5f;

        [field: Tooltip("Позиция в мире по оси X зайдя за пределы которой объекты уровня будут уничтожаны.")]
        [field: SerializeField]
        public float DespawnOffset { get; private set; } = 20f;

        [field: Tooltip("Максимально возможное кол-во блоков которое может быть создано в мире.")]
        [field: SerializeField]
        public int MaxBlockCount { get; private set; } = 20;

        [field: Tooltip("Задержка между спавном объектов.")]
        [field: SerializeField]
        public float SpawnCooldown { get; private set; } = 0.3f;
    }
}