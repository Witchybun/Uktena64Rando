using System.Collections.Generic;
using Uktena64Randomizer.Archipelago;
using UnityEngine;

namespace Uktena64Randomizer.Data;

public static class UktenaLocations
{
    public class UktenaLocation
    {
        public readonly int ID;
        public readonly string Name;
        public readonly string Scene;
        public readonly string ObjectName;
        public readonly Vector3 Position;
        
        public UktenaLocation(int id, string name, string scene, string objectName, Vector3 position)
        {
            ID = id;
            Name = name;
            Scene = scene;
            ObjectName = objectName;
            Position = position;
        }

        public bool IsLocationInformationTheSame(string scene, string objectName, Vector3 position)
        {
            if (scene != Scene)
            {
                return false;
            }

            if (objectName != ObjectName)
            {
                return false;
            }

            return Vector3.Distance(Position, position) < 0.5f;
        }

        public void SendLocation()
        {
            Plugin.ArchipelagoClient.SendLocation(ID);
        }
    }

    private const string JEB = "Jeb";
    private const string JEEB = "Jeeb";
    private const string JEB_LORE = "Jeb Lore";
    private const string JEEB_LORE = "Jeeb Lore";
    private const string PHOTO = "Photo";
    private const string MEAT = "Meat";
    private const string JEB_HYENA = "Jeb Hyena";
    private const string JEEB_HYENA = "Jeeb Hyena";
    
    public static Dictionary<string, UktenaLocation> NameToLocations = new();
    public static List<UktenaLocation> BaseJebLocations = new();
    public static List<UktenaLocation> BaseJeebLocations = new();
    public static List<UktenaLocation> BaseJebLoreLocations = new();
    public static List<UktenaLocation> BaseJeebLoreLocations = new();
    public static List<UktenaLocation> BasePhotoLocations = new();
    public static List<UktenaLocation> BaseMeatLocations = new();
    public static List<UktenaLocation> BaseJebHyenaLocations = new();
    public static List<UktenaLocation> BaseJeebHyenaLocations = new();
    public static Dictionary<string, int> ClearLookup = new();

