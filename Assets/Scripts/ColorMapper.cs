public class ColorMapper
{
    public static UnityEngine.Color Map(TileColor tileColor)
    {
        if (tileColor == TileColor.Blue)
        {
            return UnityEngine.Color.blue;
        }
        else if (tileColor == TileColor.Red)
        {
            return UnityEngine.Color.red;
        }
        else if (tileColor == TileColor.Yellow)
        {
            return UnityEngine.Color.yellow;
        }
        else if (tileColor == TileColor.Green)
        {
            return UnityEngine.Color.green;
        }

        return UnityEngine.Color.white;
    }
}
