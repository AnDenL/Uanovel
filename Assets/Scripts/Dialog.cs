using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Empty dialog", menuName = "Dialog", order = -4000)]
public class Dialog : ScriptableObject
{
    public string text;
    public string clearText;
    public float speed = 20;
    public float fontSize = 36;
    public Sprite icon;

    public Option[] next;
}

[Serializable]
public class Option
{
    public string name;
    public Dialog Value;
}