using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Map
{
    public class Map
    {
        public Texture2D Texture;
        public Texture2D[] Textures;
        public const int SPLIT = 8;
        protected Downloader Downloader;
        protected string url = "";
        protected string filename = "";
        protected bool downloadParsed = false;
        protected byte[] loadedHash = new byte[0];
        protected byte[] serverHash = new byte[0];
        protected Downloader HashDownloader;
        protected Downloader MarkerDownloader;
        public bool TexturesLoaded = false;
        protected bool markerParsed = false;

        public class Marker
        {
            public IngameMap.MarkerSetting Class;
            public string Description = "";
            public Vector3 WorldPosition;
            public Vector2 MapPosition;

            public Marker()
            {

            }
            public Marker(LitJson.JsonData node, IngameMap.MarkerSetting setting)
            {
                Class = setting;
                Description = (string)node["description"];
                float x = float.Parse((string)node["x"]);
                float y = 0f;
                if (((string)node["z"]) != "")
                    y = float.Parse((string)node["z"]);
                float z = float.Parse((string)node["y"]);
                WorldPosition = new Vector3(x, y, z);
            }
        }

        public List<Marker> Markers = new List<Marker>();

        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            byte[] HexAsBytes = new byte[hexString.Length / 2];
            for (int index = 0; index < HexAsBytes.Length; index++)
            {
                string byteValue = hexString.Substring(index * 2, 2);
                HexAsBytes[index] = byte.Parse(byteValue, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
            }
            return HexAsBytes;
        }

        public float Progress
        {
            get
            {
                if (HashDownloader.Loading && !HashDownloader.Finished)
                {
                    if (HashDownloader.BytesTotal == 0)
                        return 0f;
                    return (float)HashDownloader.BytesLoaded / (float)HashDownloader.BytesTotal;
                }
                else if (Downloader.Loading && !Downloader.Finished)
                {
                    if (Downloader.BytesTotal == 0)
                        return 0f;
                    return (float)Downloader.BytesLoaded / (float)Downloader.BytesTotal;
                }
                else if (MarkerDownloader.Loading && !MarkerDownloader.Finished)
                {
                    if (MarkerDownloader.BytesTotal == 0)
                        return 0f;
                    return (float)MarkerDownloader.BytesLoaded / (float)MarkerDownloader.BytesTotal;
                }
                else if (!TexturesLoaded)
                {
                    return (float)CurrentTexture / (float)(SPLIT * SPLIT);
                }
                return 1f;
            }
        }

        public bool Loading
        {
            get
            {
                return (Downloader != null && !Downloader.Finished && Downloader.Loading) || (HashDownloader != null && !HashDownloader.Finished && HashDownloader.Loading) || (MarkerDownloader != null && !MarkerDownloader.Finished && MarkerDownloader.Loading) || !TexturesLoaded;
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
                else if (Downloader != null && Downloader.Loading && !Downloader.Finished)
                {
                    return Downloader.BytesLoaded;
                }
                else if (MarkerDownloader != null && MarkerDownloader.Loading && !MarkerDownloader.Finished)
                {
                    return MarkerDownloader.BytesLoaded;
                }
                else if (!TexturesLoaded)
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
                else if (Downloader != null && Downloader.Loading && !Downloader.Finished)
                {
                    return Downloader.BytesTotal;
                }
                else if (MarkerDownloader != null && MarkerDownloader.Loading && !MarkerDownloader.Finished)
                {
                    return MarkerDownloader.BytesTotal;
                }
                else if (!TexturesLoaded)
                {
                    return SPLIT * SPLIT;
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
                else if (Downloader != null && Downloader.Loading && !Downloader.Finished)
                {
                    return "Downloading map";
                }
                else if (MarkerDownloader != null && MarkerDownloader.Loading && !MarkerDownloader.Finished)
                {
                    return "Downloading marker";
                }
                else if (!TexturesLoaded)
                {
                    return "Loading textures";
                }
                return "Completed";
            }
        }

        public Map(string url, string hashURL, string markerURL, string filename)
        {
            Textures = new Texture2D[SPLIT * SPLIT];
            this.filename = filename;
            this.url = url;
            this.HashDownloader = new Downloader(hashURL, false);
            this.Downloader = new Downloader(url, false);
            this.MarkerDownloader = new Downloader(markerURL, true);
            if (System.IO.File.Exists(filename))
            {
                System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                loadedHash = md5.ComputeHash(System.IO.File.ReadAllBytes(filename));
                this.HashDownloader.StartDownload();
            }
            else
            {
                this.Downloader.StartDownload();
            }
        }

        protected int CurrentTexture = -1;

        public void FreeTextures()
        {
            if (Textures != null)
                for (int i = 0; i < Textures.Length; i++)
                    if (Textures[i] != null)
                        UnityEngine.Object.Destroy(Textures[i]);
        }

        public void ParseTextures()
        {
            FreeTextures();
            string directoryName = System.IO.Path.GetDirectoryName(filename);
            if (!System.IO.Directory.Exists(directoryName))
                System.IO.Directory.CreateDirectory(directoryName);

            Texture2D whole = new Texture2D(2, 2, TextureFormat.RGB24, false, false);
            whole.LoadImage(System.IO.File.ReadAllBytes(filename));

            int widthPer = whole.width / SPLIT;
            int heightPer = whole.height / SPLIT;
            for (int x = 0; x < SPLIT; x++)
            {
                for (int y = 0; y < SPLIT; y++)
                {
                    // splitting the texture for better distributed loading performance
                    Texture2D n = new Texture2D(widthPer, heightPer, TextureFormat.RGB24, false, true);
                    n.SetPixels(whole.GetPixels(x * widthPer, y * heightPer, widthPer, heightPer));
                    n.Apply();
                    int index = x + y * SPLIT;
                    System.IO.File.WriteAllBytes(directoryName + System.IO.Path.DirectorySeparatorChar + index + ".png", n.EncodeToPNG());
                    Textures[index] = n;
                }
            }
            CurrentTexture = -1;
            TexturesLoaded = true;
            UnityEngine.Object.Destroy(whole);
        }

        void LoadTexture()
        {
            string fileName = System.IO.Path.GetDirectoryName(filename) + System.IO.Path.DirectorySeparatorChar + CurrentTexture + ".png";
            if (!System.IO.File.Exists(fileName))
                ParseTextures();
            if (System.IO.File.Exists(fileName))
            {
                Textures[CurrentTexture] = new Texture2D(2, 2, TextureFormat.RGB24, false, true);
                Textures[CurrentTexture].LoadImage(System.IO.File.ReadAllBytes(fileName));
            }
            else
            {
                CurrentTexture = -1;
                // something went terribly wrong :(
            }
            CurrentTexture++;
            if (CurrentTexture >= SPLIT * SPLIT)
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
                    string text = System.Text.UTF8Encoding.UTF8.GetString(MarkerDownloader.Data);
                    LitJson.JsonData node = LitJson.JsonMapper.ToObject(text);
                    for (int i = 0; i < node["markers"].Count; i++)
                    {
                        string n = node["markers"][i]["name"].ToString().Replace("\"", "");
                        if (IngameMap.markerSettings.ContainsKey(n))
                        {
                            Markers.Add(new Marker(node["markers"][i], IngameMap.markerSettings[n]));
                        }
                    }
                    MarkerDownloader = null;
                }
                if (HashDownloader != null && HashDownloader.Finished)
                {
                    string text = System.Text.UTF8Encoding.UTF8.GetString(HashDownloader.Data);
                    serverHash = ConvertHexStringToByteArray(text);
                    bool identical = true;
                    for (int i = 0; i < loadedHash.Length; i++)
                    {
                        if (loadedHash[i] != serverHash[i])
                        {
                            identical = false;
                            this.Downloader.StartDownload();
                            break;
                        }
                    }
                    if (identical)
                    {
                        CurrentTexture = 0;
                    }
                    HashDownloader = null;
                }
                // load textures distributed over frames
                if (CurrentTexture >= 0)
                {
                    LoadTexture();
                }

                if (this.Downloader.Finished && !downloadParsed)
                {
                    string directoryName = System.IO.Path.GetDirectoryName(filename);
                    if (!System.IO.Directory.Exists(directoryName))
                        System.IO.Directory.CreateDirectory(directoryName);
                    System.IO.File.WriteAllBytes(filename, Downloader.Data);
                    ParseTextures();
                    downloadParsed = true;
                }
            } catch (Exception e)
            {
                ModAPI.Log.Write(e.ToString());
            }
        }
    }
}
