using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Uktena64Randomizer.Archipelago;
using Uktena64Randomizer.Data;
using UnityEngine;

namespace Uktena64Randomizer.Patches;

public class ClickBlocker
{
    [HarmonyPatch(typeof(MainMenu), "Click")]
    [HarmonyPrefix]
    private static bool Click_BlockClickCallsForSeveralReasons(MainMenu __instance, int which)
    {
        if (!ArchipelagoClient.Authenticated)
        {
            return true;
        }

        Plugin.Log.LogInfo($"Click!  Which: {which}, Current Query: {__instance.current_query}");
        switch (which)
        {
            case 26:
            {
                var query = __instance.current_query;
                if (UktenaItems.JebClickToLevel.TryGetValue(query, out var level))
                {
                    if (!ArchipelagoClient.ServerData.Inventory.ContainsKey(level))
                    {
                        return false;
                    }
                }

                if (query == 4)
                {
	                if (!ArchipelagoClient.ServerData.Inventory.TryGetValue("Jeb Level Complete", out var count))
	                {
		                return false;
	                }

	                if (count < 4)
	                {
		                return false;
	                }
                }

                if (query == 0 && ArchipelagoClient.ServerData.PlayableCampaign == Campaign.Jeeb)
                {
                    return false;
                }

                break;
            }
            case 38:
            {
                var query = __instance.current_query;
                if (UktenaItems.JeebClickToLevel.TryGetValue(query, out var level))
                {
                    if (!ArchipelagoClient.ServerData.Inventory.ContainsKey(level))
                    {
                        return false;
                    }
                }

                if (query == 4)
                {
	                if (!ArchipelagoClient.ServerData.Inventory.TryGetValue("Jeeb Level Complete", out var count))
	                {
		                return false;
	                }

	                if (count < 4)
	                {
		                return false;
	                }
                }

                if (query == 0 && ArchipelagoClient.ServerData.PlayableCampaign == Campaign.Jeb)
                {
                    return false;
                }

                break;
            }
        }

        return true;
    }

    [HarmonyPatch(typeof(act_time), "OnEnable")]
    [HarmonyPrefix]
    private static bool OnEnable_DontWorkForChurchKey(act_time __instance)
    {
	    if (__instance.transform.parent is null) return true;
        if (__instance.transform.parent.name == "Church Key Picked up")
        {
	        __instance.gameObject.SetActive(false);
            return false;
        }

        return true;
    }

    [HarmonyPatch(typeof(InSolid), "Click")]
    [HarmonyPrefix]
    private static bool Click_DontIncrementPictureCountOnPhotographer(InSolid __instance, ref int ___taken_pics)
    {
	    if (!ArchipelagoClient.Authenticated) return true;
        if (!ArchipelagoClient.ServerData.Photographer) return true;

        int num = ___taken_pics;
        if (__instance.HIT > 0)
        {
            __instance.CONTROL.PLAYER.SendMessage("BeBusy");
            for (int i = 0; i < __instance.HIT; i++)
            {
                if (__instance.Targets[i] != null)
                {
                    __instance.Targets[i].gameObject.layer = 12;
                    __instance.CONTROL.CheckforBlip(__instance.Targets[i].gameObject);
                    if (__instance.Targets[i].GetComponent<Animation>() != null)
                    {
                        __instance.Targets[i].GetComponent<Animation>().Play();
                    }

                    __instance.Targets[i] = null;
                }
            }

            __instance.CONTROL.CurrentData.shots++;
            if (__instance.CONTROL.PICS_REMAINING <= 0)
            {
                __instance.CONTROL.BUTTONS[2].SetActive(value: true);
            }
            //SavePic();
        }

        if (LocationFinder.PicMeatSent)
        {
            __instance.GetComponent<AudioSource>().Play();
            int num2 = Mathf.RoundToInt(Random.Range(0, __instance.Sayings_Jeb.Length));
            __instance.Txts[1].text = __instance.Sayings_Jeb[num2];
            PlayerPrefs.SetString("RATING" + __instance.CONTROL.CurrentData.shots, __instance.Sayings_Jeb[num2]);
            __instance.CONTROL.PLAYER.gameObject.GetComponent<Player_Feet>().PLAY_SND(__instance.VOICE_JEB[num2]);
            __instance.Animy2.GetComponent<Animation>().Stop();
            __instance.Animy2.GetComponent<Animation>().Play("Pic_Take");
            LocationFinder.PicMeatSent = false;
        }

        return false;
    }

