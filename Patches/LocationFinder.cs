using System.Reflection;
using HarmonyLib;
using Uktena64Randomizer.Archipelago;
using Uktena64Randomizer.Data;
using UnityEngine;
using UnityEngine.AI;

namespace Uktena64Randomizer.Patches;

public class LocationFinder
{
    public static bool PicMeatSent;
    
    [HarmonyPatch(typeof(ClickBox), "ACT")]
    [HarmonyPrefix]
    private static bool ACT_LocationsAndFixes(ClickBox __instance, ref bool ___acted, ref bool ___upped)
    {
        if (!ArchipelagoClient.Authenticated) return true;
        if (__instance.type == 0)
        {
            if (__instance.CONTROL == null)
            {
                __instance.CONTROL = GameObject.Find("CONTROL").GetComponent<GAME_CONTROL>();
            }
            ___acted = true;
            ___upped = true;
            __instance.CONTROL.PLAYER.Pickup(__instance);
            return false;
        }

        if (__instance.type == 15)
        {
            switch (__instance.sub_type)
            {
                case 1:
                    Plugin.ArchipelagoClient.SendLocation(451);
                    break;
                case 2:
                    Plugin.ArchipelagoClient.SendLocation(452);
                    break;
                case 6:
                    Plugin.ArchipelagoClient.SendLocation(453);
                    break;
            }
        }
        if (__instance.type != 2) return true;
        if (__instance.enabled && __instance.sub_type == 10)
        {
            Plugin.ArchipelagoClient.SendLocation(32);
        }

        return true;
    }
    
    
    [HarmonyPatch(typeof(Player_Control_scr), "Pickup")]
    [HarmonyPrefix]
    private static bool Pickup_SendLocationBasedOnInformation(Player_Control_scr __instance, ClickBox BOX)
    {
        if (!ArchipelagoClient.Authenticated)
        {
            return true;
        }
        var con = GameObject.Find("CONTROL").GetComponent<GAME_CONTROL>();
        var isJeeb = con.JEEB;
        var locationSent = false;
        if (isJeeb)
        {
            foreach (var location in UktenaLocations.BaseJeebLocations)
            {
                if (location.IsLocationInformationTheSame(BOX.gameObject.scene.name, BOX.gameObject.name,
                        BOX.transform.position))
                {
                    location.SendLocation();
                    if (location.Name == "The BBQ Basket: Binoculars in Barrel")
                    {
                        Plugin.ArchipelagoClient.SendLocation(153);
                    }
                    locationSent = true;
                }
            }

            if (ArchipelagoClient.ServerData.RogueScholar)
            {
                foreach (var location in UktenaLocations.BaseJeebLoreLocations)
                {
                    if (location.IsLocationInformationTheSame(BOX.gameObject.scene.name, BOX.gameObject.name,
                            BOX.transform.position))
                    {
                        location.SendLocation();
                        locationSent = true;
                    }
                }
            }

        }
        else
        {
            foreach (var location in UktenaLocations.BaseJebLocations)
            {
                if (location.IsLocationInformationTheSame(BOX.gameObject.scene.name, BOX.gameObject.name,
                        BOX.transform.position))
                {
                    location.SendLocation();
                    if (location.Name == "Jeb's Cabin: Rocking Chair Item")
                    {
                        Plugin.ArchipelagoClient.SendLocation(3);
                    }
                    locationSent = true;
                }
            }

            if (ArchipelagoClient.ServerData.RogueScholar)
            {
                foreach (var location in UktenaLocations.BaseJebLoreLocations)
                {
                    if (location.IsLocationInformationTheSame(BOX.gameObject.scene.name, BOX.gameObject.name,
                            BOX.transform.position))
                    {
                        location.SendLocation();
                        locationSent = true;
                    }
                }
            }
        }

        if (!locationSent)
        {
            Plugin.Log.LogWarning($"Name: {BOX.gameObject.name}, Type: {BOX.type}, Sub-Type: {BOX.sub_type}, Amount: {BOX.amount}");
            Plugin.Log.LogWarning($"Position: ({BOX.gameObject.transform.position.x}, {BOX.gameObject.transform.position.y}, {BOX.gameObject.transform.position.z})");
        }
        
        BOX.gameObject.SetActive(false);
        switch (BOX.type)
        {
            case 0:
            case 4:
                Object.Instantiate(Resources.Load("Effects/FLASH_AMMO"), __instance.Wep_Text[0].transform.parent);
                return false;
            case 1:
                Object.Instantiate(Resources.Load("Effects/FLASH_HEALTH"), __instance.Wep_Text[0].transform.parent);
                return false;
            case 8 when BOX.sub_type == 1:
                Object.Instantiate(Resources.Load("Effects/FLASH_AMMO"), __instance.Wep_Text[0].transform.parent);
                Object.Destroy(BOX.gameObject);
                return false;
            default:
                return true;
        }
        
        
    }