    public static readonly List<UktenaLocation> RandoLocationData = new()
    {
        // Base Jeb Locations
        CreateLocation(1, "Jeb's Cabin: Rocking Chair Item", "Demo_Cabin_1", "Gear", new Vector3(443.353f, 9.586f, 353.818f), JEB),
        CreateLocation(2, "Jeb's Cabin: Camera Barrel", "Demo_Cabin_1", "Gear2", new Vector3(442.338f, 8.3442f, 342.317f), JEB),
        CreateLocation(3, "Jeb's Cabin: Rifle Near Rocking Chair", "Demo_Cabin_1", "Gear", new Vector3(443.353f, 9.586f, 353.818f), JEB, true), // Ignore it otherwise, send both at the same time.
        CreateLocation(4, "Jeb's Cabin: Coffee", "Demo_Cabin_1", "Coffee", new Vector3(417.621f, 8.759f, 345.923f), JEB),
        CreateLocation(5, "Jeb's Cabin: Ammo", "Demo_Cabin_1", "Ammo", new Vector3(455.592f, 9.411f, 378.173f), JEB),
        CreateLocation(6, "Jeb's Cabin Cleared", "Demo_Cabin_1", "Jeb's Cabin", Vector3.zero, JEB),
        
        CreateLocation(7, "Turkey Creek: Turkey Call Item", "Demo_Creek", "Call", new Vector3(298.638f, 9.734f, 415.221f), JEB),
        CreateLocation(8, "Turkey Creek: Hen House Barrel Ammo", "Demo_Creek", "Ammo", new Vector3(345.2535f, 9.1167f, 414.5991f), JEB),
        CreateLocation(9, "Turkey Creek: Hen House Coffee", "Demo_Creek", "Coffee", new Vector3(346.8636f, 9.813f, 409.2795f), JEB),
        CreateLocation(10, "Turkey Creek: House Barrel Ammo", "Demo_Creek", "Ammo", new Vector3(462.16f, 8.2704f, 390.253f), JEB),
        CreateLocation(11, "Turkey Creek: Dark Spot Barrel Ammo", "Demo_Creek", "Ammo", new Vector3(355.687f, 9.603f, 306.0448f), JEB),
        CreateLocation(12, "Turkey Creek: House Barrel Coffee", "Demo_Creek", "Coffee", new Vector3(449.32f, 8.2634f, 400.46f), JEB),
        CreateLocation(13, "Turkey Creek: Rock Coffee", "Demo_Creek", "Coffee", new Vector3(409.368f, 9.449f, 307.476f), JEB),
        CreateLocation(14, "Turkey Creek Cleared", "Demo_Creek", "Turkey Creek", Vector3.zero, JEB),
        
        CreateLocation(15, "Frigid Valley: Barrel Ammo", "Demo_Forest_1", "Ammo", new Vector3(427.864f, 7.996f, 350.706f), JEB),
        CreateLocation(16, "Frigid Valley: Broken Truck Ammo", "Demo_Forest_1", "Ammo", new Vector3(380.617f, 7.424f, 364.31f), JEB),
        CreateLocation(17, "Frigid Valley: Early Path Barrel Ammo", "Demo_Forest_1", "Ammo", new Vector3(286.442f, 7.252f, 432.606f), JEB),
        CreateLocation(18, "Frigid Valley: Second Dead-end Barrel Ammo", "Demo_Forest_1", "Ammo", new Vector3(103.368f, 8.2524f, 380.025f), JEB),
        CreateLocation(19, "Frigid Valley: Trailer Coffee", "Demo_Forest_1", "Coffee", new Vector3(67.608f, 8.805f, 238.954f), JEB),
        CreateLocation(20, "Frigid Valley: Trailer Ammo", "Demo_Forest_1", "Ammo", new Vector3(69.769f, 8.702f, 235.7f), JEB),
        CreateLocation(21, "Frigid Valley: Trailer Barrel Ammo", "Demo_Forest_1", "Ammo", new Vector3(311.156f, 7.353f, 166.328f), JEB),
        CreateLocation(22, "Frigid Valley: Trailer Coffee Near TV", "Demo_Forest_1", "Coffee", new Vector3(316.039f, 7.683f, 168.268f), JEB),
        CreateLocation(23, "Frigid Valley: Final Shack Barrel Coffee", "Demo_Forest_1", "Coffee", new Vector3(440.882f, 7.8464f, 147.6769f), JEB),
        CreateLocation(24, "Frigid Valley: Final Shack Barrel Ammo", "Demo_Forest_1", "Ammo", new Vector3(442.653f, 7.3794f, 143.087f), JEB),
        CreateLocation(25, "Frigid Valley: Late Path Barrel Ammo", "Demo_Forest_1", "Ammo", new Vector3(174.53f, 8.394f, 191.17f), JEB),
        CreateLocation(26, "Frigid Valley Cleared", "Demo_Forest_1", "Frigid Valley", Vector3.zero, JEB),
        
        CreateLocation(27, "Howling Marsh: Church Podium Item", "Demo_Town", "Gear", new Vector3(233.467f, 11.013f, 256.03f), JEB),
        CreateLocation(28, "Howling Marsh: House Coffee", "Demo_Town", "Coffee", new Vector3(275.998f, 10.337f, 255.555f), JEB),
        CreateLocation(29, "Howling Marsh: House Upstairs Ammo", "Demo_Town", "Ammo_Mag", new Vector3(284.121f, 13.060f, 260.501f), JEB),
        CreateLocation(30, "Howling Marsh: Fenced House Barrel Ammo 1", "Demo_Town", "Ammo", new Vector3(303.083f, 8.4734f, 274.602f), JEB),
        CreateLocation(31, "Howling Marsh: Fenced House Barrel Ammo 2", "Demo_Town", "Ammo", new Vector3(304.77f, 8.609f, 270.528f), JEB),
        CreateLocation(32, "Howling Marsh: Graveyard Key Item", "Demo_Town", "Church Key Picked up", new Vector3(367.124f, 32.676f, 124.35f), JEB),
        CreateLocation(33, "Howling Marsh: Graveyard Barrel Ammo", "Demo_Town", "Ammo_Mag", new Vector3(383.076f, 32.909f, 71.453f), JEB),
        CreateLocation(34, "Howling Marsh: Greenhouse Barrel Ammo 1", "Demo_Town", "Ammo", new Vector3(154.32f, 39.675f, 93.55f), JEB),
        CreateLocation(35, "Howling Marsh: Greenhouse Barrel Ammo 2", "Demo_Town", "Ammo_Mag", new Vector3(152.835f, 39.652f, 93.690f), JEB),
        CreateLocation(36, "Howling Marsh: Trailer Coffee", "Demo_Town", "Coffee", new Vector3(168.251f, 40.544f, 72.979f), JEB),
        CreateLocation(37, "Howling Marsh Cleared", "Demo_Town", "Howling Marsh", Vector3.zero, JEB),
        
        CreateLocation(38, "Bleeding Grove: Starved Corpse's Weapon", "Demo_Mountain", "BREN", new Vector3(398.587f, 7.651f, 243.584f), JEB),
        CreateLocation(39, "Bleeding Grove Cleared", "Demo_Mountain", "Bleeding Grove", Vector3.zero, JEB),
        
        // Jeb Lore
        CreateLocation(51, "Jeb's Cabin: Controls Manual", "Demo_Cabin_1", "Book_Myth0", new Vector3(419.332f, 8.857f, 338.485f), JEB_LORE),
        CreateLocation(52, "Jeb's Cabin: Totally Real CDC Notice", "Demo_Cabin_1", "Note0", new Vector3(403.061f, 9.379f, 349.017f), JEB_LORE),
        CreateLocation(53, "Turkey Creek: Hero of the Horned Snake", "Demo_Creek", "Book_Myth0", new Vector3(280.29f, 9.451f, 425.629f), JEB_LORE),
        CreateLocation(54, "Turkey Creek: They Ate All The Fish! Note", "Demo_Creek", "Note0", new Vector3(357.468f, 10.337f, 313.396f), JEB_LORE),
        CreateLocation(55, "Turkey Creek: Note on Starved Chickens", "Demo_Creek", "Note1", new Vector3(339.328f, 9.675f, 409.555f), JEB_LORE),
        CreateLocation(56, "Turkey Creek: Newfound God Gibberish", "Demo_Creek", "Note3", new Vector3(411.462f, 10.071f, 310.392f), JEB_LORE),
        CreateLocation(57, "Turkey Creek: Dead Man's Realization on Turkeys", "Demo_Creek", "Note2", new Vector3(451.826f, 8.002f, 394.562f), JEB_LORE),
        CreateLocation(58, "Frigid Valley: The Uktena Part 1", "Demo_Forest_1", "Book_Myth0", new Vector3(424.766f, 8.684f, 344.935f), JEB_LORE),
        CreateLocation(59, "Frigid Valley: The Uktena Part 2", "Demo_Forest_1", "Book_Myth1", new Vector3(67.98f, 8.756f, 237.054f), JEB_LORE),
        CreateLocation(60, "Frigid Valley: Note on How Loud The Sound Is", "Demo_Forest_1", "Note2", new Vector3(431.883f, 8.237f, 127.668f), JEB_LORE),
        CreateLocation(61, "Frigid Valley: Ringing Sound Note", "Demo_Forest_1", "Note1", new Vector3(436.307f, 8.498f, 143.731f), JEB_LORE),
        CreateLocation(62, "Frigid Valley: Nightmare Note", "Demo_Forest_1", "Note0", new Vector3(442.088f, 9.043f, 145.67f), JEB_LORE),
        CreateLocation(63, "Howling Marsh: The End Has Come Note", "Demo_Town", "Note0", new Vector3(237.465f, 10.117f, 260.459f), JEB_LORE),
        CreateLocation(64, "Howling Marsh: Diary Day 1", "Demo_Town", "Note1", new Vector3(170.896f, 40.348f, 70.013f), JEB_LORE),
        CreateLocation(65, "Howling Marsh: Diary Day 5", "Demo_Town", "Note2", new Vector3(166.73f, 40.319f, 106.23f), JEB_LORE),
        
        CreateLocation(66, "Bleeding Grove: More Than One Note", "Demo_Mountain", "Note0", new Vector3(202.985f, 9.047f, 409.185f), JEB_LORE),
        CreateLocation(67, "Bleeding Grove: The Uktena Part 3", "Demo_Mountain", "Book_Myth0", new Vector3(288.235f, 10.173f, 196.601f), JEB_LORE),
        CreateLocation(68, "Bleeding Grove: The Hunger Note", "Demo_Mountain", "Note1", new Vector3(290.172f, 9.234f, 190.489f), JEB_LORE),
        CreateLocation(69, "Bleeding Grove: No Food Left Note", "Demo_Mountain", "Note2", new Vector3(395f, 7.958f, 241.791f), JEB_LORE),
        
        // Photos
        CreateLocation(81, "Jeb's Cabin: Squirrel Photo", "Demo_Cabin_1", "Squirrel", Vector3.zero, PHOTO),
        
        CreateLocation(82, "Turkey Creek: Hen House Turkey Photo", "Demo_Creek", "TURK_ROOT_12", Vector3.zero, PHOTO),
        CreateLocation(83, "Turkey Creek: Dark Spot Linked Turkey 1 Photo", "Demo_Creek", "TURK_ROOT_4", Vector3.zero, PHOTO),
        CreateLocation(84, "Turkey Creek: Dark Spot Linked Turkey 2 Photo", "Demo_Creek", "TURK_ROOT_8", Vector3.zero, PHOTO),
        CreateLocation(85, "Turkey Creek: Dark Spot Linked Turkey 3 Photo", "Demo_Creek", "TURK_ROOT_7", Vector3.zero, PHOTO),
        CreateLocation(86, "Turkey Creek: Rock Linked Turkey 1 Photo", "Demo_Creek", "TURK_ROOT_3", Vector3.zero, PHOTO),
        CreateLocation(87, "Turkey Creek: Rock Linked Turkey 2 Photo", "Demo_Creek", "TURK_ROOT_9", Vector3.zero, PHOTO),
        CreateLocation(88, "Turkey Creek: Rock Linked Turkey 3 Photo", "Demo_Creek", "TURK_ROOT_6", Vector3.zero, PHOTO),
        CreateLocation(89, "Turkey Creek: Decomposing Body", "Demo_Creek", "Skull 1", Vector3.zero, PHOTO),
        CreateLocation(90, "Turkey Creek: House Linked Turkey 1 Photo", "Demo_Creek", "TURK_ROOT_2", Vector3.zero, PHOTO),
        CreateLocation(91, "Turkey Creek: House Linked Turkey 2 Photo", "Demo_Creek", "TURK_ROOT_5", Vector3.zero, PHOTO),
        CreateLocation(92, "Turkey Creek: House Linked Turkey 3 Photo", "Demo_Creek", "TURK_ROOT_10", Vector3.zero, PHOTO),
        CreateLocation(93, "Turkey Creek: Barrel Turkey", "Demo_Creek", "TURK_ROOT_11", Vector3.zero, PHOTO),
        CreateLocation(94, "Turkey Creek: Headless Turkey", "Demo_Creek", "TURK_ROOT_1", Vector3.zero, PHOTO),
        
        CreateLocation(95, "Frigid Valley: Early Dead-end Wolf 1 Photo", "Demo_Forest_1", "ROOT1_16", Vector3.zero, PHOTO),
        CreateLocation(96, "Frigid Valley: Early Dead-end Wolf 2 Photo", "Demo_Forest_1", "ROOT1_9", Vector3.zero, PHOTO),
        CreateLocation(97, "Frigid Valley: Early Dead-end Wolf 3 Photo", "Demo_Forest_1", "ROOT1_11", Vector3.zero, PHOTO),
        CreateLocation(98, "Frigid Valley: Second Dead-end Wolf 1 Photo", "Demo_Forest_1", "ROOT1_10", Vector3.zero, PHOTO),
        CreateLocation(99, "Frigid Valley: Second Dead-end Wolf 2 Photo", "Demo_Forest_1", "ROOT1_12", Vector3.zero, PHOTO),
        CreateLocation(100, "Frigid Valley: Second Dead-end Wolf 3 Photo", "Demo_Forest_1", "ROOT1_14", Vector3.zero, PHOTO),
        CreateLocation(101, "Frigid Valley: Second Dead-end Wolf 4 Photo", "Demo_Forest_1", "ROOT1_4", Vector3.zero, PHOTO),
        CreateLocation(102, "Frigid Valley: Open Area Wolf 1 Photo", "Demo_Forest_1", "ROOT1_15", Vector3.zero, PHOTO),
        CreateLocation(103, "Frigid Valley: Open Area Wolf 2 Photo", "Demo_Forest_1", "ROOT1_13", Vector3.zero, PHOTO),
        CreateLocation(104, "Frigid Valley: Rock Circle Meat Photo", "Demo_Forest_1", "HEART", new Vector3(96.15f, 8.519f, 94.16f), PHOTO),
        CreateLocation(105, "Frigid Valley: Trailer Skinned Dog Photo", "Demo_Forest_1", "ROOT1", new Vector3(229.9618f, 9.599f, 70.088f), PHOTO),
        CreateLocation(106, "Frigid Valley: Skinned Dog in Tree 1 Photo", "Demo_Forest_1", "ROOT1", new Vector3(278.695f, 14.741f, 106.966f), PHOTO),
        CreateLocation(107, "Frigid Valley: Skinned Dog in Tree 2 Photo", "Demo_Forest_1", "ROOT1", new Vector3(397.008f, 13.858f, 128.483f), PHOTO),
        CreateLocation(108, "Frigid Valley: Huge Elk Photo", "Demo_Forest_1", "ROOT2_3", Vector3.zero, PHOTO),
        CreateLocation(109, "Frigid Valley: Skinned Return Dog 1 Photo", "Demo_Forest_1", "ROOT1_7", Vector3.zero, PHOTO),
        CreateLocation(110, "Frigid Valley: Skinned Return Dog 2 Photo", "Demo_Forest_1", "ROOT1_6", Vector3.zero, PHOTO),
        CreateLocation(111, "Frigid Valley: Skinned Return Dog 3 Photo", "Demo_Forest_1", "ROOT1_8", Vector3.zero, PHOTO),
        CreateLocation(112, "Frigid Valley: Skinned Return Dog 4 Photo", "Demo_Forest_1", "ROOT1_5", Vector3.zero, PHOTO),
        CreateLocation(113, "Frigid Valley: Skinned Return Dog 5 Photo", "Demo_Forest_1", "ROOT1_1", Vector3.zero, PHOTO),
        CreateLocation(114, "Frigid Valley: Skinned Return Dog 6 Photo", "Demo_Forest_1", "ROOT1_2", Vector3.zero, PHOTO),
        CreateLocation(115, "Frigid Valley: Skinned Return Dog 7 Photo", "Demo_Forest_1", "ROOT1_18", Vector3.zero, PHOTO),
        CreateLocation(116, "Frigid Valley: Snowman Skull Photo", "Demo_Forest_1", "Skull(Clone)", new Vector3(355.01f, 8.404f, 438.83f), PHOTO),
        //CreateLocation(117, "Frigid Valley: Wolf Watching Over Valley Photo", "Demo_Forest_1", "ROOT1_17", new Vector3(155.478f, 8.379f, 75.584f), PHOTO),
        
        CreateLocation(118, "Howling Marsh: Bear 1 Photo", "Demo_Town", "BEAR_ROOT_6", Vector3.zero, PHOTO),
        CreateLocation(119, "Howling Marsh: Bear 2 Photo", "Demo_Town", "BEAR_ROOT_2", Vector3.zero, PHOTO),
        CreateLocation(120, "Howling Marsh: Bear 3 Photo", "Demo_Town", "BEAR_ROOT_4", Vector3.zero, PHOTO),
        CreateLocation(121, "Howling Marsh: Bear 4 Photo", "Demo_Town", "BEAR_ROOT_7", Vector3.zero, PHOTO),
        CreateLocation(122, "Howling Marsh: Burned Corpses 1 Photo", "Demo_Town", "BODY1", Vector3.zero, PHOTO),
        CreateLocation(123, "Howling Marsh: Burned Corpses 2 Photo", "Demo_Town", "BODY2", Vector3.zero, PHOTO),
        CreateLocation(124, "Howling Marsh: Burned Corpses 3 Photo", "Demo_Town", "BODY3", Vector3.zero, PHOTO),
        CreateLocation(125, "Howling Marsh: Burned Corpses 4 Photo", "Demo_Town", "BODY4", Vector3.zero, PHOTO),
        CreateLocation(126, "Howling Marsh: Burned Corpses 5 Photo", "Demo_Town", "BODY5", Vector3.zero, PHOTO),
        CreateLocation(127, "Howling Marsh: Burned Corpses 6 Photo", "Demo_Town", "BODY6", Vector3.zero, PHOTO),
        CreateLocation(128, "Howling Marsh: Huge Bear Photo", "Demo_Town", "BEAR_ROOT_3", Vector3.zero, PHOTO),
        
        CreateLocation(129, "Bleeding Grove: Starved Man Photo", "Demo_Mountain", "Corpse_PL", new Vector3(398.64f, 7.89f, 243.235f), PHOTO),
        
        
        // Base Jeeb Locations
        CreateLocation(151, "The BBQ Basket: Cooking Utensils", "Echo_BBQ", "Gear", new Vector3(462.967f, 14.544f, 401.788f), JEEB),
        CreateLocation(152, "The BBQ Basket: Binoculars in Barrel", "Echo_BBQ", "Gear2", new Vector3(465.687f, 13.236f, 411.092f), JEEB),
        CreateLocation(153, "The BBQ Basket: Ruger in Barrel", "Echo_BBQ", "Gear2", new Vector3(465.687f, 13.236f, 411.092f), JEEB, true),
        CreateLocation(154, "The BBQ Basket: Coffee", "Echo_BBQ", "Coffee", new Vector3(463.45f, 15.13f, 393.715f), JEEB),
        CreateLocation(155, "The BBQ Basket Cleared", "Echo_BBQ", "Jeeb's BBQ", Vector3.zero, JEEB),
        
        CreateLocation(156, "Ritual Road: Early Bridge Barrel Ammo", "ECHO_Road", "Ammo", new Vector3(180.11f, 14.542f, 177.74f), JEEB),
        CreateLocation(157, "Ritual Road: Outside Dollar Venereal Barrel Ammo", "ECHO_Road", "Ammo", new Vector3(141.583f, 17.736f, 48.973f), JEEB),
        CreateLocation(158, "Ritual Road: Dollar Venereal Coffee", "ECHO_Road", "Coffee", new Vector3(125.202f, 17.883f, 51.253f), JEEB),
        CreateLocation(159, "Ritual Road: Dollar Venereal Ammo", "ECHO_Road", "Ammo", new Vector3(138.34f, 18.019f, 48.790f), JEEB),
        CreateLocation(160, "Ritual Road: Truck Barrel Ammo", "ECHO_Road", "Ammo", new Vector3(80.832f, 16.672f, 148.335f), JEEB),
        CreateLocation(161, "Ritual Road: Tent Coffee", "ECHO_Road", "Coffee", new Vector3(68.968f, 13.654f, 202.408f), JEEB),
        CreateLocation(162, "Ritual Road Cleared", "ECHO_Road", "Ritual Road", Vector3.zero, JEEB),
        
        CreateLocation(163, "Lake Linger: Starting House Barrel Ammo", "Echo_Lake", "Ammo", new Vector3(345.651f, 13.766f, 114.894f), JEEB),
        CreateLocation(164, "Lake Linger: Boat House Fridge Coffee", "Echo_Lake", "Coffee", new Vector3(465.346f, 14.136f, 296.988f), JEEB),
        CreateLocation(165, "Lake Linger: Island Barrel Coffee", "Echo_Lake", "Coffee", new Vector3(316.31f, 14.524f, 256.24f), JEEB),
        CreateLocation(166, "Lake Linger: Island Barrel Ammo", "Echo_Lake", "Ammo", new Vector3(317.57f, 14.527f, 255.48f), JEEB),
        CreateLocation(167, "Lake Linger: Northern Dead End Barrel Ammo", "Echo_Lake", "Ammo", new Vector3(473.528f, 17.117f, 439.343f), JEEB),
        CreateLocation(168, "Lake Linger: Two Story Building Barrel Coffee", "Echo_Lake", "Coffee", new Vector3(419.739f, 18.106f, 461.381f), JEEB),
        CreateLocation(169, "Lake Linger: Main Office Barrel Ammo", "Echo_Lake", "Ammo", new Vector3(34.27f, 17.594f, 452.617f), JEEB),
        CreateLocation(170, "Lake Linger: Main Office Bear Trap Pack", "Echo_Lake", "click", new Vector3(34.77f, 16.6143f, 444.95f), JEEB),
        CreateLocation(171, "Lake Linger: North House Set Bear Trap", "Echo_Lake", "click", new Vector3(34.389f, 17.545f, 284.075f), JEEB),
        CreateLocation(172, "Lake Linger: Coffee Trap Set Bear Trap 1", "Echo_Lake", "click", new Vector3(31.708f, 14.948f, 141.729f), JEEB),
        CreateLocation(173, "Lake Linger: Coffee Trap Set Bear Trap 2", "Echo_Lake", "click", new Vector3(30.22f, 14.795f, 139.19f), JEEB),
        CreateLocation(174, "Lake Linger: Coffee Trap Set Bear Trap 3", "Echo_Lake", "click", new Vector3(27.49f, 15.27f, 137.25f), JEEB),
        CreateLocation(175, "Lake Linger: Coffee Trap Coffee", "Echo_Lake", "Coffee (1)", new Vector3(26.584f, 16.507f, 142.671f), JEEB),
        CreateLocation(176, "Lake Linger: Dam Barrel Ammo 1", "Echo_Lake", "Ammo", new Vector3(53.32f, 18.6864f, 55.77f), JEEB),
        CreateLocation(177, "Lake Linger: Dam Barrel Ammo 2", "Echo_Lake", "Ammo", new Vector3(51.75f, 18.686f, 55.76f), JEEB),
        CreateLocation(178, "Lake Linger: Trapped Start Trap 1", "Echo_Lake", "click", new Vector3(358.34f, 13.54f, 99.93f), JEEB),
        CreateLocation(179, "Lake Linger: Trapped Start Trap 2", "Echo_Lake", "click", new Vector3(361.87f, 13.625f, 107.11f), JEEB),
        CreateLocation(180, "Lake Linger: Trapped Start Trap 3", "Echo_Lake", "click", new Vector3(366.96f, 13.625f, 114.9f), JEEB),
        CreateLocation(181, "Lake Linger: Trapped Start Trap 4", "Echo_Lake", "click", new Vector3(377.14f, 14.036f, 116.45f), JEEB),
        CreateLocation(182, "Lake Linger: Trapped Start Trap 5", "Echo_Lake", "click", new Vector3(385.88f, 14.164f, 112f), JEEB),
        CreateLocation(183, "Lake Linger: Trapped Start Trap 6", "Echo_Lake", "click", new Vector3(396.4f, 14.67f, 100.3f), JEEB),
        CreateLocation(184, "Lake Linger: Trapped Start Trap 7", "Echo_Lake", "click", new Vector3(346.57f, 19.328f, 76.27f), JEEB),
        CreateLocation(185, "Lake Linger Cleared", "Echo_Lake", "Lake Linger", Vector3.zero, JEEB),
        
        CreateLocation(186, "Pallid Park: Office Chamber Barrel Ammo", "Echo_Park", "Ammo", new Vector3(153.5f, 17.1414f, 133.71f), JEEB),
        CreateLocation(187, "Pallid Park: Dead Owner Bench Ammo", "Echo_Park", "Ammo", new Vector3(96.482f, 16.144f, 291.96f), JEEB),
        CreateLocation(188, "Pallid Park: Swamp Bridge Barrel Ammo", "Echo_Park", "Ammo", new Vector3(313.553f, 13.199f, 467.837f), JEEB),
        CreateLocation(189, "Pallid Park: Crossbow Park Bench Item", "Echo_Park", "Gear", new Vector3(460.89f, 16.479f, 413.718f), JEEB),
        CreateLocation(190, "Pallid Park: North Coffee Barrel Ammo", "Echo_Park", "Coffee", new Vector3(461.068f, 15.169f, 401.279f), JEEB),
        CreateLocation(191, "Pallid Park: Swamp Shack Barrel Ammo", "Echo_Park", "Ammo", new Vector3(359.3f, 17.030f, 227.44f), JEEB),
        CreateLocation(192, "Pallid Park: Swamp Shack Bolts", "Echo_Park", "Bolts", new Vector3(340.174f, 17.656f, 231.031f), JEEB),
        CreateLocation(193, "Pallid Park: Swamp Shack Barrel Coffee", "Echo_Park", "Coffee", new Vector3(335.685f, 17.124f, 227.037f), JEEB),
        CreateLocation(194, "Pallid Park: South Office Coffee", "Echo_Park", "Coffee (1)", new Vector3(426.667f, 16.183f, 94.096f), JEEB),
        CreateLocation(195, "Pallid Park: South Office Ammo", "Echo_Park", "Ammo (1)", new Vector3(418.003f, 9.125f, 77.652f), JEEB),
        CreateLocation(196, "Pallid Park Cleared", "Echo_Park", "Pallid Park", Vector3.zero, JEEB),
        
        // Find a good way of sending this when the fight starts.
        CreateLocation(197, "Burning Grove: Jeeb's Banjo", "ECHO_Mountain", "", new Vector3(), JEEB),
        CreateLocation(198, "Burning Grove Cleared", "ECHO_Mountain", "Burning Grove", Vector3.zero, JEEB),
        
        // Jeeb Lore
        CreateLocation(201, "The BBQ Basket: Jeeb's Diary", "Echo_BBQ", "Book_Note0", new Vector3(460.237f, 14.14f, 389.156f), JEEB_LORE),
        CreateLocation(202, "The BBQ Basket: The Totally Real CIA Message", "Echo_BBQ", "Note1", new Vector3(457.881f, 14.946f, 383.216f), JEEB_LORE),
        CreateLocation(203, "The BBQ Basket: Praise the Tree Note", "Echo_BBQ", "Note0", new Vector3(301.287f, 13.067f, 401.704f), JEEB_LORE),
        CreateLocation(204, "Ritual Road: Nothing Satisfies Note", "ECHO_Road", "Note", new Vector3(201.6f, 17.656f, 125.8f), JEEB_LORE),
        CreateLocation(205, "Ritual Road: Black Spines Note", "ECHO_Road", "Note", new Vector3(54.7f, 19.361f, 101.266f), JEEB_LORE),
        CreateLocation(206, "Ritual Road: Taste That Flesh Note", "ECHO_Road", "Note", new Vector3(120.314f, 17.762f, 51.525f), JEEB_LORE),
        CreateLocation(207, "Ritual Road: Elation Note", "ECHO_Road", "Note", new Vector3(124.05f, 3.872f, 185.22f), JEEB_LORE),
        CreateLocation(208, "Lake Linger: Boathouse Diary", "Echo_Lake", "Book_Note0", new Vector3(473.303f, 14.515f, 293.087f), JEEB_LORE),
        CreateLocation(209, "Lake Linger: The Deluge", "Echo_Lake", "Book_Myth0", new Vector3(314.021f, 14.371f, 265.516f), JEEB_LORE),
        CreateLocation(210, "Lake Linger: Almighty Bayagototh Note", "Echo_Lake", "Note1", new Vector3(25.377f, 17.311f, 272.232f), JEEB_LORE),
        CreateLocation(211, "Lake Linger: Otter Warning Note", "Echo_Lake", "Note0", new Vector3(212.651f, 14.194f, 257.826f), JEEB_LORE),
        CreateLocation(212, "Pallid Park: Juiced Baby Note", "Echo_Park", "Note2", new Vector3(238.784f, 16.205f, 158.942f), JEEB_LORE),
        CreateLocation(213, "Pallid Park: Too Woke Note", "Echo_Park", "Note0", new Vector3(132.889f, 16.316f, 400.19f), JEEB_LORE),
        CreateLocation(214, "Pallid Park: The Great Leech of Tlanusi'yi", "Echo_Park", "Book_Myth0", new Vector3(459.893f, 16.342f, 395.557f), JEEB_LORE),
        CreateLocation(215, "Pallid Park: Right to be Naked Note", "Echo_Park", "Note3", new Vector3(344.105f, 17.815f, 227.244f), JEEB_LORE),
        CreateLocation(216, "Burning Grove: What Magic Note", "ECHO_Mountain", "Note", new Vector3(324.87f, 7.63f, 229.89f), JEEB_LORE),
        CreateLocation(217, "Burning Grove: Partook Flesh Note", "ECHO_Mountain", "Note", new Vector3(85.8f, 8.013f, 234.97f), JEEB_LORE),
        CreateLocation(218, "Burning Grove: Feel The Vines Note", "ECHO_Mountain", "", new Vector3(165.87f, 9.186f, 75.196f), JEEB_LORE),
        CreateLocation(219, "Burning Grove: Someday Peace Note", "ECHO_Mountain", "Note", new Vector3(265.785f, 10.851f, 115.305f), JEEB_LORE),
        CreateLocation(220, "Pallid Park: Nipple Yogurt Note", "Echo_Park", "Note1", new Vector3(170.29f, 15.642f, 325.25f), JEEB_LORE),
        
        // Jeeb Meat
        CreateLocation(231, "The BBQ Basket: Squirrel Meat", "Echo_BBQ", "SQ", new Vector3(449.096f, 13.877f, 447.922f), MEAT),
        CreateLocation(232, "Ritual Road: Bridge Condor 1 Meat", "ECHO_Road", "ROOT_9", Vector3.zero, MEAT),
        CreateLocation(233, "Ritual Road: Bridge Condor 2 Meat", "ECHO_Road", "ROOT_4", Vector3.zero, MEAT),
        CreateLocation(234, "Ritual Road: Bridge Condor 3 Meat", "ECHO_Road", "ROOT_8", Vector3.zero, MEAT),
        CreateLocation(235, "Ritual Road: Roadkill Bridge 1 Meat", "ECHO_Road", "GorePile3", new Vector3(104.061f, 14.046f, 196.116f), MEAT),
        CreateLocation(236, "Ritual Road: Roadkill Bridge 2 Meat", "ECHO_Road", "GorePile2", new Vector3(110.131f, 14.132f, 189.446f), MEAT),
        CreateLocation(237, "Ritual Road: Dollar Venereal Condor 1 Meat", "ECHO_Road", "ROOT_2", Vector3.zero, MEAT),
        CreateLocation(238, "Ritual Road: Dollar Venereal Condor 2 Meat", "ECHO_Road", "ROOT_5", Vector3.zero, MEAT),
        CreateLocation(239, "Ritual Road: Dollar Venereal Condor 3 Meat", "ECHO_Road", "ROOT_3", Vector3.zero, MEAT),
        CreateLocation(240, "Ritual Road: Tree Roadkill Meat", "ECHO_Road", "GorePile3", new Vector3(82.529f, 18.211f, 86.9f), MEAT),
        CreateLocation(241, "Ritual Road: Intersection Condor 1 Meat", "ECHO_Road", "ROOT_7", Vector3.zero, MEAT),
        CreateLocation(242, "Ritual Road: Intersection Condor 2 Meat", "ECHO_Road", "ROOT_6",  Vector3.zero, MEAT),
        CreateLocation(243, "Ritual Road: Intersection Roadkill Meat", "ECHO_Road", "GorePile2", new Vector3(57.524f, 18.927f, 67.231f), MEAT),
        CreateLocation(244, "Ritual Road: Questionable Car Crash Meat", "ECHO_Road", "GorePile2", new Vector3(43.575f, 18.927f, 32.738f), MEAT),
        CreateLocation(245, "Ritual Road: Legless Deer Meat", "ECHO_Road", "ROOT_1", Vector3.zero, MEAT),
        CreateLocation(246, "Ritual Road: Roadkill Under Truck Meat", "ECHO_Road", "GorePile2", new Vector3(255.715f, 14.568f, 221.220f), MEAT),
        CreateLocation(247, "Lake Linger: Dead Person Meat", "Echo_Lake", "Meat", new Vector3(440.951f, 14.647f, 327.091f), MEAT),
        CreateLocation(248, "Lake Linger: Hiding Otter 1 Meat", "Echo_Lake", "ROOT_5",Vector3.zero, MEAT),
        CreateLocation(249, "Lake Linger: Hiding Otter 2 Meat", "Echo_Lake", "ROOT_7", Vector3.zero, MEAT),
        CreateLocation(250, "Lake Linger: Hiding Otter 3 Meat", "Echo_Lake", "ROOT_17", Vector3.zero, MEAT),
        CreateLocation(251, "Lake Linger: Covered Body Meat", "Echo_Lake", "MEAT", new Vector3(406.72f, 17.664f, 441.93f), MEAT),
        CreateLocation(252, "Lake Linger: North House Otter 1 Meat", "Echo_Lake", "ROOT_19", Vector3.zero, MEAT),
        CreateLocation(253, "Lake Linger: North House Otter 2 Meat", "Echo_Lake", "ROOT_20", Vector3.zero, MEAT),
        CreateLocation(254, "Lake Linger: North House Otter 3 Meat", "Echo_Lake", "ROOT_4", Vector3.zero, MEAT),
        CreateLocation(255, "Lake Linger: North House Otter 4 Meat", "Echo_Lake", "ROOT_16", Vector3.zero, MEAT),
        CreateLocation(256, "Lake Linger: North House Otter 5 Meat", "Echo_Lake", "ROOT_2", Vector3.zero, MEAT),
        CreateLocation(257, "Lake Linger: North House Otter 6 Meat", "Echo_Lake", "ROOT_6", Vector3.zero, MEAT),
        CreateLocation(258, "Lake Linger: North House Otter 7 Meat", "Echo_Lake", "ROOT_9", Vector3.zero, MEAT),
        CreateLocation(259, "Lake Linger: Trapped Coffee Otter 1 Meat", "Echo_Lake", "ROOT_12", Vector3.zero, MEAT),
        CreateLocation(260, "Lake Linger: Trapped Coffee Otter 2 Meat", "Echo_Lake", "ROOT_13", Vector3.zero, MEAT),
        CreateLocation(261, "Lake Linger: Trapped Coffee Otter 3 Meat", "Echo_Lake", "ROOT_21", Vector3.zero, MEAT),
        CreateLocation(262, "Lake Linger: Dam Water Otter 1 Meat", "Echo_Lake", "ROOT_11", Vector3.zero, MEAT),
        CreateLocation(263, "Lake Linger: Dam Water Otter 2 Meat", "Echo_Lake", "ROOT_23", Vector3.zero, MEAT),
        CreateLocation(264, "Lake Linger: Warning Otter 1 Meat", "Echo_Lake", "ROOT_10", Vector3.zero, MEAT),
        CreateLocation(265, "Lake Linger: Warning Otter 2 Meat", "Echo_Lake", "ROOT_3", Vector3.zero, MEAT),
        CreateLocation(266, "Lake Linger: Warning Otter 3 Meat", "Echo_Lake", "ROOT_1", Vector3.zero, MEAT),
        CreateLocation(267, "Lake Linger: Warning Otter 4 Meat", "Echo_Lake", "ROOT_24", Vector3.zero, MEAT),
        CreateLocation(268, "Lake Linger: Warning Otter 5 Meat", "Echo_Lake", "ROOT_15", Vector3.zero, MEAT),
        CreateLocation(269, "Lake Linger: Trapped Start Otter 1 Meat", "Echo_Lake", "ROOT_8", Vector3.zero, MEAT),
        CreateLocation(270, "Lake Linger: Trapped Start Otter 2 Meat", "Echo_Lake", "ROOT_14", Vector3.zero, MEAT),
        CreateLocation(271, "Lake Linger: Trapped Start Otter 3 Meat", "Echo_Lake", "ROOT_22", Vector3.zero, MEAT),
        CreateLocation(272, "Lake Linger: Trapped Start Otter 4 Meat", "Echo_Lake", "ROOT_18", Vector3.zero, MEAT),
        CreateLocation(273, "Pallid Park: Little Doggy Meat", "Echo_Park", "ROOT_1", Vector3.zero, MEAT),
        CreateLocation(274, "Pallid Park: Dead Owner Meat", "Echo_Park", "GorePile2", new Vector3(93.573f, 15.474f, 293.784f), MEAT),
        CreateLocation(275, "Pallid Park: North Leech 1 Meat", "Echo_Park", "ROOT_8", Vector3.zero, MEAT),
        CreateLocation(276, "Pallid Park: North Leech 2 Meat", "Echo_Park", "ROOT_14", Vector3.zero, MEAT),
        CreateLocation(277, "Pallid Park: North Leech 3 Meat", "Echo_Park", "ROOT_10", Vector3.zero, MEAT),
        CreateLocation(278, "Pallid Park: North Leech 4 Meat", "Echo_Park", "ROOT_5", Vector3.zero, MEAT),
        CreateLocation(279, "Pallid Park: South Leech 1 Meat", "Echo_Park", "ROOT_11", Vector3.zero, MEAT),
        CreateLocation(280, "Pallid Park: South Leech 2 Meat", "Echo_Park", "ROOT_7", Vector3.zero, MEAT),
        CreateLocation(281, "Pallid Park: South Leech 3 Meat", "Echo_Park", "ROOT_2", Vector3.zero, MEAT),
        CreateLocation(282, "Pallid Park: South Leech 4 Meat", "Echo_Park", "ROOT_9", Vector3.zero, MEAT),
        CreateLocation(283, "Pallid Park: Ghost 1 Meat", "Echo_Park", "GorePile_13", Vector3.zero, MEAT),
        CreateLocation(284, "Pallid Park: Ghost 2 Meat", "Echo_Park", "GorePile_12", Vector3.zero, MEAT),
        CreateLocation(285, "Burning Grove: Burning Dog 1 Meat", "ECHO_Mountain", "ROOT1_3", Vector3.zero, MEAT),
        CreateLocation(286, "Burning Grove: Burning Dog 2 Meat", "ECHO_Mountain", "ROOT1_4", Vector3.zero, MEAT),
        CreateLocation(287, "Burning Grove: Burning Dog 3 Meat", "ECHO_Mountain", "ROOT1_7", Vector3.zero, MEAT),
        CreateLocation(288, "Burning Grove: Burning Dog 4 Meat", "ECHO_Mountain", "ROOT1_9", Vector3.zero, MEAT),
        CreateLocation(289, "Burning Grove: Burning Dog 5 Meat", "ECHO_Mountain", "ROOT1_2", Vector3.zero, MEAT),
        CreateLocation(290, "Burning Grove: Burning Dog 6 Meat", "ECHO_Mountain", "ROOT1_12", Vector3.zero, MEAT),
        CreateLocation(291, "Burning Grove: Rotting Corpse Meat", "ECHO_Mountain", "GorePile2", new Vector3(272.011f, 7.089f, 116.213f), MEAT),
        
        // Hyena Locations
        CreateLocation(451, "Turkey Creek: Hyena Deep in Creek", "", "", new Vector3(), JEB_HYENA),
        CreateLocation(452, "Frigid Valley: Hyena Behind Truck in Hills", "", "", new Vector3(), JEB_HYENA),
        CreateLocation(453, "Howling Marsh: Hyena Behind Trees", "", "", new Vector3(), JEB_HYENA),
        CreateLocation(454, "Lake Linger: Hyena Behind Dam Fences", "Echo_Lake", "", new Vector3(), JEEB_HYENA),
        CreateLocation(455, "Burning Grove: Hyena In Attic", "", "", new Vector3(), JEEB_HYENA),
    };

