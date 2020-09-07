// Map.Map
using LitJson;
using ModAPI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Map
{
    public class Marker
    {
        public IngameMap.MarkerSetting Class;

        public string Description = "";

        public Vector3 WorldPosition;

        public Vector2 MapPosition;

        public Marker()
        {
        }

        public Marker(JsonData node, IngameMap.MarkerSetting setting)
        {
            Class = setting;
            Description = (string)node["description"];
            float x = float.Parse((string)node["x"]);
            float y = 0f;
            if ((string)node["z"] != "")
            {
                y = float.Parse((string)node["z"]);
            }
            float z = float.Parse((string)node["y"]);
            WorldPosition = new Vector3(x, y, z);
        }
    }

    public Texture2D Texture;

    public Texture2D[] Textures;

    public const int SPLIT = 8;

    protected Downloader Downloader;

    protected string url = "";

    protected string filename = "";

    protected bool downloadParsed;

    protected byte[] loadedHash = new byte[0];

    protected byte[] serverHash = new byte[0];

    protected Downloader HashDownloader;

    protected Downloader MarkerDownloader;

    public bool TexturesLoaded;

    protected bool markerParsed;

    public List<Marker> Markers = new List<Marker>();

    protected int CurrentTexture = -1;

    public float Progress
    {
        get
        {
            if (HashDownloader.Loading && !HashDownloader.Finished)
            {
                if (HashDownloader.BytesTotal == 0)
                {
                    return 0f;
                }
                return (float)HashDownloader.BytesLoaded / (float)HashDownloader.BytesTotal;
            }
            if (Downloader.Loading && !Downloader.Finished)
            {
                if (Downloader.BytesTotal == 0)
                {
                    return 0f;
                }
                return (float)Downloader.BytesLoaded / (float)Downloader.BytesTotal;
            }
            if (MarkerDownloader.Loading && !MarkerDownloader.Finished)
            {
                if (MarkerDownloader.BytesTotal == 0)
                {
                    return 0f;
                }
                return (float)MarkerDownloader.BytesLoaded / (float)MarkerDownloader.BytesTotal;
            }
            if (!TexturesLoaded)
            {
                return (float)CurrentTexture / 64f;
            }
            return 1f;
        }
    }

    public bool Loading
    {
        get
        {
            if ((Downloader == null || Downloader.Finished || !Downloader.Loading) && (HashDownloader == null || HashDownloader.Finished || !HashDownloader.Loading) && (MarkerDownloader == null || MarkerDownloader.Finished || !MarkerDownloader.Loading))
            {
                return !TexturesLoaded;
            }
            return true;
        }
    }

    public int BytesLoaded
    {
        get
        {
            if (HashDownloader != null && HashDownloader.Loading && !HashDownloader.Finished)
            {
                return HashDownloader.BytesLoaded;
            }
            if (Downloader != null && Downloader.Loading && !Downloader.Finished)
            {
                return Downloader.BytesLoaded;
            }
            if (MarkerDownloader != null && MarkerDownloader.Loading && !MarkerDownloader.Finished)
            {
                return MarkerDownloader.BytesLoaded;
            }
            if (!TexturesLoaded)
            {
                return CurrentTexture;
            }
            return 0;
        }
    }

    public int BytesTotal
    {
        get
        {
            if (HashDownloader != null && HashDownloader.Loading && !HashDownloader.Finished)
            {
                return HashDownloader.BytesTotal;
            }
            if (Downloader != null && Downloader.Loading && !Downloader.Finished)
            {
                return Downloader.BytesTotal;
            }
            if (MarkerDownloader != null && MarkerDownloader.Loading && !MarkerDownloader.Finished)
            {
                return MarkerDownloader.BytesTotal;
            }
            if (!TexturesLoaded)
            {
                return 64;
            }
            return 0;
        }
    }

    public string CurrentTask
    {
        get
        {
            if (HashDownloader != null && HashDownloader.Loading && !HashDownloader.Finished)
            {
                return "Downloading hash";
            }
            if (Downloader != null && Downloader.Loading && !Downloader.Finished)
            {
                return "Downloading map";
            }
            if (MarkerDownloader != null && MarkerDownloader.Loading && !MarkerDownloader.Finished)
            {
                return "Downloading marker";
            }
            if (!TexturesLoaded)
            {
                return "Loading textures";
            }
            return "Completed";
        }
    }

    public static byte[] ConvertHexStringToByteArray(string hexString)
    {
        byte[] array = new byte[hexString.Length / 2];
        for (int i = 0; i < array.Length; i++)
        {
            string s = hexString.Substring(i * 2, 2);
            array[i] = byte.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
        }
        return array;
    }

    public Map(string url, string hashURL, string markerURL, string filename)
    {
        Textures = new Texture2D[64];
        this.filename = filename;
        this.url = url;
        HashDownloader = new Downloader(hashURL);
        Downloader = new Downloader(url);
        MarkerDownloader = new Downloader(markerURL, startInstant: true);
        if (File.Exists(filename))
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            loadedHash = mD5CryptoServiceProvider.ComputeHash(File.ReadAllBytes(filename));
            HashDownloader.StartDownload();
        }
        else
        {
            Downloader.StartDownload();
        }
    }

    public void FreeTextures()
    {
        if (Textures == null)
        {
            return;
        }
            for (int i = 0; i < Textures.Length; i++)
            {
                if (Textures[i] != null)
                {
                    UnityEngine.Object.Destroy(Textures[i]);
                }
            }
    }

    public void ParseTextures()
    {
        FreeTextures();
        string directoryName = Path.GetDirectoryName(filename);
        if (!Directory.Exists(directoryName))
        {
            Directory.CreateDirectory(directoryName);
        }
        Texture2D texture2D = new Texture2D(2, 2, TextureFormat.RGB24, mipmap: false, linear: false);
        texture2D.LoadImage(File.ReadAllBytes(filename));
        int num = texture2D.width / 8;
        int num2 = texture2D.height / 8;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Texture2D texture2D2 = new Texture2D(num, num2, TextureFormat.RGB24, mipmap: false, linear: true);
                texture2D2.SetPixels(texture2D.GetPixels(i * num, j * num2, num, num2));
                texture2D2.Apply();
                int num3 = i + j * 8;
                File.WriteAllBytes(directoryName + Path.DirectorySeparatorChar.ToString() + num3 + ".png", texture2D2.EncodeToPNG());
                Textures[num3] = texture2D2;
            }
        }
        CurrentTexture = -1;
        TexturesLoaded = true;
        UnityEngine.Object.Destroy(texture2D);
    }

    private void LoadTexture()
    {
        string path = Path.GetDirectoryName(filename) + Path.DirectorySeparatorChar.ToString() + CurrentTexture + ".png";
        if (!File.Exists(path))
        {
            ParseTextures();
        }
        if (File.Exists(path))
        {
            Textures[CurrentTexture] = new Texture2D(2, 2, TextureFormat.RGB24, mipmap: false, linear: true);
            Textures[CurrentTexture].LoadImage(File.ReadAllBytes(path));
        }
        else
        {
            CurrentTexture = -1;
        }
        CurrentTexture++;
        if (CurrentTexture >= 64)
        {
            CurrentTexture = -1;
            TexturesLoaded = true;
        }
    }

    public void Update()
    {
        try
        {
            if (MarkerDownloader != null && MarkerDownloader.Finished)
            {
                JsonData jsonData = JsonMapper.ToObject(Encoding.UTF8.GetString(MarkerDownloader.Data));
                for (int i = 0; i < jsonData["markers"].Count; i++)
                {
                    string key = jsonData["markers"][i]["name"].ToString().Replace("\"", "");
                    if (IngameMap.markerSettings.ContainsKey(key))
                    {
                        Markers.Add(new Marker(jsonData["markers"][i], IngameMap.markerSettings[key]));
                    }
                }
                MarkerDownloader = null;
            }
            if (HashDownloader != null && HashDownloader.Finished)
            {
                string @string = Encoding.UTF8.GetString(HashDownloader.Data);
                serverHash = ConvertHexStringToByteArray(@string);
                bool flag = true;
                for (int j = 0; j < loadedHash.Length; j++)
                {
                    if (loadedHash[j] != serverHash[j])
                    {
                        flag = false;
                        Downloader.StartDownload();
                        break;
                    }
                }
                if (flag)
                {
                    CurrentTexture = 0;
                }
                HashDownloader = null;
            }
            if (CurrentTexture >= 0)
            {
                LoadTexture();
            }
            if (Downloader.Finished && !downloadParsed)
            {
                string directoryName = Path.GetDirectoryName(filename);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                File.WriteAllBytes(filename, Downloader.Data);
                ParseTextures();
                downloadParsed = true;
            }
        }
        catch (Exception ex)
        {
            Log.Write(ex.ToString());
        }
    }
}