    [HarmonyPatch(typeof(Boss2Control), "Start")]
    [HarmonyPostfix]
    private static void Start_AlsoJustSendBanjo()
    {
        Plugin.ArchipelagoClient.SendLocation(197);
    }

    [HarmonyPatch(typeof(GAME_CONTROL), "CheckforBlip")]
    [HarmonyPrefix]
    private static void CheckforBlip_SeeIfThisGivesUsInfoOnTargetPicture(GameObject blip)
    {
        Plugin.Log.LogInfo("Checking Blip Information");
        if (blip == null)
        {
            Plugin.Log.LogInfo("The blip target is null");
        }
        else
        {
            var parentName = blip.transform.parent is not null ? blip.transform.parent.gameObject.name : "None";
            Plugin.Log.LogInfo($"Name: {blip.name}, Parent: {parentName}, Position: ({blip.transform.position.x}, {blip.transform.position.y}, {blip.transform.position.z})");
        }

        var additional = "";
        if (blip.GetComponent<EnemyTag>() is not null)
        {
            Plugin.Log.LogInfo($"Tag: {blip.GetComponent<EnemyTag>().info}");
            additional = $"_{blip.GetComponent<EnemyTag>().info}";
        }

        Plugin.Log.LogInfo($"Location Identifier: {blip.name}{additional}");
        foreach (var location in UktenaLocations.BasePhotoLocations)
        {
            if (location.IsLocationInformationTheSame(blip.gameObject.scene.name, blip.name + additional, Vector3.zero))
            {
                location.SendLocation();
                PicMeatSent = true;
            }
            // Sometimes the name is duplicated and there's no great way of documenting....maybe I should've attached to blip idk.
            if (location.IsLocationInformationTheSame(blip.gameObject.scene.name, blip.name, blip.transform.position))
            {
                location.SendLocation();
                PicMeatSent = true;
            }
        }
    }

    [HarmonyPatch(typeof(AI_simple), "Die")]
    [HarmonyPrefix]
    private static bool Die_TransferTagToDeadBody(AI_simple __instance, ref bool ___dying, ref bool ___GiveDeadMaterial, ref NavMeshAgent ___Agent)
    {
        ___dying = true;
        __instance.Player.gameObject.GetComponent<Player_Control_scr>().CONTROL.CheckforBlip(__instance.gameObject);
        if (__instance.Nesty != null)
        {
            __instance.Nesty.amount--;
        }
        var deadThing = Object.Instantiate(Resources.Load(__instance.Dead), __instance.transform.position, __instance.transform.rotation, __instance.transform.parent) as GameObject;
        if (__instance.gameObject.GetComponent<EnemyTag>() is not null)
        {
            var passedInfo = __instance.gameObject.GetComponent<EnemyTag>().info;
            var blipAdder = deadThing.transform.GetComponentInChildren<AddBlip>();
            if (blipAdder is not null)
            {
                var blipChild = deadThing.transform.GetComponentInChildren<AddBlip>().gameObject;
                blipChild.AddComponent<EnemyTag>();
                blipChild.GetComponent<EnemyTag>().info = passedInfo;
            }
            else
            {
                Plugin.Log.LogWarning("There was no AddBlip to work off of.");
            }
        }
        if (__instance.Alt_Dead)
        {
            deadThing.transform.localScale = __instance.transform.localScale;
            if (___GiveDeadMaterial)
            {
                deadThing.transform.GetChild(0).GetChild(2).GetComponent<SkinnedMeshRenderer>()
                    .sharedMaterial = __instance.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>()
                    .sharedMaterial;
            }
        }
        if (deadThing.GetComponent<Rigidbody>() != null && deadThing.GetComponent<Rigidbody>().useGravity)
        {
            deadThing.GetComponent<Rigidbody>().AddForce(deadThing.transform.forward * ___Agent.speed * 1.5f, ForceMode.VelocityChange);
        }
        if (__instance.StayAt != null && __instance.StayAt.name == "HIVE")
        {
            __instance.StayAt.GetComponent<Hive>().MemberLost(deadThing, __instance.gameObject);
        }
        Object.Destroy(__instance.gameObject);
        return false;
    }

    [HarmonyPatch(typeof(kill_other), "OnEnable")]
    [HarmonyPostfix]
    private static void KillOther_AlsoSendLevelCompleteCheck(kill_other __instance)
    {
        if (__instance.gameObject.scene.name != "Map_Menu") return;
        if (__instance.name != "WIN" && __instance.name != "WIN2") return;
        Plugin.Log.LogInfo($"Checking level {Plugin.CurrentLevel}");
        if (UktenaLocations.ClearLookup.TryGetValue(Plugin.CurrentLevel, out var ID))
        {
            
            Plugin.ArchipelagoClient.SendLocation(ID);
        }
    }
}