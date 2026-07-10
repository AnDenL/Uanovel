using UnityEngine;

public class History : MonoBehaviour
{
    public static History instance;

    public GameObject prefab;
    public Transform point;

    private void Awake()
    {
        instance = this;
    }

    public static void Add(Dialog dialog)
    {
        Instantiate(instance.prefab, instance.point);
    }
}
