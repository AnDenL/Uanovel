using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public Dialog start;

    private Dialog current;
    private TextMeshProUGUI textmp;
    private Coroutine coroutine;
    private bool isPlaying;

    private void Start()
    {
        textmp = GetComponentInChildren<TextMeshProUGUI>();

        coroutine = StartCoroutine(Show(start));
    }

    public void Click()
    {
        if (isPlaying) Skip();
        else if (current.next.Length > 0) coroutine = StartCoroutine(Show(current.next[0].Value));
    }

    private IEnumerator Show(Dialog dialog)
    {
        History.Add(dialog);
        textmp.text = " ";
        float speed = 1 / dialog.speed;

        current = dialog;

        int i = 0;

        isPlaying = true;

        while (i < dialog.text.Length)
        {
            yield return new WaitForSeconds(speed);
            textmp.text += dialog.text[i];
            i++;
        }

        isPlaying = false;
    }

    private void Skip()
    {
        if (coroutine == null) return;

        textmp.text = current.text;
        isPlaying = false;
        StopCoroutine(coroutine);
    }
}
