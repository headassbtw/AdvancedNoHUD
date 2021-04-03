using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using HMUI;
using AdvancedNoHUD.UI.ViewControllers;

namespace AdvancedNoHUD.UI
{
    class AdvancedNoHUDFlowCoordinator : FlowCoordinator
    {
        private GameplayViewController _GameplayViewController;
        private PauseViewController _PauseViewController;
        private LIVViewController _LIVViewController;
        public void Awake()
        {
            if (!_GameplayViewController)
                _GameplayViewController = BeatSaberUI.CreateViewController<GameplayViewController>();
            if (!_PauseViewController)
                _PauseViewController = BeatSaberUI.CreateViewController<PauseViewController>();
            //if (!_LIVViewController)
            //    _LIVViewController = BeatSaberUI.CreateViewController<LIVViewController>();
        }
        protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            try
            {
                if (firstActivation)
                {
                    SetTitle("HUD Settings");
                    showBackButton = true;
                    ProvideInitialViewControllers(_GameplayViewController, _PauseViewController);
                }
            }
            catch (Exception e)
            {
                Plugin.Log.Error(e);
            }
        }

        protected override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
            _GameplayViewController.o();
            _PauseViewController.o();
            //_LIVViewController.o();
        }
    }
}
