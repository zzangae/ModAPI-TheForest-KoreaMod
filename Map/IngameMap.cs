// Map.IngameMap
using ModAPI;
using ModAPI.Attributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheForest.Items;
using TheForest.Items.Inventory;
using TheForest.UI.Multiplayer;
using TheForest.Utils;
using UnityEngine;

public class IngameMap : MonoBehaviour
{
    public class MarkerSetting
    {
        public string ID;

        public string Label;

        public int Texture;

        public Color Color;

        public string Category;

        public bool Selected = true;
    }

    public class MarkerCategory
    {
        public bool Selected;

        public List<MarkerSetting> Markers = new List<MarkerSetting>();

        public Color Color;
    }

    public static Vector2 WorldToMapFactor = new Vector2(6250f, 3550f);

    public static Vector2 WorldToMapOffset = new Vector2(5f, 15f);

    public static Dictionary<string, MarkerSetting> markerSettings = new Dictionary<string, MarkerSetting>
    {
        {
            "Big tree",
            new MarkerSetting
            {
                Label = "큰 나무",
                Texture = 0,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Good spot",
            new MarkerSetting
            {
                Label = "좋은 자리",
                Texture = 1,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Passenger",
            new MarkerSetting
            {
                ID = "Passenger",
                Label = "승객",
                Texture = 2,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Sharks",
            new MarkerSetting
            {
                Label = "상어",
                Texture = 3,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Bedroll",
            new MarkerSetting
            {
                Label = "침상",
                Texture = 4,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Tent",
            new MarkerSetting
            {
                Label = "텐트",
                Texture = 5,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Boat",
            new MarkerSetting
            {
                Label = "보트",
                Texture = 6,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Plane",
            new MarkerSetting
            {
                Label = "비행기",
                Texture = 7,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Anchor",
            new MarkerSetting
            {
                Label = "닻",
                Texture = 8,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Suitcase",
            new MarkerSetting
            {
                Label = "여행가방",
                Texture = 9,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Cave",
            new MarkerSetting
            {
                Label = "동굴",
                Texture = 10,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Collectible",
            new MarkerSetting
            {
                Label = "수집물",
                Texture = 11,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Paper",
            new MarkerSetting
            {
                Label = "사진",
                Texture = 48,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Magazine",
            new MarkerSetting
            {
                Label = "잡지",
                Texture = 48,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Cassette",
            new MarkerSetting
            {
                Label = "카세트",
                Texture = 40,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Camcorder",
            new MarkerSetting
            {
                Label = "캠코더",
                Texture = 41,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Tape",
            new MarkerSetting
            {
                Label = "테이프",
                Texture = 42,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Toy",
            new MarkerSetting
            {
                Label = "인형",
                Texture = 43,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Map",
            new MarkerSetting
            {
                Label = "지도",
                Texture = 44,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Compass",
            new MarkerSetting
            {
                Label = "나침반",
                Texture = 45,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Pedometer",
            new MarkerSetting
            {
                Label = "만보계",
                Texture = 47,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Fortune",
            new MarkerSetting
            {
                Label = "재산",
                Texture = 46,
                Color = new Color(0.9f, 0.1f, 1f),
                Category = "수집물"
            }
        },
        {
            "Explodable",
            new MarkerSetting
            {
                Label = "폭발물",
                Texture = 12,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Crate",
            new MarkerSetting
            {
                Label = "상자",
                Texture = 13,
                Color = new Color(0.75f, 0.75f, 0.75f),
                Category = "기본"
            }
        },
        {
            "Berries",
            new MarkerSetting
            {
                Label = "열매",
                Texture = 14,
                Color = new Color(0.5f, 1f, 0.5f),
                Category = "음식"
            }
        },
        {
            "Flower",
            new MarkerSetting
            {
                Label = "꽃",
                Texture = 14,
                Color = new Color(0.5f, 1f, 0.5f),
                Category = "음식"
            }
        },
        {
            "Mushrooms",
            new MarkerSetting
            {
                Label = "버섯",
                Texture = 16,
                Color = new Color(0.5f, 1f, 0.5f),
                Category = "음식"
            }
        },
        {
            "Oyster",
            new MarkerSetting
            {
                Label = "굴",
                Texture = 16,
                Color = new Color(0.5f, 1f, 0.5f),
                Category = "음식"
            }
        },
        {
            "Fish",
            new MarkerSetting
            {
                Label = "물고기",
                Texture = 15,
                Color = new Color(0.5f, 1f, 0.5f),
                Category = "음식"
            }
        },
        {
            "Medicine",
            new MarkerSetting
            {
                Label = "약",
                Texture = 17,
                Color = new Color(0.5f, 1f, 0.5f),
                Category = "음식"
            }
        },
        {
            "Native camp",
            new MarkerSetting
            {
                Label = "원주민 캠프장",
                Texture = 18,
                Color = Color.red,
                Category = "원주민"
            }
        },
        {
            "Cannibal",
            new MarkerSetting
            {
                Label = "식인종",
                Texture = 35,
                Color = Color.red,
                Category = "원주민"
            }
        },
        {
            "Mutant",
            new MarkerSetting
            {
                Label = "돌연변이",
                Texture = 49,
                Color = Color.red,
                Category = "원주민"
            }
        },
        {
            "Babies",
            new MarkerSetting
            {
                Label = "아기돌연변이",
                Texture = 35,
                Color = Color.red,
                Category = "원주민"
            }
        },
        {
            "Flashlight",
            new MarkerSetting
            {
                Label = "손전등",
                Texture = 19,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        },
        {
            "DrinkZone",
            new MarkerSetting
            {
                Label = "마시는지역",
                Texture = 20,
                Color = new Color(0.5f, 1f, 0.5f),
                Category = "음식"
            }
        },
        {
            "Flare",
            new MarkerSetting
            {
                Label = "플레어",
                Texture = 21,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        },
        {
            "Money",
            new MarkerSetting
            {
                Label = "돈",
                Texture = 22,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        },
        {
            "Rope",
            new MarkerSetting
            {
                Label = "밧줄",
                Texture = 23,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        },
        {
            "Circuit",
            new MarkerSetting
            {
                Label = "회로판",
                Texture = 24,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        },
        {
            "Rebreather",
            new MarkerSetting
            {
                Label = "수중호흡기",
                Texture = 25,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        },
        {
            "Air Canister",
            new MarkerSetting
            {
                Label = "산소통",
                Texture = 26,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        },
        {
            "Pot",
            new MarkerSetting
            {
                Label = "냄비",
                Texture = 27,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        },
        {
            "Stick",
            new MarkerSetting
            {
                Label = "막대기",
                Texture = 28,
                Color = new Color(1f, 0.6f, 0f),
                Category = "무기"
            }
        },
        {
            "Axe",
            new MarkerSetting
            {
                Label = "도끼",
                Texture = 29,
                Color = new Color(1f, 0.6f, 0f),
                Category = "무기"
            }
        },
        {
            "Bow",
            new MarkerSetting
            {
                Label = "활",
                Texture = 37,
                Color = new Color(1f, 0.6f, 0f),
                Category = "무기"
            }
        },
        {
            "Gun",
            new MarkerSetting
            {
                Label = "총",
                Texture = 30,
                Color = new Color(1f, 0.6f, 0f),
                Category = "무기"
            }
        },
        {
            "Chainsaw",
            new MarkerSetting
            {
                Label = "전기톱",
                Texture = 38,
                Color = new Color(1f, 0.6f, 0f),
                Category = "무기"
            }
        },
        {
            "Hairspray",
            new MarkerSetting
            {
                Label = "헤어스프레이",
                Texture = 39,
                Color = new Color(1f, 0.6f, 0f),
                Category = "무기"
            }
        },
        {
            "Fuel Canister",
            new MarkerSetting
            {
                Label = "연료",
                Texture = 20,
                Color = new Color(1f, 0.6f, 0f),
                Category = "무기"
            }
        },
        {
            "Ammo",
            new MarkerSetting
            {
                Label = "총알",
                Texture = 34,
                Color = new Color(1f, 0.6f, 0f),
                Category = "무기"
            }
        },
        {
            "Paint",
            new MarkerSetting
            {
                Label = "페인트",
                Texture = 33,
                Color = new Color(0.75f, 0.75f, 1f),
                Category = "도구"
            }
        }
    };

    public static bool Opened = false;

    protected Map Overworld;

    protected Map Underworld;

    protected Texture2D background;

    protected Texture2D foreground;

    protected Vector2 BaseSize = new Vector2(200f, 200f);

    protected Vector2 Position = Vector2.zero;

    protected float Zoom = 1f;

    protected int texNum = 31;

    public static bool livemarkers = true;

    private Map.Marker playerMarker;

    private Map.Marker playerMPMarker;

    private Map.Marker mutantMarker;

    private Map currentMap;

    protected bool Drag;

    protected Vector2 LastMousePos = Vector2.zero;

    protected Downloader overworldMapLoader;

    protected Downloader underworldMapLoader;

    protected GUIStyle WhiteLabel;

    protected float ShowPhase;

    protected Texture2D Markers;

    protected Dictionary<string, MarkerCategory> Categories;

    protected bool overworldParsed;

    protected bool underworldParsed;

    protected Texture2D overworldTexture;

    protected Texture2D underworldTexture;

    protected bool visible;

    public static Rect GetTextureCoords(int index)
    {
        int num = index % 6;
        int num2 = Mathf.FloorToInt((float)index / 6f);
        float x = (float)num / 6f;
        float y = 1f - (float)(num2 + 1) / 9f;
        return new Rect(x, y, 0.166666672f, 0.111111112f);
    }

    public static Vector2 WorldToMap(Vector3 world)
    {
        return new Vector2((0f - world.z + WorldToMapOffset.x) / WorldToMapFactor.x, (0f - world.x + WorldToMapOffset.y) / WorldToMapFactor.y);
    }

    [ExecuteOnGameStart]
    public static void Init()
    {
        new GameObject("__Map__").AddComponent<IngameMap>();
    }

    public bool DrawMarker(Map.Marker marker, float angle = 0f, float scale = 1f)
    {
        Vector2 vector = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f) + Position * Zoom;
        Vector2 vector2 = new Vector2((float)Screen.height * 1.777778f, (float)Screen.height) * Zoom;
        Vector2 vector3 = WorldToMap(marker.WorldPosition);
        vector3.x *= vector2.x;
        vector3.y *= vector2.y;
        float num = GetMarkerSize() * scale;
        Rect textureCoords = GetTextureCoords(marker.Class.Texture);
        Vector2 vector4 = new Vector2(vector.x + vector3.x, vector.y + vector3.y);
        Rect position = new Rect(vector4.x - num / 2f, vector4.y - num / 2f, num, num);
        GUI.color = marker.Class.Color;
        if (angle != 0f)
        {
            Matrix4x4 matrix = GUI.matrix;
            GUIUtility.RotateAroundPivot(angle, vector4);
            GUI.DrawTextureWithTexCoords(position, Markers, textureCoords);
            GUI.matrix = matrix;
        }
        else
        {
            GUI.DrawTextureWithTexCoords(position, Markers, textureCoords);
        }
        GUI.color = Color.white;
        return position.Contains(Event.current.mousePosition);
    }

    private void OnGUI()
    {
        try
        {
            GUI.skin = Interface.Skin;
            GUI.color = Color.white;
            if (Opened)
            {
                GUI.DrawTexture(new Rect(0f, 0f, (float)Camera.main.pixelWidth, (float)Camera.main.pixelHeight), background);
                if (currentMap.Textures != null && currentMap.Textures.Length != 0)
                {
                    Vector2 vector = new Vector2((float)Screen.height * 1.777778f, (float)Screen.height) * Zoom;
                    if (vector.x < vector.y)
                    {
                        vector.y = vector.x;
                    }
                    else
                    {
                        vector.x = vector.y;
                    }
                    Vector2 vector2 = vector / 8f;
                    Vector2 vector3 = new Vector2((float)Screen.width / 2f, (float)Screen.height / 2f) + Position * Zoom - vector / 2f;
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            int num = i + (8 - j - 1) * 8;
                            GUI.DrawTexture(new Rect(vector3.x + vector2.x * (float)i, vector3.y + vector2.y * (float)j, vector2.x, vector2.y), currentMap.Textures[num]);
                        }
                    }
                    GUI.DrawTexture(new Rect(0f, 0f, 500f, 500f), currentMap.Texture);
                    List<Map.Marker> list = new List<Map.Marker>();
                    for (int k = 0; k < currentMap.Markers.Count; k++)
                    {
                        if (currentMap.Markers[k].Class.Selected && DrawMarker(currentMap.Markers[k], 0f, 1f))
                        {
                            list.Add(currentMap.Markers[k]);
                        }
                    }
                    playerMarker = new Map.Marker
                    {
                        Class = new MarkerSetting
                        {
                            ID = "Player",
                            Color = Color.green,
                            Label = "플레이어",
                            Texture = 31,
                            Category = "플레이어"
                        },
                        Description = "플레이어",
                        WorldPosition = Vector3.zero
                    };
                    playerMarker.WorldPosition.x = 0f - LocalPlayer.Transform.position.z;
                    playerMarker.WorldPosition.y = LocalPlayer.Transform.position.y;
                    playerMarker.WorldPosition.z = LocalPlayer.Transform.position.x;
                    Map.Marker marker = playerMarker;
                    Quaternion quaternion = LocalPlayer.Transform.rotation;
                    if (DrawMarker(marker, 90f + quaternion.eulerAngles.y, 2f))
                    {
                        list.Add(playerMarker);
                    }
                    if (BoltNetwork.isRunning && (UnityEngine.Object)Scene.SceneTracker != (UnityEngine.Object)null && Scene.SceneTracker.allPlayerEntities != null)
                    {
                        PlayerManager.Players.Clear();
                        PlayerManager.Players.AddRange(from o in Scene.SceneTracker.allPlayerEntities.Where(delegate (BoltEntity o)
                        {
                            if (o.isAttached && o.StateIs<IPlayerState>() && (UnityEngine.Object)LocalPlayer.Entity != (UnityEngine.Object)o && o.gameObject.activeSelf && o.gameObject.activeInHierarchy)
                            {
                                return (UnityEngine.Object)o.GetComponent<BoltPlayerSetup>() != (UnityEngine.Object)null;
                            }
                            return false;
                        })
                                                       orderby o.GetState<IPlayerState>().name
                                                       select new Player(o));
                    }
                    if (BoltNetwork.isRunning)
                    {
                        foreach (Player player in PlayerManager.Players)
                        {
                            playerMPMarker = new Map.Marker
                            {
                                Class = new MarkerSetting
                                {
                                    ID = "Player",
                                    Color = Color.red,
                                    Label = player.Name,
                                    Texture = 32,
                                    Category = "플레이어"
                                },
                                Description = player.Name,
                                WorldPosition = Vector3.zero
                            };
                            playerMPMarker.WorldPosition.x = 0f - player.Position.z;
                            playerMPMarker.WorldPosition.y = player.Position.y;
                            playerMPMarker.WorldPosition.z = player.Position.x;
                            if (DrawMarker(playerMPMarker, 0f, 2f))
                            {
                                list.Add(playerMPMarker);
                            }
                        }
                    }
                    if (livemarkers)
                    {
                        List<GameObject> list2;
                        List<GameObject>.Enumerator enumerator2;
                        if (LocalPlayer.IsInCaves)
                        {
                            list2 = new List<GameObject>(Scene.MutantControler.activeCaveCannibals);
                            enumerator2 = Scene.MutantControler.activeInstantSpawnedCannibals.GetEnumerator();
                            try
                            {
                                while (enumerator2.MoveNext())
                                {
                                    GameObject current2 = enumerator2.Current;
                                    if (!list2.Contains(current2))
                                    {
                                        list2.Add(current2);
                                    }
                                }
                            }
                            finally
                            {
                                ((IDisposable)enumerator2).Dispose();
                            }
                            list2.RemoveAll((GameObject o) => (UnityEngine.Object)o == (UnityEngine.Object)null);
                            list2.RemoveAll((GameObject o) => (bool)o != o.activeSelf);
                        }
                        else
                        {
                            list2 = new List<GameObject>(Scene.MutantControler.activeWorldCannibals);
                            enumerator2 = Scene.MutantControler.activeInstantSpawnedCannibals.GetEnumerator();
                            try
                            {
                                while (enumerator2.MoveNext())
                                {
                                    GameObject current3 = enumerator2.Current;
                                    if (!list2.Contains(current3))
                                    {
                                        list2.Add(current3);
                                    }
                                }
                            }
                            finally
                            {
                                ((IDisposable)enumerator2).Dispose();
                            }
                            list2.RemoveAll((GameObject o) => (UnityEngine.Object)o == (UnityEngine.Object)null);
                            list2.RemoveAll((GameObject o) => (bool)o != o.activeSelf);
                        }
                        if (list2.Count > 0)
                        {
                            enumerator2 = list2.GetEnumerator();
                            try
                            {
                                while (enumerator2.MoveNext())
                                {
                                    GameObject current4 = enumerator2.Current;
                                    if ((UnityEngine.Object)current4 != (UnityEngine.Object)null)
                                    {
                                        mutantMarker = new Map.Marker
                                        {
                                            Class = new MarkerSetting
                                            {
                                                ID = "Live Cannibal",
                                                Color = Color.red,
                                                Label = "식인종",
                                                Texture = 36,
                                                Category = "원주민"
                                            },
                                            Description = "식인종",
                                            WorldPosition = Vector3.zero
                                        };
                                        mutantMarker.WorldPosition.x = 0f - current4.transform.position.z;
                                        mutantMarker.WorldPosition.y = current4.transform.position.y;
                                        mutantMarker.WorldPosition.z = current4.transform.position.x;
                                        quaternion = current4.GetComponentInChildren<Animator>().rootRotation;
                                        float y = quaternion.eulerAngles.y;
                                        if (DrawMarker(mutantMarker, 90f + y, 2f))
                                        {
                                            list.Add(mutantMarker);
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                ((IDisposable)enumerator2).Dispose();
                            }
                        }
                    }
                    if (list.Count > 0)
                    {
                        Vector2 vector4 = new Vector2(Event.current.mousePosition.x - 125f, Event.current.mousePosition.y + 5f);
                        float height = (float)list.Count * 30f + 5f;
                        GUI.Box(new Rect(vector4.x, vector4.y, 120f, height), "");
                        float num2 = 0f;
                        for (int l = 0; l < list.Count; l++)
                        {
                            Rect position = new Rect(vector4.x, vector4.y + num2, 120f, 30f);
                            GUI.color = new Color(list[l].Class.Color.r, list[l].Class.Color.g, list[l].Class.Color.b, 0.2f);
                            GUI.DrawTexture(position, foreground);
                            GUI.color = list[l].Class.Color;
                            GUI.DrawTextureWithTexCoords(new Rect(vector4.x + 5f, vector4.y + num2 + 5f, 20f, 20f), Markers, GetTextureCoords(list[l].Class.Texture));
                            GUI.color = Color.white;
                            GUI.Label(new Rect(vector4.x + 30f, vector4.y + num2 + 5f, 90f, 30f), list[l].Class.Label);
                            num2 += 30f;
                        }
                    }
                    float num3 = 0f;
                    int num4 = 0;
                    Dictionary<string, MarkerCategory>.ValueCollection.Enumerator enumerator3 = Categories.Values.GetEnumerator();
                    List<MarkerSetting>.Enumerator enumerator4;
                    try
                    {
                        while (enumerator3.MoveNext())
                        {
                            MarkerCategory current5 = enumerator3.Current;
                            num3 += 20f;
                            if (current5.Selected)
                            {
                                num4 = 0;
                                enumerator4 = current5.Markers.GetEnumerator();
                                try
                                {
                                    while (enumerator4.MoveNext())
                                    {
                                        MarkerSetting current8 = enumerator4.Current;
                                        if (num4 == 0)
                                        {
                                            num3 += 20f;
                                        }
                                        num4++;
                                        if (num4 >= 2)
                                        {
                                            num4 = 0;
                                        }
                                    }
                                }
                                finally
                                {
                                    ((IDisposable)enumerator4).Dispose();
                                }
                            }
                        }
                    }
                    finally
                    {
                        ((IDisposable)enumerator3).Dispose();
                    }
                    float num5 = 70f;
                    GUI.Box(new Rect(10f, (float)Screen.height - (num3 + 30f) - num5, 200f, num3 + 35f), "필터", GUI.skin.window);
                    int num6 = 0;
                    int num7 = 0;
                    enumerator3 = Categories.Values.GetEnumerator();
                    try
                    {
                        while (enumerator3.MoveNext())
                        {
                            MarkerCategory current6 = enumerator3.Current;
                            num7 = 0;
                            string category = current6.Markers[0].Category;
                            current6.Selected = GUI.Toggle(new Rect(10f, (float)Screen.height - num3 - num5 + (float)num6, 200f, 20f), current6.Selected, category, GUI.skin.button);
                            num6 += 20;
                            if (current6.Selected)
                            {
                                enumerator4 = current6.Markers.GetEnumerator();
                                try
                                {
                                    while (enumerator4.MoveNext())
                                    {
                                        MarkerSetting current7 = enumerator4.Current;
                                        Rect position2 = new Rect((float)(10 + num7), (float)Screen.height - num3 - num5 + (float)num6, 100f, 20f);
                                        GUI.color = new Color(current6.Color.r, current6.Color.g, current6.Color.b, current7.Selected ? 0.2f : 0f);
                                        GUI.DrawTexture(position2, foreground);
                                        GUI.color = current6.Color;
                                        position2 = new Rect((float)(10 + num7), (float)Screen.height - num3 - num5 + (float)num6, 20f, 20f);
                                        GUI.DrawTextureWithTexCoords(position2, Markers, GetTextureCoords(current7.Texture));
                                        GUI.color = Color.white;
                                        position2 = new Rect((float)(35 + num7), (float)Screen.height - num3 - num5 + (float)num6, 65f, 20f);
                                        current7.Selected = GUI.Toggle(position2, current7.Selected, current7.Label, GUI.skin.label);
                                        num7 += 100;
                                        if (num7 >= 200)
                                        {
                                            num7 = 0;
                                            num6 += 20;
                                        }
                                    }
                                }
                                finally
                                {
                                    ((IDisposable)enumerator4).Dispose();
                                }
                                if (num7 == 100)
                                {
                                    num6 += 20;
                                }
                            }
                        }
                    }
                    finally
                    {
                        ((IDisposable)enumerator3).Dispose();
                    }
                    GUI.Label(new Rect(40f, (float)Screen.height - 60f, 200f, 20f), "실시간 식인종", GUI.skin.label);
                    livemarkers = GUI.Toggle(new Rect(10f, (float)Screen.height - 60f, 20f, 30f), livemarkers, "");
                    if (GUI.Button(new Rect(10f, (float)Screen.height - 30f, 200f, 20f), "저장"))
                    {
                        SaveMarkers(Categories, "Mods/Map.settings");
                    }
                    if (Event.current.type == EventType.MouseDown)
                    {
                        Drag = true;
                        LastMousePos = Event.current.mousePosition;
                    }
                    else if (Event.current.type == EventType.MouseDrag)
                    {
                        Vector2 a = Event.current.mousePosition - LastMousePos;
                        float num8 = (float)Mathf.Min(Screen.width, Screen.height);
                        Position += a / Zoom;
                        Position.x = Mathf.Clamp(Position.x, num8 / -2f, num8 / 2f);
                        Position.y = Mathf.Clamp(Position.y, num8 / -2f, num8 / 2f);
                        LastMousePos = Event.current.mousePosition;
                    }
                    else if (Event.current.type == EventType.MouseUp)
                    {
                        Drag = false;
                    }
                    if (Event.current.type == EventType.ScrollWheel)
                    {
                        Zoom = Mathf.Clamp(Zoom + Event.current.delta.y / -20f, 1f, 3f);
                    }
                    GUIContent content = new GUIContent("https://theforestmap.com/ 지도데이터를 만들어주신분께 감사를 드립니다.");
                    GUIContent content2 = new GUIContent("https://cafe.naver.com/steamforest 네이버 더 포레스트 카페 방문하기");
                    Vector2 vector5 = GUI.skin.label.CalcSize(content);
                    GUI.color = Color.black;
                    GUI.Label(new Rect((float)Screen.width - 5f - vector5.x, (float)Screen.height - 25f, vector5.x + 10f, vector5.y + 10f), content);
                    GUI.Label(new Rect((float)Screen.width - 5f - vector5.x, (float)Screen.height - 45f, vector5.x + 10f, vector5.y + 10f), content2);
                    GUI.color = Color.white;
                    GUI.Label(new Rect((float)Screen.width - 6f - vector5.x, (float)Screen.height - 26f, vector5.x + 10f, vector5.y + 10f), content);
                    GUI.Label(new Rect((float)Screen.width - 6f - vector5.x, (float)Screen.height - 46f, vector5.x + 10f, vector5.y + 10f), content2);
                }
                if (currentMap.Loading)
                {
                    if (currentMap.Textures == null || currentMap.Textures.Length == 0)
                    {
                        string text = "불러오는중...";
                        Vector2 vector6 = GUI.skin.label.CalcSize(new GUIContent(text));
                        GUI.Label(new Rect((float)Screen.width / 2f - vector6.x / 2f, (float)Screen.height / 2f - vector6.y - 5f, vector6.x + 10f, vector6.y + 10f), text, WhiteLabel);
                        GUI.DrawTexture(new Rect((float)Screen.width / 4f, (float)Screen.height / 2f + 1f, (float)Screen.width / 2f * currentMap.Progress, 2f), foreground);
                        string text2 = currentMap.CurrentTask + ": " + Mathf.FloorToInt(currentMap.Progress * 100f) + "% (" + Mathf.FloorToInt((float)currentMap.BytesLoaded) + "kb / " + Mathf.FloorToInt((float)currentMap.BytesTotal / 1024f) + "kb)";
                        vector6 = GUI.skin.label.CalcSize(new GUIContent(text2));
                        GUI.Label(new Rect((float)Screen.width / 2f - vector6.x / 2f, (float)Screen.height / 2f + 2f, vector6.x + 10f, vector6.y + 10f), text2, WhiteLabel);
                        GUI.DrawTexture(new Rect((float)Screen.width / 4f, (float)Screen.height / 2f, (float)Screen.width / 2f, 1f), foreground);
                    }
                    else
                    {
                        string text3 = "불러오는중...";
                        Vector2 vector7 = GUI.skin.label.CalcSize(new GUIContent(text3));
                        GUI.Label(new Rect((float)Screen.width - 110f - vector7.x / 2f, (float)Screen.height - 40f - vector7.y - 5f, vector7.x + 10f, vector7.y + 10f), text3, WhiteLabel);
                        GUI.DrawTexture(new Rect((float)Screen.width - 210f, (float)Screen.height - 40f + 1f, 200f * currentMap.Progress, 2f), foreground);
                        string text4 = currentMap.CurrentTask + ": " + Mathf.FloorToInt(currentMap.Progress * 100f) + " % (" + Mathf.FloorToInt((float)currentMap.BytesLoaded) + "kb / " + Mathf.FloorToInt((float)currentMap.BytesTotal / 1024f) + "kb)";
                        vector7 = GUI.skin.label.CalcSize(new GUIContent(text4));
                        GUI.Label(new Rect((float)Screen.width - 110f - vector7.x / 2f, (float)Screen.height - 40f + 2f, vector7.x + 10f, vector7.y + 10f), text4, WhiteLabel);
                        GUI.DrawTexture(new Rect((float)Screen.width - 210f, (float)Screen.height - 40f, 200f, 1f), foreground);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Log.Write(ex.ToString());
        }
    }

    public void SaveMarkers(Dictionary<string, MarkerCategory> Categories, string path)
    {
        INIHelper iNIHelper = new INIHelper(path);
        foreach (MarkerCategory value in Categories.Values)
        {
            foreach (MarkerSetting marker in value.Markers)
            {
                iNIHelper.Write("Map", marker.Label, marker.Selected.ToString());
            }
        }
        iNIHelper.Write("Map", "LiveMarkers", livemarkers.ToString());
    }

    public float GetMarkerSize()
    {
        return 16f * Zoom;
    }

    private void Start()
    {
        try
        {
            loadSettings();
            Categories = new Dictionary<string, MarkerCategory>();
            foreach (MarkerSetting value in markerSettings.Values)
            {
                if (!Categories.ContainsKey(value.Category))
                {
                    Categories.Add(value.Category, new MarkerCategory());
                    Categories[value.Category].Color = value.Color;
                }
                Categories[value.Category].Markers.Add(value);
            }
            Markers = ModAPI.Resources.GetTexture("Markers.png", "Map");
            background = new Texture2D(2, 2);
            background.SetPixel(0, 0, new Color(0f, 0f, 0f, 0.7f));
            background.SetPixel(1, 0, new Color(0f, 0f, 0f, 0.7f));
            background.SetPixel(0, 1, new Color(0f, 0f, 0f, 0.7f));
            background.SetPixel(1, 1, new Color(0f, 0f, 0f, 0.7f));
            background.filterMode = FilterMode.Point;
            background.Apply();
            foreground = new Texture2D(2, 2);
            foreground.SetPixel(0, 0, new Color(1f, 1f, 1f, 1f));
            foreground.SetPixel(1, 0, new Color(1f, 1f, 1f, 1f));
            foreground.SetPixel(0, 1, new Color(1f, 1f, 1f, 1f));
            foreground.SetPixel(1, 1, new Color(1f, 1f, 1f, 1f));
            foreground.filterMode = FilterMode.Point;
            foreground.Apply();
            WhiteLabel = new GUIStyle(Interface.Skin.label);
            WhiteLabel.normal.textColor = Color.white;
            Overworld = new Map("https://theforestmap.com/map/map-4096.jpg", "https://theforestmap.com/map/md5.php?map=forest", "https://theforestmap.com/inc/api/?json&map=forest", "Mods/Map/Cache/Overworld/map.jpg");
            Underworld = new Map("https://theforestmap.com/map/cave-4096.jpg", "https://theforestmap.com/map/md5.php?map=cave", "https://theforestmap.com/inc/api/?json&map=cave", "Mods/Map/Cache/Underworld/map.jpg");
        }
        catch (Exception ex)
        {
            Log.Write(ex.ToString());
        }
    }

    private void Update()
    {
        if (TheForest.Utils.Input.GetButtonDown("Esc"))
        {
            Opened = false;
        }
        try
        {
            Overworld.Update();
            Underworld.Update();
            if (ModAPI.Input.GetButtonDown("OpenMap") && !ChatBox.IsChatOpen && LocalPlayer.Inventory.CurrentView != PlayerInventory.PlayerViews.Pause)
            {
                if (LocalPlayer.IsInCaves)
                {
                    currentMap = Underworld;
                }
                else
                {
                    currentMap = Overworld;
                }
                Opened = !Opened;
                ShowPhase = 0f;
                if (Opened)
                {
                    if (!LocalPlayer.Inventory.IsRightHandEmpty())
                    {
                        if (!LocalPlayer.Inventory.RightHand.IsHeldOnly)
                        {
                            LocalPlayer.Inventory.MemorizeItem(Item.EquipmentSlot.RightHand);
                        }
                        LocalPlayer.Inventory.StashEquipedWeapon(false);
                    }
                    LocalPlayer.Inventory.StashLeftHand();
                }
            }
            if (Opened)
            {
                if (currentMap.TexturesLoaded && ShowPhase < 1f)
                {
                    ShowPhase += Time.unscaledDeltaTime;
                }
                LocalPlayer.FpCharacter.LockView(true);
            }
            else if (visible)
            {
                LocalPlayer.FpCharacter.UnLockView();
            }
            visible = Opened;
        }
        catch (Exception ex)
        {
            Log.Write(ex.ToString());
        }
    }

    private void readIni(string path)
    {
        INIHelper iNIHelper = new INIHelper(path);
        foreach (MarkerSetting value in markerSettings.Values)
        {
            try
            {
                value.Selected = Convert.ToBoolean(iNIHelper.Read("Map", value.Label));
            }
            catch (Exception)
            {
                value.Selected = true;
            }
        }
        try
        {
            livemarkers = Convert.ToBoolean(iNIHelper.Read("Map", "LiveMarkers"));
        }
        catch (Exception)
        {
            livemarkers = true;
        }
    }

    private void loadSettings()
    {
        if (File.Exists("Mods/Map.settings"))
        {
            readIni("Mods/Map.settings");
        }
    }
}
