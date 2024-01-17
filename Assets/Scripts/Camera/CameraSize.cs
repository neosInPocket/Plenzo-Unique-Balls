using UnityEngine;

public static class CameraSize
{
    public static Vector2 GetSize()
    {
        return ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    public static Vector3 ScreenToWorldPoint(Vector2 screenPosition)
    {
        var objective = Camera.main.ScreenPointToRay(screenPosition);

        var direction = objective.direction;
        var origin = objective.origin;

        Vector3 normal = new Vector3(0, 0, 1);
        Vector3 point = new Vector3(0, 0, 0);

        float product = Vector3.Dot(direction, normal);

        float magnitude = Vector3.Dot(point - origin, normal) / product;

        Vector3 result = origin + magnitude * direction;
        return result;
    }
}
