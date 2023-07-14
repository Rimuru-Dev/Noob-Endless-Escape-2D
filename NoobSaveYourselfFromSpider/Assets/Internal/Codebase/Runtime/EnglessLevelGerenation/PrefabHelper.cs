// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using System.Linq;
using NaughtyAttributes;
using UnityEngine;

namespace Internal.Codebase.Runtime.EnglessLevelGerenation
{
    [SelectionBase]
    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    public sealed class PrefabHelper : MonoBehaviour
    {
        #region Fields

        [SerializeField] private bool canSetLayerForAll;

        [SerializeField, ShowIf("canSetLayerForAll")]
        private int spriteRendererSortingOrder;

        [SerializeField] private BoxCollider2D prefabHeight;

        #endregion

        #region Class Interface's

        public Vector2 PrefabSize => prefabHeight.size;
        public Vector2 PrefabOffset => prefabHeight.offset;
        public Bounds PrefabBounds => prefabHeight.bounds;

        #endregion

        #region Editor Validate

        [System.Diagnostics.Conditional("DEBUG")]
        private void OnValidate()
        {
            SetNewSpriteRendererSortingOrder();

            ValidatePrefabHeight();
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void SetNewSpriteRendererSortingOrder()
        {
            if (!canSetLayerForAll)
                return;

            GetComponentsInChildren<SpriteRenderer>(true)
                .Where(sr => sr != null)
                .ToList()
                .ForEach(sr => { sr.sortingOrder = spriteRendererSortingOrder; });
        }

        [System.Diagnostics.Conditional("DEBUG")]
        private void ValidatePrefabHeight()
        {
            if (prefabHeight != null)
            {
                if (prefabHeight.isTrigger == false)
                    prefabHeight.isTrigger = true;

                return;
            }

#if UNITY_EDITOR
            Debug.Log(
                "<color=green>Very important! Pass the prefab to the field 'prefabHeight' type 'BoxCollider2D'!!!'\n'Otherwise, the level generation will break.</color>");
            Debug.Log(
                "<color=green>Очень важно! Передайте префаб в поле 'prefabHeight' типа 'BoxCollider2D'!!!'\n'Иначе сломается генерация уровня.</color>");
#endif
        }

        #endregion
    }
}