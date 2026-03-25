using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using Uktena64Randomizer.Data;
using Uktena64Randomizer.Patches;
using Uktena64Randomizer.Utils;
using UnityEngine;

namespace Uktena64Randomizer.Archipelago;

public class ArchipelagoClient
{
    public const string APVersion = "0.6.6";
    private const string Game = "Uktena 64";

    public static bool Authenticated;
    private bool attemptingConnection;

    public static ArchipelagoData ServerData = new();
    private DeathLinkHandler DeathLinkHandler;
    private ArchipelagoSession _session;
    private readonly SortedDictionary<long, ArchipelagoItem> _locationTable = new() { };
    private static readonly Queue<ReceivedItem> ItemsToProcess = new();

    /// <summary>
    /// call to connect to an Archipelago session. Connection info should already be set up on ServerData
    /// </summary>
    /// <returns></returns>
    public void Connect()
    {
        if (Authenticated || attemptingConnection) return;
        attemptingConnection = true;

        try
        {
            _session = ArchipelagoSessionFactory.CreateSession(ServerData.Uri);
            SetupSession();
        }
        catch (Exception e)
        {
            Plugin.Log.LogError(e);
        }

        TryConnect();
    }

    /// <summary>
    /// add handlers for Archipelago events
    /// </summary>
    private void SetupSession()
    {
        _session.MessageLog.OnMessageReceived += message => ArchipelagoConsole.LogMessage(message.ToString());
        _session.Items.ItemReceived += OnItemReceived;
        _session.Socket.ErrorReceived += OnSessionErrorReceived;
        _session.Socket.SocketClosed += OnSessionSocketClosed;
    }

    /// <summary>
    /// attempt to connect to the server with our connection info
    /// </summary>
    private void TryConnect()
    {
        try
        {
            // it's safe to thread this function call but unity notoriously hates threading so do not use excessively
            ThreadPool.QueueUserWorkItem(
                _ => HandleConnectResult(
                    _session.TryConnectAndLogin(
                        Game,
                        ServerData.SlotName,
                        ItemsHandlingFlags.AllItems,
                        new Version(APVersion),
                        password: ServerData.Password,
                        requestSlotData: ServerData.NeedSlotData
                    )));
        }
        catch (Exception e)
        {
            Plugin.Log.LogError(e);
            HandleConnectResult(new LoginFailure(e.ToString()));
            attemptingConnection = false;
        }
    }

    /// <summary>
    /// handle the connection result and do things
    /// </summary>
    /// <param name="result"></param>
    private void HandleConnectResult(LoginResult result)
    {
        string outText;
        if (result.Successful)
        {
            Plugin.Log.LogInfo("If you don't see HUMAN RIGHTS connection failed.");
            var success = (LoginSuccessful)result;
            Plugin.Log.LogInfo(1);
            ServerData.SetupSession(success.SlotData, _session.RoomState.Seed);
            Plugin.Log.LogInfo(2);
            
            DeathLinkHandler = new(_session.CreateDeathLinkService(), ServerData.SlotName, ServerData.DeathLink);
            _session.Locations.CompleteLocationChecksAsync(ServerData.CheckedLocations.ToArray());
            
            BuildLocations();
            
            Plugin.Log.LogInfo(4);
            
            Plugin.Log.LogInfo(5);
            outText = $"Successfully connected to {ServerData.Uri} as {ServerData.SlotName}!";
            
            Plugin.Log.LogInfo(6);
            ArchipelagoConsole.LogMessage(outText);
            
            Plugin.Log.LogInfo(7);
            Authenticated = true;
            Plugin.Log.LogInfo("HUMAN RIGHTS!");
        }
        else
        {
            var failure = (LoginFailure)result;
            outText = $"Failed to connect to {ServerData.Uri} as {ServerData.SlotName}.";
            outText = failure.Errors.Aggregate(outText, (current, error) => current + $"\n    {error}");

            Plugin.Log.LogError(outText);

            Authenticated = false;
            Disconnect();
        }

        ArchipelagoConsole.LogMessage(outText);
        attemptingConnection = false;
    }
    
