using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using UnityEngine.UI;
using HMUI;

namespace AdvancedNoHUD.UI.ViewControllers
{
    class LIVViewController : BSMLResourceViewController
    {
        public override string ResourceName => "AdvancedNoHUD.UI.Views.HUDControls.bsml";
        [UIValue("setting-scene")] string SettingScene = "LIV";
        private bool _hudEn = Configuration.PluginConfig.Instance.LIV.everything;
        [UIValue("enabled-bool")] public bool IngameHudEnabled {
            get => _hudEn;
            set
            {
                _hudEn = value;
                NotifyPropertyChanged();
            }
        }


        private bool _comboEn = Configuration.PluginConfig.Instance.LIV.elements.combo;
        [UIValue("combo-bool")] public bool IngameComboEnabled
        {
            get => _comboEn;
            set
            {
                _comboEn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _scoreEn = Configuration.PluginConfig.Instance.LIV.elements.score;
        [UIValue("score-bool")] public bool IngameScoreEnabled
        {
            get => _scoreEn;
            set
            {
                _scoreEn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _rankEn = Configuration.PluginConfig.Instance.LIV.elements.rank;
        [UIValue("rank-bool")] public bool IngameRankEnabled
        {
            get => _rankEn;
            set
            {
                _rankEn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _multiplierEn = Configuration.PluginConfig.Instance.LIV.elements.multiplier;
        [UIValue("multiplier-bool")] public bool IngameMultiplierEnabled
        {
            get => _multiplierEn;
            set
            {
                _multiplierEn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _progressEn = Configuration.PluginConfig.Instance.LIV.elements.progress;
        [UIValue("progress-bool")] public bool IngameProgressEnabled
        {
            get => _progressEn;
            set
            {
                _progressEn = value;
                NotifyPropertyChanged();
            }
        }
        private bool _healthEn = Configuration.PluginConfig.Instance.LIV.elements.health;
        [UIValue("health-bool")] public bool IngameHealthEnabled
        {
            get => _healthEn;
            set
            {
                _healthEn = value;
                NotifyPropertyChanged();
            }
        }


        [UIAction("yes")]
        public void o()
        {
            var _AAAAAAA = new CustomTypes.HudElements(IngameComboEnabled, IngameScoreEnabled, IngameRankEnabled, IngameMultiplierEnabled, IngameProgressEnabled, IngameHealthEnabled);
            var _BBBBBBB = new CustomTypes.LocationPreset(CustomTypes.whereHUD.LIV, _AAAAAAA, IngameHudEnabled);
            Configuration.PluginConfig.Instance.LIV = _BBBBBBB;
        }
    }
}
