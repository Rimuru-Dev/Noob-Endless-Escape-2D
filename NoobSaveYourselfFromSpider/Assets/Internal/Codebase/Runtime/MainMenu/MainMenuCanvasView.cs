// **************************************************************** //
//
//   Copyright (c) RimuruDev. All rights reserved.
//   Contact me: rimuru.dev@gmail.com
//
// **************************************************************** //

using Internal.Codebase.Infrastructure.Services.ActionUpdater;
using Internal.Codebase.Runtime.BiomeShop;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Codebase.Runtime.MainMenu
{
    public sealed class MainMenuCanvasView : MonoBehaviour
    {
        [field: SerializeField] public CurrancyUIView Emerald { get; private set; }
        [field: SerializeField] public CurrancyUIView Fish { get; private set; }
        [field: SerializeField] public SettingsUIView Settings { get; private set; }
        [field: SerializeField] public BestDistanceUIView BestDistance { get; private set; }

        [field: SerializeField] public Button PlayButton { get; private set; }
        [field: SerializeField] public BiomeShopView BiomeShopView { get; private set; }
        public BuyCurrency BuyCurrencyView;
        public BuyCurrency BuyCurrencyViewShortPanel;

        private IActionUpdaterService actionUpdaterService;

        public void Constructor( IActionUpdaterService actionUpdaterService)
        {
            this.actionUpdaterService = actionUpdaterService;
            this.actionUpdaterService.Subscribe(OnFixedUpdate, UpdateType.FixedUpdate);
            this.actionUpdaterService.Subscribe(OnUpdate, UpdateType.Update);
            this.actionUpdaterService.Subscribe(OnLateUpdate, UpdateType.LateUpdate);
            // updater?.Register(this as IUpdateable, this as IFixedUpdatable, this as ILateUpdatable);
        }

        public void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                actionUpdaterService.Unsubscribe(OnUpdate, UpdateType.Update);
                // var u = this as IUpdateable;
                // updater.Unregister(u);
                //     updater.UnregisterUpdatable(this);
            }

            Debug.Log("OnUpdate()");
        }

        public void OnLateUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                // var l = this as ILateUpdatable;
                // //updater.Unregister(l);
                // updater.UnregisterLateUpdatable(l);
                actionUpdaterService.Unsubscribe(OnLateUpdate, UpdateType.LateUpdate);
            }

            Debug.Log("OnLateUpdate()");
        }

        public void OnFixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                // var f = this as IFixedUpdatable;
                // // updater.Unregister(f);
                // updater.UnregisterFixedUpdatable(f);

                actionUpdaterService.Unsubscribe(OnFixedUpdate, UpdateType.FixedUpdate);
            }

            Debug.Log("OnFixedUpdate()");
        }
    }
}