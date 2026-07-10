using UnityEngine;
using TMPro;
using System.Collections.Generic;

using Random = UnityEngine.Random;

[RequireComponent(typeof(TMP_Text))]
public class TextEffects : MonoBehaviour
{
    private (int,Effect)[] effects;
    private int effectIndex;

    private TMP_Text textMesh;
    private Mesh mesh;
    private Vector3[] vertices;

    private Effect current = Effect.None;

    private void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    public void GetTags(Dialog dialog)
    {
        current = Effect.None;
        var text = new string(dialog.text);
        effectIndex = 0;

        int offset = 0;

        List<(int,Effect)> effects = new();

        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '/')
            {
                switch (text[i+1])
                {
                    case '0':
                        effects.Add((i - offset, Effect.None));
                        break;
                    case '1':
                        effects.Add((i - offset, Effect.Wobble));
                        break;
                    case '2':
                        effects.Add((i - offset, Effect.Wave));
                        break;
                    case '3':
                        effects.Add((i - offset, Effect.Shake));
                        break;
                }
                text = text.Remove(i,2);
                //offset += 2;
                i++;
            }
        }

        effects.Add((text.Length + 1, Effect.None));

        foreach (var item in effects)
        {
            print($"{item.Item1} {item.Item2}");
        }
        
        this.effects = effects.ToArray();
        dialog.clearText = text;
    }

    private void Update()
    {
        if (string.IsNullOrWhiteSpace(textMesh.text)) return;
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        effectIndex = 0;
        current = Effect.None;

        for (int i = 0; i < textMesh.textInfo.characterCount; i++)
        {
            TMP_CharacterInfo c = textMesh.textInfo.characterInfo[i];

            int index = c.vertexIndex;

            if (effects[effectIndex].Item1 == i)
            {
                current = effects[effectIndex].Item2;
                effectIndex++;
            }

            switch (current) {
                case Effect.Wobble: 
                    Wobble(Time.time + i, index);
                    break;
                case Effect.Wave: 
                    Wave(Time.time * 3 + i, index);
                    break;
                case Effect.Shake: 
                    Shake(index);
                    break;
            }
        }

        mesh.vertices = vertices;
        textMesh.canvasRenderer.SetMesh(mesh);
    }

    private void Wobble(float time, int i) {
        Vector3 offset = new Vector2(Mathf.Sin(time*3.3f), Mathf.Cos(time*2.5f))* 5;
        vertices[i] += offset;
        vertices[i + 1] += offset;
        vertices[i + 2] += offset;
        vertices[i + 3] += offset;
    }

    private void Wave(float time, int i) {
        Vector3 offset = new Vector2(0, 2 * Mathf.Sin(time/2))* 5;
        vertices[i] += offset;
        vertices[i + 1] += offset;
        vertices[i + 2] += offset;
        vertices[i + 3] += offset;
    }

    private void Shake(int i) {
        Vector3 offset = Random.insideUnitCircle * 5;
        vertices[i] += offset;
        vertices[i + 1] += offset;
        vertices[i + 2] += offset;
        vertices[i + 3] += offset;
    }
}

enum Effect
{
    None,    //0
    Wobble,  //1
    Wave,    //2
    Shake,   //3
}