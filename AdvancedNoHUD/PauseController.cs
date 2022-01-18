using System.Reflection;
using System.Collections.Generic;
using HarmonyLib;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvancedNoHUD.HarmonyPatches
{
	[HarmonyPatch("Awake")]
	[HarmonyPatch(typeof(AudioTimeSyncController), nameof(AudioTimeSyncController.StartSong))]
	class HookAudioTimeSyncController
	{
		static void Postfix(AudioTimeSyncController __instance)
		{
#if DEBUG
			Plugin.Log.Info("AudioTimeSyncController.StartSong()");
#endif
			if (HUDManager.findHUD())
			{
				Plugin.Found = true;
				HUDManager.FindHUDElements();
				HUDManager.PutThings(CustomTypes.whereHUD.HMD);
			}
#if DEBUG
			Plugin.Log.Info("Finished Setup");
#endif
		}
	}



	[HarmonyPatch]
	class HookAudioTimeSyncController2
	{
		static void Postfix()
		{
#if DEBUG
			Plugin.Log.Info("AudioTimeSyncController.Pause()");
#endif
			if(Plugin.Found)
				HUDManager.PutThings(CustomTypes.whereHUD.Pause);
		}

		[HarmonyTargetMethods]
		static IEnumerable<MethodBase> TargetMethods()
		{
			yield return AccessTools.Method(typeof(AudioTimeSyncController), nameof(AudioTimeSyncController.Pause));
		}
	}



	[HarmonyPatch]
	class HookAudioTimeSyncController3
	{
		static void Postfix()
		{
#if DEBUG
			Plugin.Log.Info("AudioTimeSyncController.Resume()");
#endif
			if(Plugin.Found)
				HUDManager.PutThings(CustomTypes.whereHUD.HMD);
		}

		[HarmonyTargetMethods]
		static IEnumerable<MethodBase> TargetMethods()
		{
			yield return AccessTools.Method(typeof(AudioTimeSyncController), nameof(AudioTimeSyncController.Resume));
		}
	}
}
