using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Uktena64Randomizer.Archipelago;
using Uktena64Randomizer.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Uktena64Randomizer.Patches;

public class LevelSetup
{
    private static int _instantCoffees = 0;

    [HarmonyPatch(typeof(Player_Control_scr), "Hurt")]
    [HarmonyPostfix]
    private static void Hurt_Heal20IfHurtEnough(Player_Control_scr __instance, float power)
    {
        if (_instantCoffees <= 0)
        {
            return;
        }
        Plugin.Log.LogInfo($"Current health {__instance.health}.  Will I heal?  {__instance.health is <= 50 and > 0}");
        if (__instance.health is <= 50 and > 0)
        {
            __instance.health += 20;
            __instance.Wep_Text[2].transform.GetChild(0).gameObject.GetComponent<Healthbar_scr>().CheckHealth(__instance.health / 100f);
        }

        _instantCoffees--;
    }
    
    [HarmonyPatch(typeof(Player_Control_scr), "Awake")]
    [HarmonyPrefix]
    private static bool Awake_ModifyInventoryBasedOnInventory(Player_Control_scr __instance, ref int ___health, ref float ___HandPivotScale)
    {
        if (!ArchipelagoClient.Authenticated)
        {
            return true;
        }

        __instance.ROT.y = __instance.transform.rotation.eulerAngles.y;
        __instance.BODY = __instance.GetComponent<Rigidbody>();
        __instance.BODY.collisionDetectionMode = CollisionDetectionMode.Discrete;
        __instance.BODY.freezeRotation = true;
        __instance.BODY.useGravity = false;
        __instance.BODY.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        __instance.HANDS.transform.parent = __instance.CONTROL.transform;
        Camera.main.fieldOfView = __instance.CONTROL.CurrentData.SETTING_FOV;
        __instance.Wep_Text[2].transform.GetChild(0).gameObject.GetComponent<Healthbar_scr>().CheckHealth(___health / 100f);
        __instance.StartCoroutine("Shake");
        __instance.HANDS.GetChild(0).GetChild(0).transform.Translate(Vector3.forward * ___HandPivotScale);
        foreach (Transform item in __instance.HANDS.GetChild(0).GetChild(0))
        {
            item.transform.Translate(Vector3.back * ___HandPivotScale);
        }
        
        var con = GameObject.Find("CONTROL").GetComponent<GAME_CONTROL>();
        if (con == null)
        {
            return false;
        }
        var isJeeb = con.JEEB;
        var weapons = con.PlayerWeapons;
        var ammo = con.PlayerAmmo;
        var newLoadout = new List<Weapon>();
        if (__instance.Current_Weapon.name != "NULL")
        {
            __instance.Current_Weapon = weapons[0];
        }
        if (!isJeeb)
        {
            foreach (var weapon in UktenaItems.JebWeapons)
            {
                if (!ArchipelagoClient.ServerData.Inventory.ContainsKey(weapon))
                {
                    continue;
                }
                var weaponObject = weapons[UktenaItems.WeaponToArraySpot[weapon]];
                newLoadout.Add(weaponObject);
            }
        }
        else
        {
            foreach (var weapon in UktenaItems.JeebWeapons)
            {
                if (!ArchipelagoClient.ServerData.Inventory.ContainsKey(weapon))
                {
                    continue;
                }
                var weaponObject = weapons[UktenaItems.WeaponToArraySpot[weapon]];
                newLoadout.Add(weaponObject);
            }
        }

        if (newLoadout.Count > 0)
        {
            __instance.Equipped =  newLoadout.ToArray();
            __instance.Equipped_Amount =  newLoadout.Count;
        }
        else
        {
            __instance.Equipped = [__instance.Current_Weapon];
            __instance.Equipped_Amount = 0;
        }
        
        var swapMethod = __instance.GetType().GetMethod("OnSwap", BindingFlags.Instance | BindingFlags.NonPublic);
        swapMethod.Invoke(__instance, [0]);
        if (Plugin.CurrentScene == "Demo_Cabin_1")
        {
            con.AdjustAmmo(1, 20);
        }
        
        foreach (var bullet in UktenaItems.GameAmmo)
        {
            if (!ArchipelagoClient.ServerData.Inventory.TryGetValue(bullet, out var count))
            {
                continue;
            }
            var total = UktenaItems.OneOnlyAmmo.Contains(bullet) ? 1 + count : 10 + 10 * count;
            con.AdjustAmmo(UktenaItems.AmmoToArraySpot[bullet], total);
            int wepText = con.CheckAmmo(con.PLAYER.Current_Weapon.Ammo_Type);
            con.PLAYER.Wep_Text[0].GetComponent<Animation>().Play();
            var setWepTextMethod = __instance.GetType().GetMethod("SetWepText", BindingFlags.Instance | BindingFlags.NonPublic);
            setWepTextMethod.Invoke(__instance, [wepText]);
        }
        
        var healCount = ArchipelagoClient.ServerData.Inventory.TryGetValue("Instant Coffee", out var invCount) ? invCount : 0;
        _instantCoffees = healCount;
        Plugin.Log.LogInfo($"We start with {_instantCoffees} instant coffees!");

        return false;
    }

