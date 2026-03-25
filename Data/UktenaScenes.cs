using System.Collections.Generic;

namespace Uktena64Randomizer.Data;

public static class UktenaScenes
{
    public static readonly List<string> Levels = new()
    {
        "Demo_Cabin_1", "Demo_Creek", "Demo_Forest_1", "Demo_Town", "Demo_Mountain",
        "Echo_BBQ", "ECHO_Road", "Echo_Lake", "Echo_Park", "ECHO_Mountain"
    };
    public static readonly List<string> JebLevels = new()
    {
        "Demo_Cabin_1", "Demo_Creek", "Demo_Forest_1", "Demo_Town", "Demo_Mountain"
    };
    
    public static readonly List<string> JeebLevels = new ()
    {
        "Echo_BBQ", "ECHO_Road", "Echo_Lake", "Echo_Park", "ECHO_Mountain"
    };

    public static readonly Dictionary<string, string> SceneToPhoto = new()
    {
        { "Demo_Cabin_1", "Jeb's Cabin Photo" },
        { "Demo_Creek", "Turkey Creek Photo" },
        { "Demo_Forest_1", "Frigid Valley Photo" },
        { "Demo_Town", "Howling Marsh Photo" },
        { "Demo_Mountain", "Bleeding Grove Photo" },
    };

    public static readonly Dictionary<string, string> SceneToMeat = new()
    {
        { "Echo_BBQ", "The BBQ Basket Meat" },
        { "ECHO_Road", "Ritual Road Meat" },
        { "Echo_Lake", "Lake Linger Meat" },
        { "Echo_Park", "Pallid Park Meat" },
        { "ECHO_Mountain", "Burning Grove Meat" }
    };

    public static readonly Dictionary<string, string> SceneToLevelName = new()
    {
        { "Demo_Cabin_1", "Jeb's Cabin" },
        { "Demo_Creek", "Turkey Creek" },
        { "Demo_Forest_1", "Frigid Valley" },
        { "Demo_Town", "Howling Marsh" },
        { "Demo_Mountain", "Bleeding Gorge" },
        { "Echo_BBQ", "Jeeb's BBQ" },
        { "ECHO_Road", "Ritual Road" },
        { "Echo_Lake", "Lake Linger" },
        { "Echo_Park", "Pallid Park" },
        { "ECHO_Mountain", "Burning Grove" }
    };
}