    private void BuildLocationTable()
        {
            List<int> locations = new();

            if (ServerData.PlayableCampaign != Campaign.Jeeb)
            {
                locations.AddRange(UktenaLocations.BaseJebLocations.Select(location => location.ID));
                if (ServerData.Photographer)
                {
                    locations.AddRange(UktenaLocations.BasePhotoLocations.Select(location => location.ID));
                }
                if (ServerData.RogueScholar)
                {
                    locations.AddRange(UktenaLocations.BaseJebLoreLocations.Select(location => location.ID));
                }
                if (ServerData.Hyenas)
                {
                    locations.AddRange(UktenaLocations.BaseJebHyenaLocations.Select(location => location.ID));
                }
            }

            if (ServerData.PlayableCampaign != Campaign.Jeb)
            {
                locations.AddRange(UktenaLocations.BaseJeebLocations.Select(location => location.ID));
                if (ServerData.BBQChef)
                {
                    locations.AddRange(UktenaLocations.BaseMeatLocations.Select(location => location.ID));
                }
                if (ServerData.RogueScholar)
                {
                    locations.AddRange(UktenaLocations.BaseJeebLoreLocations.Select(location => location.ID));
                }
                if (ServerData.Hyenas)
                {
                    locations.AddRange(UktenaLocations.BaseJeebHyenaLocations.Select(location => location.ID));
                }
            }
            
            foreach (var id in locations)
            {
                _locationTable[id] = null;
            }
        }
    
    private void BuildLocations()
    {
        BuildLocationTable();
        // Scout unchecked locations
        var uncheckedLocationIDs = from locationID in _locationTable.Keys select locationID;
        var locations = _session.Locations.AllLocations;
        foreach (var location in uncheckedLocationIDs)
        {
            if (locations.Contains(location))
            {
                continue;
            }
            Plugin.Log.LogWarning($"There's a location you're trying to scout that isn't there!  Location: {location}");
        }
        var scoutedInfoTask = Task.Run(async () => await _session.Locations.ScoutLocationsAsync(false, uncheckedLocationIDs.ToArray()));
        //Task<LocationInfoPacket> locationInfoTask = Task.Run(async () => await Session.Locations.ScoutLocationsAsync(false, uncheckedLocationIDs.ToArray()));
        if (scoutedInfoTask.IsFaulted)
        {
            Plugin.Log.LogError(scoutedInfoTask.Exception.GetBaseException().Message);
            return;
        }
        var scoutedInfo = scoutedInfoTask.Result;
        
        foreach (var item in scoutedInfo.Values)
        {
            int locationID = (int)item.LocationId;
            _locationTable[locationID] = new ArchipelagoItem(item, false);
        }
        ServerData.LocationTable = _locationTable;
    }

    /// <summary>
    /// something went wrong, or we need to properly disconnect from the server. cleanup and re null our session
    /// </summary>
    private void Disconnect()
    {
        Plugin.Log.LogDebug("disconnecting from server...");
        _session?.Socket.DisconnectAsync();
        _session = null;
        Authenticated = false;
    }

    public void SendAPMessage(string message)
    {
        _session.Socket.SendPacketAsync(new SayPacket { Text = message });
    }

    private void OnItemReceived(ReceivedItemsHelper helper)
    {
        var item = helper.DequeueItem();
        Plugin.Log.LogInfo($"Received {item.ItemName} with index of {helper.Index}");
        if (helper.Index < ServerData.Index)
        {
            return;
        }
        if (!ServerData.Inventory.ContainsKey(item.ItemName))
        {
            ServerData.Inventory[item.ItemName] = 0;
        }

        ServerData.Inventory[item.ItemName] += 1;
        Plugin.Log.LogInfo($"Added {item.ItemName} to inventory");
        Plugin.Log.LogInfo($"Sanity Check: {ServerData.Inventory[item.ItemName]}");
        LevelSetup.UpdateGameInventoryIfRelevant(item.ItemName, ServerData.Inventory[item.ItemName]);
        ServerData.Index++;
    }

    private void OnSessionErrorReceived(Exception e, string message)
    {
        Plugin.Log.LogError(e);
        ArchipelagoConsole.LogMessage(message);
    }

    private void OnSessionSocketClosed(string reason)
    {
        Plugin.Log.LogError($"Connection to Archipelago lost: {reason}");
        Disconnect();
    }
    
    public long GetLocationIDFromName(string locationName)
    {
        return _session.Locations.GetLocationIdFromName(Game, locationName);
    }

    public string GetLocationNameFromID(long location)
    {
        return _session.Locations.GetLocationNameFromId(location, "Uktena 64");
    }
    
    public string GetPlayerNameFromSlot(int slot)
    {
        return _session.Players.GetPlayerName(slot);
    }

    public void SendLocation(long locationID)
    {
        _session.Locations.CompleteLocationChecks(locationID);
    }

    public void Win()
    {
        _session.SetGoalAchieved();
    }

    public void ReceiveDeathLink()
    {
        DeathLinkHandler.KillPlayer();
    }

    public void SendDeathLink()
    {
        DeathLinkHandler.SendDeathLink();
    }
}