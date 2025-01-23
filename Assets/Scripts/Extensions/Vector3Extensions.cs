using UnityEngine;

public static class Vector3Extensions
{
    /*
     *  x, y and z are being set to null if there is no input for them
     * 
     * 
     * new vector3 is seeing if the x, y or z are null, if they are then use origianl x, y or z
     * 
     */
    public static Vector3 With(this Vector3 origianl, float? x = null, float? y = null, float? z = null)
    {
        return new Vector3(x ?? origianl.x, y ?? origianl.y, z ?? origianl.z);
    }

    public static Vector3 Flat(this Vector3 original)
    {
        return new Vector3(original.x, 0f, original.z);
    }

    public static Vector3 DirectionTo(this Vector3 source, Vector3 destination)
    {
        return destination - source;
    }

    public static Vector3 DirectionToNormalize(this Vector3 source, Vector3 destination)
    {
        return Vector3.Normalize(destination - source);
    }

}
