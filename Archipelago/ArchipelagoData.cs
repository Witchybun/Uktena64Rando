using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Uktena64Randomizer.Data;

namespace Uktena64Randomizer.Archipelago;

public class ArchipelagoData
{
    public string Uri;
    public string SlotName;
    public string Password;
    public int Index;

    public readonly Dictionary<string, int> Inventory = new();
    public SortedDictionary<long, ArchipelagoItem> LocationTable = new();

    public const string CAMP_KEY = "campaign";
    public const string CAMERA_KNIFE_KEY = "randomize_camera_knife";
    public const string PHOTO_KEY = "photographer";
    public const string BBQ_KEY = "bbq_chef";
    public const string HYENA_KEY = "hyenas";
    public const string ROGUE_KEY = "rogue_scholar";
    public const string DEATH_KEY = "death_link";
    public const string SEED_KEY = "seed";

    public Campaign PlayableCampaign { get; private set; }
    public bool RandomizeCameraKnife { get; private set; }
    public bool Photographer { get; private set; }
    public bool BBQChef { get; private set; }
    public bool Hyenas { get; private set; }
    public bool RogueScholar { get; private set; }
    public bool DeathLink { get; private set; }
    public int Seed { get; private set; }

    public List<long> CheckedLocations;

    private Dictionary<string, object> _slotData;

    public bool NeedSlotData => _slotData == null;

    public ArchipelagoData()
    {
        Uri = "localhost";
        SlotName = "Player1";
        CheckedLocations = new();
    }

    public ArchipelagoData(string uri, string slotName, string password)
    {
        Uri = uri;
        SlotName = slotName;
        Password = password;
        CheckedLocations = new();
    }

    public void SetupSession(Dictionary<string, object> roomSlotData, string roomSeed)
    {
        Plugin.Log.LogInfo("Starting Slot Data Reading.  If you do not see \"TRANS RIGHTS\" in a new line after this, " +
                           "something went wrong.  Report it.");
        _slotData = roomSlotData;
        Seed = GetSlotSetting(SEED_KEY, 0);
        PlayableCampaign = GetSlotSetting(CAMP_KEY, Campaign.Jeb);
        RandomizeCameraKnife = GetSlotSetting(CAMERA_KNIFE_KEY, false);
        Photographer = GetSlotSetting(PHOTO_KEY, false);
        BBQChef = GetSlotSetting(BBQ_KEY, false);
        Hyenas = GetSlotSetting(HYENA_KEY, false);
        RogueScholar = GetSlotSetting(ROGUE_KEY, false);
        DeathLink = GetSlotSetting(DEATH_KEY, false);
        Plugin.Log.LogInfo($"TRANS RIGHTS");
    }

    public Campaign GetSlotSetting(string key, Campaign defaultValue)
    {
        return (Campaign)(_slotData.TryGetValue(key, out var field) ? Enum.Parse(typeof(Campaign), field.ToString()) : GetSlotDefaultValue(key, defaultValue));
    }
    
    public int GetSlotSetting(string key, int defaultValue)
    {
        return _slotData.TryGetValue(key, out var field) ? (int)(long)field : GetSlotDefaultValue(key, defaultValue);
    }
    
    public bool GetSlotSetting(string key, bool defaultValue)
    {
        if (_slotData.ContainsKey(key) && _slotData[key] != null && _slotData[key] is bool boolValue)
        {
            return boolValue;
        }
        if (_slotData[key] is string strValue && bool.TryParse(strValue, out var parsedValue))
        {
            return parsedValue;
        }
        if (_slotData[key] is int intValue)
        {
            return intValue != 0;
        }
        if (_slotData[key] is long longValue)
        {
            return longValue != 0;
        }
        if (_slotData[key] is short shortValue)
        {
            return shortValue != 0;
        }

        return GetSlotDefaultValue(key, defaultValue);
    }

    private T GetSlotDefaultValue<T>(string key, T defaultValue)
    {
        Plugin.Log.LogWarning($"SlotData did not contain expected key: \"{key}\"");
        return defaultValue;
    }
}

public enum Campaign
{
    Jeb = 0,
    Jeeb = 1,
    Both = 2
}