    private static UktenaLocation CreateLocation(int id, string name, string scene, string objectName, Vector3 position, string type, bool ignore = false)
    {
        var location = new UktenaLocation(id, name, scene, objectName, position);
        
        NameToLocations[location.Name] =  location;
        if (ignore)
        {
            return location;
        }
        switch (type)
        {
            case JEB:
            {
                BaseJebLocations.Add(location);
                if (location.Name.Contains("Cleared"))
                {
                    ClearLookup[location.ObjectName] = location.ID;
                }
                break;
            }
            case JEEB:
            {
                BaseJeebLocations.Add(location);
                if (location.Name.Contains("Cleared"))
                {
                    ClearLookup[location.ObjectName] = location.ID;
                }
                break;
            }
            case JEB_LORE:
            {
                BaseJebLoreLocations.Add(location);
                break;
            }
            case JEEB_LORE:
            {
                BaseJeebLoreLocations.Add(location);
                break;
            }
            case PHOTO:
            {
                BasePhotoLocations.Add(location);
                break;
            }
            case MEAT:
            {
                BaseMeatLocations.Add(location);
                break;
            }
            case JEB_HYENA:
            {
                BaseJebHyenaLocations.Add(location);
                break;
            }
            case JEEB_HYENA:
            {
                BaseJeebHyenaLocations.Add(location);
                break;
            }
        }
        
        return location;
    }

}