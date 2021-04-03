﻿using System;
using HMUI;
using IPA.Utilities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using AdvancedNoHUD.CustomTypes;

namespace AdvancedNoHUD
{
    internal static class Accessors
    {
        //internal static readonly FieldAccessor<PlayerOptionsViewController, Toggle>.Accessor PlayerSettings = FieldAccessor<PlayerOptionsViewController, Toggle>.GetAccessor("_noTextsAndHudsToggle");
    }


    public class HUDManager
    {
        public static GameObject Combo;
        public static GameObject Score;
        public static GameObject Rank;
        public static GameObject Multiplier;
        public static GameObject Progress;
        public static GameObject Health;



        static int HiddenHudLayer = 23;
        static int NormalHudLayer = 5;



        public static Camera LIVCam;
        public static GameObject HUD;


        


        


        public static void FindHUDElements()
        {
            if(HUD != null)
            {
                //i know this is jank but in case some idiot has counters+ installs and breaks shit
                try { Combo = GameObject.Find("ComboPanel"); } catch {}

                try { Score = GameObject.Find("ScoreText"); } catch { }
                
                try {
                    Rank = GameObject.Find("ImmediateRankText");
                    GameObject.Find("RelativeScoreText").gameObject.transform.SetParent(Rank.transform);
                } catch { }
                
                try { Multiplier = GameObject.Find("MultiplierCanvas"); } catch { }
                
                try { Progress = GameObject.Find("SongProgressCanvas"); } catch { }
                
                try { Health = GameObject.Find("EnergyPanel"); } catch { }
                
            }
            else { findHUD(); }
        }


        public static void PutThings(whereHUD wh)
        {
            LocationPreset tempPreset = new LocationPreset();

            switch (wh)
            {
                case whereHUD.HMD:
                    tempPreset = Configuration.PluginConfig.Instance.HMD;
                    break;
                case whereHUD.Pause:
                    tempPreset = Configuration.PluginConfig.Instance.Pause;
                    break;
            }

            try
            {
                Combo.SetActive(tempPreset.elements.combo);
                Score.SetActive(tempPreset.elements.score);
                Rank.SetActive(tempPreset.elements.rank);
                Multiplier.SetActive(tempPreset.elements.multiplier);
                Progress.SetActive(tempPreset.elements.progress);
                Health.SetActive(tempPreset.elements.health);
            }
            catch (NullReferenceException)
            {
                FindHUDElements();
                PutThings(wh);
            }

        }
        public static void HideInLiv()
        {
            HudElements elements = Configuration.PluginConfig.Instance.LIV.elements;

            if (!elements.combo)
                Combo.layer = 23;
            else
                Combo.layer = 5;

            if (!elements.score)
                Score.layer = 23;
            else
                Score.layer = 5;

            if (!elements.rank)
                Rank.layer = 23;
            else
                Rank.layer = 5;

            if (!elements.multiplier)
                Multiplier.layer = 23;
            else
                Multiplier.layer = 5;

            if (!elements.progress)
                Progress.layer = 23;
            else
                Progress.layer = 5;

            if (!elements.health)
                Health.layer = 23;
            else
                Health.layer = 5;

        }
        public static void FindLivCamera()
        {
            int hudToggle(int flag, bool show = true) => show ? flag | 1 << HiddenHudLayer : flag & ~(1 << HiddenHudLayer);

            foreach (var cam in Resources.FindObjectsOfTypeAll<Camera>())
            {
                if (cam.name == "MainCamera")
                {
                    cam.cullingMask = hudToggle(cam.cullingMask, false);

                    if (cam.name != "MainCamera")
                        continue;

                    var x = cam.GetComponent<LIV.SDK.Unity.LIV>();

                    if (x != null)
                        x.SpectatorLayerMask = HiddenHudLayer;
                }
                else
                {
                    cam.cullingMask = hudToggle(cam.cullingMask, (cam.cullingMask & (1 << NormalHudLayer)) != 0);
                }
            }
        }

        public static void findHUD()
        {
            HUD = GameObject.Find("BasicGameHUD");
            if (HUD == null)
                HUD = GameObject.Find("NarrowGameHUD");
            if (HUD == null)
                HUD = GameObject.Find("FlyingGameHUD");
        }

        

    }
}
