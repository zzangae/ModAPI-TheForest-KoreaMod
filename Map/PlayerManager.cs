// Map.PlayerManager
using System.Collections.Generic;
using System.Linq;

public class PlayerManager
{
    public static List<Player> Players
    {
        get;
    } = new List<Player>();


    public PlayerManager(Map instance)
    {
    }

    public Player GetPlayerBySteamId(ulong steamId)
    {
        return Players.FirstOrDefault((Player o) => o.SteamId == steamId);
    }
}
