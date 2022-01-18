using IPA;
using IPA.Config;
using IPA.Config.Stores;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using HarmonyLib;
using AdvancedNoHUD.HarmonyPatches;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using UnityEngine.SceneManagement;
using AdvancedNoHUD.UI.ViewControllers;
using AdvancedNoHUD.UI;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace AdvancedNoHUD
{
    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin
    {
        static AdvancedNoHUDFlowCoordinator FC;
        internal static Plugin Instance { get; private set; }
        internal static IPALogger Log { get; private set; }
        internal static Harmony harmony { get; private set; }
        internal static bool Found { get; set; } = false;

        [Init]
        /// <summary>
        /// Called when the plugin is first loaded by IPA (either when the game starts or when the plugin is enabled if it starts disabled).
        /// [Init] methods that use a Constructor or called before regular methods like InitWithConfig.
        /// Only use [Init] with one Constructor.
        /// </summary>
        public void Init(IPALogger logger)
        {
            Instance = this;
            Log = logger;

            harmony = new Harmony("headassbtw.AdvancedNoHUD");
            harmony.PatchAll(Assembly.GetExecutingAssembly());

            MenuButton menuButton = new MenuButton("AdvancedNoHUD", "Manage when and where the HUD is shown", ShowFlow);
            MenuButtons.instance.RegisterButton(menuButton);
            Log.Info("AdvancedNoHUD initialized.");
        }

        public static void ShowFlow()
        {
            if (FC == null)
                FC = BeatSaberUI.CreateFlowCoordinator<AdvancedNoHUDFlowCoordinator>();
            BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(FC);
        }

        #region BSIPA Config        
        [Init]
        public void InitWithConfig(IPA.Config.Config conf)
        {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
            Log.Debug("Config loaded");
        }
        
        #endregion

        [OnStart]
        public void OnApplicationStart()
        {
            new GameObject("AdvancedNoHUDController").AddComponent<AdvancedNoHUDController>();

            

            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;


        }

        public void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            if (arg1.name.Contains("Menu"))
            {
                
                SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            }
                
        }


        [OnExit]
        public void OnApplicationQuit()
        {
            
        }
    }
}
