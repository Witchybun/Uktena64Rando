using System.Collections.Generic;

namespace Uktena64Randomizer.Data;

public static class UktenaItems
{
    public static readonly Dictionary<string, int> WeaponToArraySpot = new()
    {
        {"Binoculars", 0},
        {"Ruger", 1},
        {"Butcher Knives", 2},
        {"Bear Trap", 3},
        {"Crossbow", 4},
        {"Banjo", 5},
        {"Knife", 6},
        {"Rifle", 7},
        {"Camera", 8},
        {"Revolver", 9},
        {"Bren LMG", 10},
    };

    public static readonly List<string> JebWeapons = new()
    {
        "Knife", "Rifle", "Camera", "Revolver", "Bren LMG"
    };

    public static readonly List<string> JeebWeapons = new()
    {
        "Binoculars", "Ruger", "Butcher Knives", "Bear Trap", "Crossbow", "Banjo"
    };

    public static readonly List<string> GameAmmo = new()
    {
        "Starting Ruger Ammo +10", "Starting Rifle Ammo +10", "Starting Revolver Ammo +10", "Starting Bear Traps + 1",
        "Starting Crossbow Bolts + 1"
    };

    public static readonly Dictionary<string, int> AmmoToArraySpot = new()
    {
        { "Starting Ruger Ammo +10", 0},
        { "Starting Rifle Ammo +10", 1 },
        { "Starting Revolver Ammo +10", 2},
        { "Starting Bear Traps + 1", 3},
        { "Starting Crossbow Bolts + 1", 4},
    };

    public static readonly Dictionary<int, string> JebClickToLevel = new()
    {
        { 1, "Turkey Creek" },
        { 2, "Frigid Valley" },
        { 3, "Howling Marsh" },
    };
    
    public static readonly Dictionary<int, string> JeebClickToLevel = new()
    {
        { 1, "Ritual Road" },
        { 2, "Lake Linger" },
        { 3, "Pallid Park" },
    };

    public static readonly List<string> OneOnlyAmmo = new()
    {
        "Starting Bear Traps + 1", "Starting Crossbow Bolts + 1"
    };

    public static readonly List<string> Levels = new()
    {
        "Turkey Creek", "Frigid Valley", "Howling Marsh", "Bleeding Grove", "Ritual Road", "Lake Linger", "Pallid Park", "Burning Grove"

    };

    public static readonly List<string> Photos = new()
    {
        "Jeb's Cabin Photo", "Turkey Creek Photo", "Frigid Valley Photo", "Howling Marsh Photo", "Bleeding Grove Photo"
    };

    public static readonly List<string> Meat = new()
    {
        "The BBQ Basket Meat", "Ritual Road Meat", "Lake Linger Meat", "Pallid Park Meat", "Burning Grove Meat"
    };
}