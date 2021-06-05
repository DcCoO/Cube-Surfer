using UnityEngine;
public static class ExtensionMethods
{
    public static Vector3 Ground(this Vector3 v3)
    {
        v3.y = 0;
        return v3;
    }

    public static Vector3 SetX(this Vector3 v3, float x)
    {
        v3.x = x;
        return v3;
    }
    public static Vector3 SetY(this Vector3 v3, float y)
    {
        v3.y = y;
        return v3;
    }
}