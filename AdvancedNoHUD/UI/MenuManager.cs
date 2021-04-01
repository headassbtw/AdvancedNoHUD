using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using HMUI;
using UnityEngine.UI;
using HarmonyLib;
using AdvancedNoHUD.UI;
using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.ViewControllers;

namespace AdvancedNoHUD.UI
{
	//[HarmonyPatch("Awake")]
	[HarmonyPatch(typeof(PlayerSettingsPanelController), "SetLayout")]
	internal class StaticlightsToggle
	{
		private static PlayerSettingsPanelController instance;

		private static Toggle toggle1;

		private static Toggle toggle2;

		private static GameObject replaceLabel;

		private static ToggleWithCallbacks replaceToggle;


		

		// Token: 0x06000009 RID: 9 RVA: 0x000022A0 File Offset: 0x000004A0
		[HarmonyPriority(-2147483648)]
		private static void Postfix(PlayerSettingsPanelController __instance, Toggle ____noTextsAndHudsToggle, Toggle ____advanceHudToggle)
		{
			if (__instance.transform.parent.name == "PlayerSettingsViewController" || StaticlightsToggle.instance != null)
			{
				return;
			}
			StaticlightsToggle.instance = __instance;
			StaticlightsToggle.toggle1 = ____noTextsAndHudsToggle;
			StaticlightsToggle.toggle2 = ____advanceHudToggle;

			GameObject one = toggle1.transform.parent.gameObject;
			GameObject two = toggle2.transform.parent.gameObject;

			one.gameObject.SetActive(false);
			float modifiedY = one.transform.position.y - 0.06f;
			two.transform.position = new Vector3(one.transform.position.x, modifiedY, one.transform.position.z);
			

			GameObject newButton = GameObject.Instantiate(GameObject.Find("BSMLButton"), two.transform);
			GameObject.Destroy(newButton.GetComponent<HoverHint>());
			RectTransform newBounds = newButton.GetComponent<RectTransform>();
			newBounds = two.GetComponent<RectTransform>();
			two.SetActive(false);
			var buttonText = newButton.GetComponentInChildren<CurvedTextMeshPro>();
			buttonText.text = "Advanced No HUD";
			buttonText.transform.localScale.Set(0.5f, 1.0f, 1.0f);
			newButton.transform.localScale.Set(2, 1, 1);
			newButton.transform.SetParent(two.transform);
			newButton.name = "AdvNoHudButton";
			//newButton.GetComponentInChildren<Button>().onClick = InSongFlow.oi();

			//StaticlightsToggle.Setup(true);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000022F8 File Offset: 0x000004F8
		public static void ToggleEffectState(bool setStatic)
		{
			EnvironmentEffectsFilterPreset environmentEffectsFilterPreset = setStatic ? EnvironmentEffectsFilterPreset.NoEffects : 0;
			StaticlightsToggle.toggle1.isOn = false;
			StaticlightsToggle.toggle2.isOn = true;
			StaticlightsToggle.instance.SetIsDirty();
		}

		/*
		public static void Setup(bool enable)
		{
			if (StaticlightsToggle.instance == null)
			{
				return;
			}
			Transform transform = StaticlightsToggle.instance.transform.Find("ViewPort/Content/CommonSection");
			transform.GetComponent<VerticalLayoutGroup>().enabled = true;
			
			//StaticlightsToggle.< Setup > g__disableNext | 7_0(StaticlightsToggle.toggle2, !enable).SetActive(!enable);
			Transform parent = StaticlightsToggle.toggle1.transform.parent;
			GameObject gameObject = parent.Find("Label").gameObject;
			if (StaticlightsToggle.replaceLabel == null)
			{
				StaticlightsToggle.replaceLabel = UnityEngine.Object.Instantiate<GameObject>(gameObject, parent);
				UnityEngine.Object.Destroy(StaticlightsToggle.replaceLabel.GetComponents<MonoBehaviour>().First((MonoBehaviour l) => l.GetType().Name == "LocalizedTextMeshProUGUI"));
				StaticlightsToggle.replaceLabel.GetComponent<CurvedTextMeshPro>().text = "Static Lights";
			}
			if (StaticlightsToggle.replaceToggle == null)
			{
				StaticlightsToggle.replaceToggle = UnityEngine.Object.Instantiate<ToggleWithCallbacks>(transform.GetComponentInChildren<ToggleWithCallbacks>(), parent);
				StaticlightsToggle.replaceToggle.onValueChanged.RemoveAllListeners();
				//StaticlightsToggle.replaceToggle.onValueChanged.AddListener(new UnityAction<bool>(StaticlightsToggle.ToggleEffectState));
			}
			gameObject.SetActive(!enable);
			parent.Find("SimpleTextDropDown").gameObject.SetActive(!enable);
			StaticlightsToggle.replaceLabel.SetActive(enable);
			StaticlightsToggle.replaceToggle.gameObject.SetActive(enable);
			if (enable)
			{
				StaticlightsToggle.replaceToggle.isOn = true;
				StaticlightsToggle.ToggleEffectState(true);
			}
		}*/
}



	class MenuManager
    {
        static RectTransform bounds;
        static Transform position;

        public static void DestroyExistingToggle(Toggle no, Toggle yes)
        {
            //bounds = GameObject.Find("ViewPort/Content/CommonSection/NoTextsAndHUDs").gameObject.GetComponent<RectTransform>();
            position = no.gameObject.GetComponent<Transform>();
            GameObject.Destroy(GameObject.Find("ViewPort/Content/CommonSection/NoTextsAndHUDs"));
            GameObject.Destroy(GameObject.Find("ViewPort/Content/CommonSection/AdvancedHUD"));
        }
        public static void CreateButton()
        {
            GameObject button = GameObject.Find("LevelDetail/ActionButtons/ActionButton");
            GameObject newButton = GameObject.Instantiate(button, position);
            RectTransform newBounds = newButton.AddComponent<RectTransform>();
            //newBounds = bounds;
        }
    }
}
