// Map.INIHelper
using System.Runtime.InteropServices;
using System.Text;

internal class INIHelper : IngameMap
{
    private string filePath;

    public string FilePath
    {
        get
        {
            return filePath;
        }
        set
        {
            filePath = value;
        }
    }

    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

    [DllImport("kernel32")]
    private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    public INIHelper(string filePath)
    {
        this.filePath = filePath;
    }

    public void Write(string section, string key, string value)
    {
        WritePrivateProfileString(section, key, value.ToLower(), filePath);
    }

    public string Read(string section, string key)
    {
        StringBuilder stringBuilder = new StringBuilder(255);
        GetPrivateProfileString(section, key, "", stringBuilder, 255, filePath);
        return stringBuilder.ToString();
    }
}
