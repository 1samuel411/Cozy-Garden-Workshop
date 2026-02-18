using UnityEngine;

public static class TransformUtils
{
    
    public static void LookAtPoint(this Transform transform, Vector3 transformPosition)
    {
        Vector3 lookPos = new Vector3(transformPosition.x, transform.position.y, transformPosition.z);
        transform.LookAt(lookPos);
    }
}
