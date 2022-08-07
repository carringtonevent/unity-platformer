using UnityEngine;

public static class Extensions
{
    public static void SetX(this Vector2 vector, float number) {
        vector.Set(number, vector.y);
    }

    public static void SetY(this Vector2 vector, float number) {
        vector.Set(vector.x, number);
    }
}
