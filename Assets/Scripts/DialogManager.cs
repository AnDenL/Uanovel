using System.Collections;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public Dialog start;

    private Dialog current;
    private TextMeshProUGUI textmp;
    private TextEffects effects;
    private Coroutine coroutine;
    private bool isPlaying;

    private void Start()
    {
        textmp = GetComponentInChildren<TextMeshProUGUI>();
        effects = textmp.GetComponent<TextEffects>();

        Show(start);
    }

    public void Click()
    {
        if (isPlaying) Skip();
        else if (current.next.Length > 0) Show(current.next[0].Value);
    }

    private void Show(Dialog dialog)
    {
        History.Add(dialog);
        
        current = dialog;
        effects.GetTags(dialog);

        coroutine = StartCoroutine(ShowCoroutine(dialog.clearText, 1 / dialog.speed));
    }

    private IEnumerator ShowCoroutine(string Text, float speed)
    {
        textmp.text = "";

        int i = 0;

        isPlaying = true;

        while (i < Text.Length)
        {
            yield return new WaitForSeconds(speed);
            textmp.text += Text[i];
            i++;
        }

        isPlaying = false;
    }

    private void Skip()
    {
        if (coroutine == null) return;

        textmp.text = current.clearText;
        isPlaying = false;
        StopCoroutine(coroutine);
    }
}
