// Map.Player
using Bolt;
using Steamworks;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player
{
    private static readonly Dictionary<string, ulong> CachedIds = new Dictionary<string, ulong>();

    public BoltEntity Entity { get; }

    public ulong SteamId
    {
        get
        {
            if (!CachedIds.ContainsKey(Name))
            {
                return CachedIds[Name] = CoopLobby.Instance.AllMembers.FirstOrDefault((CSteamID o) => SteamFriends.GetFriendPersonaName(o) == Name).m_SteamID;
            }
            return CachedIds[Name];
        }
    }

    public string Name => Entity.GetState<IPlayerState>().name;

    public BoltPlayerSetup PlayerSetup => Entity.GetComponent<BoltPlayerSetup>();

    public CoopPlayerRemoteSetup CoopPlayer => Entity.GetComponent<CoopPlayerRemoteSetup>();

    public Transform Transform => Entity.transform;

    public Vector3 Position => Transform.position;

    public NetworkId NetworkId => Entity.networkId;

    public Player(BoltEntity player)
    {
        Entity = player;
    }
}
