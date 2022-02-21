public static class PNConvert
{
    public static string ToFabPath(string name)
    {
        return string.Concat("Prefabs/UI/Panel/", name);
    }
    public static string ToName(string fabPath)
    {
        return fabPath.Substring(fabPath.LastIndexOf('/') + 1);
    }
}



