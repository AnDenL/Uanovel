using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty dialog", menuName = "Dialog", order = -4000)]
public class Dialog : ScriptableObject
{
    public string text;
    public float speed = 20;

    public Option<Dialog>[] next;
}

[Serializable]
public class Option<T>
{
    public string name;
    public T Value;
}