    public static void SetupPhotosAndMeatOnSceneLoaded(GAME_CONTROL con, Player_Control_scr player)
    {
        if (ArchipelagoClient.ServerData.Photographer)
        {
            if (UktenaScenes.SceneToPhoto.TryGetValue(Plugin.CurrentScene, out var photo))
            {
                
                if (ArchipelagoClient.ServerData.Inventory.TryGetValue(photo, out var count))
                {
                    con.PICS_REMAINING = Math.Max(0, con.PICS_REMAINING - count);
                    var levelPics = (int)player.Sight.GetType().GetField("taken_pics", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(player.Sight);
                    levelPics += count;
                    player.Sight.GetType().GetField("taken_pics", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(player.Sight, levelPics);
                    if (con.PICS_REMAINING <= 0)
                    {
                        con.BUTTONS[2].SetActive(value: true);
                    }
                }
            }
        }

        if (ArchipelagoClient.ServerData.BBQChef)
        {
            if (UktenaScenes.SceneToMeat.TryGetValue(Plugin.CurrentScene, out var meat))
            {
                if (ArchipelagoClient.ServerData.Inventory.TryGetValue(meat, out var count))
                {
                    con.PICS_REMAINING = Math.Max(0, con.PICS_REMAINING - count);
                    var levelPics = (int)player.Sight.GetType().GetField("taken_pics", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(player.Sight);
                    levelPics += count;
                    player.Sight.GetType().GetField("taken_pics", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(player.Sight, levelPics);
                    if (con.PICS_REMAINING <= 0)
                    {
                        con.BUTTONS[2].SetActive(value: true);
                    }
                }
            }
        }
    }

    private static void GivePhotoOrMeatInScene(GAME_CONTROL con, Player_Control_scr player, string itemName)
    {
        if (ArchipelagoClient.ServerData.Photographer)
        {
            if (UktenaScenes.SceneToPhoto.TryGetValue(Plugin.CurrentScene, out var photo))
            {
                
                if (itemName == photo)
                {
                    con.PICS_REMAINING = Math.Max(0, con.PICS_REMAINING - 1);
                    var levelPics = (int)player.Sight.GetType().GetField("taken_pics", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(player.Sight);
                    levelPics += 1;
                    player.Sight.GetType().GetField("taken_pics", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(player.Sight, levelPics);
                    if (con.PICS_REMAINING <= 0)
                    {
                        con.BUTTONS[2].SetActive(value: true);
                    }
                    return;
                }
            }
        }

        if (!ArchipelagoClient.ServerData.BBQChef) return;
        if (!UktenaScenes.SceneToMeat.TryGetValue(Plugin.CurrentScene, out var meat)) return;
        if (itemName != meat) return;
        con.PICS_REMAINING = Math.Max(0, con.PICS_REMAINING - 1);
        var levelMeat = (int)player.Sight.GetType().GetField("taken_pics", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(player.Sight);
        levelMeat += 1;
        player.Sight.GetType().GetField("taken_pics", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(player.Sight, levelMeat);
        if (con.PICS_REMAINING <= 0)
        {
            con.BUTTONS[2].SetActive(value: true);
        }
    }

    public static void UpdateGameInventoryIfRelevant(string itemName, int currentAmount)
    {
        if (!UktenaScenes.Levels.Contains(Plugin.CurrentScene)) return;
        var con = GameObject.Find("CONTROL").GetComponent<GAME_CONTROL>();
        if (con == null)
        {
            return;
        }
        var isJeeb = con.JEEB;
        var weapons = con.PlayerWeapons;
        var player = con.PLAYER;
        if (player is null)
        {
            return;
        }
        if (UktenaItems.WeaponToArraySpot.Keys.ToList().Contains(itemName))
        {
            Plugin.Log.LogInfo($"We got a weapon: {itemName}");
            if (currentAmount > 1) return;
            if (isJeeb && UktenaItems.JebWeapons.Contains(itemName)) return;
            if (!isJeeb && UktenaItems.JeebWeapons.Contains(itemName)) return;
            Plugin.Log.LogInfo("Trying to give weapon");
            var currentWeapons = player.Equipped.Where(x => x is not null && x.name != "BINOC").ToList();
            var newWeapon = weapons[UktenaItems.WeaponToArraySpot[itemName]];
            currentWeapons.Add(newWeapon);
            player.Equipped = currentWeapons.ToArray();
            player.Equipped_Amount++;
            return;
        }

        if (UktenaItems.AmmoToArraySpot.Keys.ToList().Contains(itemName))
        {
            var subType = UktenaItems.AmmoToArraySpot[itemName];
            var isTypeOne = UktenaItems.OneOnlyAmmo.Contains(itemName);
            var amount = isTypeOne ? 1 : 10;
            con.AdjustAmmo(subType, amount);
            return;
        }
        if (itemName == "Instant Coffee")
        {
            if (player.health > 50)
            {
                _instantCoffees++;
                return;
            }
            player.health = Mathf.Min(100f, player.health + 50);
            player.Wep_Text[2].transform.GetChild(0).gameObject.GetComponent<Healthbar_scr>().CheckHealth(player.health / 100f);
            Object.Instantiate(Resources.Load("Effects/FLASH_HEALTH"), player.Wep_Text[0].transform.parent);
            ArchipelagoClient.ServerData.Inventory["Instant Coffee"] =
                Math.Max(0, ArchipelagoClient.ServerData.Inventory["Instant Coffee"] - 1);
            return;
        }
        GivePhotoOrMeatInScene(con, con.PLAYER, itemName);
    }

    [HarmonyPatch(typeof(Boss_control), "Over")]
    [HarmonyPostfix]
    private static void JebOver_VictoryOrItem(Boss_control __instance)
    {
        if (!ArchipelagoClient.Authenticated)
        {
            return;
        }

        if (ArchipelagoClient.ServerData.PlayableCampaign == Campaign.Jeb)
        {
            Plugin.ArchipelagoClient.Win();
        }

        if (ArchipelagoClient.ServerData.PlayableCampaign == Campaign.Both &&
            ArchipelagoClient.ServerData.Inventory.TryGetValue("Jeeb Level Complete", out var count))
        {
            if (count > 4) Plugin.ArchipelagoClient.Win();
            
        }
        Plugin.ArchipelagoClient.SendLocation(39);
    }
    
    [HarmonyPatch(typeof(Boss2Control), "Over")]
    [HarmonyPostfix]
    private static void JeebOver_VictoryOrItem(Boss_control __instance)
    {
        if (!ArchipelagoClient.Authenticated)
        {
            return;
        }

        if (ArchipelagoClient.ServerData.PlayableCampaign == Campaign.Jeeb)
        {
            Plugin.ArchipelagoClient.Win();
        }

        if (ArchipelagoClient.ServerData.PlayableCampaign == Campaign.Both &&
            ArchipelagoClient.ServerData.Inventory.ContainsKey("JebClear"))
        {
            Plugin.ArchipelagoClient.Win();
        }
        Plugin.ArchipelagoClient.SendLocation(198);
    }
    
    [HarmonyPatch(typeof(GAME_CONTROL), "Dead")]
    [HarmonyPostfix]
    private static void Dead_MaybeDeathlinkButAlsoAvoidCheckSent()
    {
        if (!ArchipelagoClient.Authenticated)
        {
            return;
        }
        Plugin.CurrentLevel = "Splash";
        Plugin.ArchipelagoClient.SendDeathLink();
    }

    private static void SayQuip(GAME_CONTROL con)
    {
        
    }
}