    [HarmonyPatch(typeof(InSolid), "Butch")]
    [HarmonyPrefix]
    private static bool Butch_DontSendMeatIfBBQChef(InSolid __instance, ref int ___taken_pics, ref int __result)
    {
	    if (!ArchipelagoClient.Authenticated) return true;
	    if (!ArchipelagoClient.ServerData.BBQChef) return true;
	    
        int num = ___taken_pics;
		int result = 0;
		if (__instance.HIT > 0)
		{
			__instance.CONTROL.PLAYER.SendMessage("BeBusy");
			for (int i = 0; i < __instance.HIT; i++)
			{
				if (!(__instance.Targets[i] != null))
				{
					continue;
				}
				__instance.Targets[i].gameObject.layer = 12;
				__instance.CONTROL.CheckforBlip(__instance.Targets[i].gameObject);
				if (__instance.Targets[i].GetComponent<Animation>() != null)
				{
					__instance.Targets[i].GetComponent<Animation>().Play();
				}
				if (__instance.Targets[i].GetComponent<SkeletonHolder>() != null)
				{
					__instance.Targets[i].GetComponent<SkeletonHolder>().BoneZone();
				}
				string text = __instance.Targets[i].transform.parent.gameObject.name;
				bool flag = false;
				if (__instance.CONTROL.CurrentData.ingrediants == null)
				{
					__instance.CONTROL.CurrentData.ingrediants = new List<string>();
					__instance.CONTROL.CurrentData.ingrediants.Add(text + "01");
					__instance.Targets[i] = null;
					continue;
				}
				for (int j = 0; j < __instance.CONTROL.CurrentData.ingrediants.Count; j++)
				{
					if (__instance.CONTROL.CurrentData.ingrediants[j].Contains(text))
					{
						int num2 = int.Parse(__instance.CONTROL.CurrentData.ingrediants[j].Substring(__instance.CONTROL.CurrentData.ingrediants[j].Length - 2, 2));
						num2++;
						__instance.CONTROL.CurrentData.ingrediants[j] = __instance.CONTROL.CurrentData.ingrediants[j].Substring(0, __instance.CONTROL.CurrentData.ingrediants[j].Length - 2);
						if (num2 > 9)
						{
							__instance.CONTROL.CurrentData.ingrediants[j] += num2;
						}
						else
						{
							List<string> ingrediants = __instance.CONTROL.CurrentData.ingrediants;
							int index = j;
							ingrediants[index] = ingrediants[index] + "0" + num2;
						}
						flag = true;
						j = 999;
					}
				}
				if (!flag)
				{
					__instance.CONTROL.CurrentData.ingrediants.Add(text + "01");
				}
				__instance.Targets[i] = null;
			}
		}
		if (LocationFinder.PicMeatSent)
		{
			__instance.transform.GetChild(0).GetComponent<AudioSource>().Play();
			__instance.GetComponent<AudioSource>().Play();
			int num3 = Mathf.RoundToInt(Random.Range(0, __instance.Sayings.Length));
			__instance.Txts[5].text = __instance.Sayings[num3];
			__instance.CONTROL.PLAYER.gameObject.GetComponent<Player_Feet>().PLAY_SND(__instance.VOICE[num3]);
			__instance.Animy.GetComponent<Animation>().Stop();
			__instance.Animy.GetComponent<Animation>().Play("GetMeat");
			LocationFinder.PicMeatSent = false;
		}

		__result = result;
		return false;
    }

    [HarmonyPatch(typeof(CutScene), "OnEnable")]
    [HarmonyPostfix]
    private static void OnEnable_Nah(CutScene __instance)
    {
	    if (Plugin.CurrentScene != "Demo_Cabin_1" && Plugin.CurrentScene != "Echo_BBQ") return;
	    var skipMethod = __instance.GetType().GetMethod("SKIP", BindingFlags.Instance | BindingFlags.NonPublic);
	    skipMethod.Invoke(__instance, null);
    }

}