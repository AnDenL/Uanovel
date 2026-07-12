using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Dialog start;
    public GameObject optionPrefab;

    private Transform options;
    private Dialog current;
    private TextMeshProUGUI textmp;
    private TextEffects effects;
    private Coroutine coroutine;
    private bool isPlaying;
    private bool canClick;

    private void Start()
    {
        textmp = GetComponentInChildren<TextMeshProUGUI>();
        effects = textmp.GetComponent<TextEffects>();
        options = transform.GetChild(1);

        Show(start);
    }

    public void Click()
    {
        if (!canClick) return;
        if (isPlaying) Skip();
        else if (current.next.Length < 1) return;
        else if (string.IsNullOrWhiteSpace(current.next[0].name)) Show(current.next[0].Value);
        else
        {
            canClick = false;
            textmp.enabled = false;
            foreach (Transform op in options)
                Destroy(op.gameObject);
            
            for (int i = 0; i < current.next.Length; i++)
                SetOption(Instantiate(optionPrefab, options), i);
        }
    }

    private void SetOption(GameObject obj, int i)
    {
        obj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = current.next[i].name;
        obj.GetComponent<Button>().onClick.AddListener(() => Show(current.next[i].Value));
    }

    private void Show(Dialog dialog)
    {
        History.Add(dialog);

        foreach (Transform op in options)
            Destroy(op.gameObject);
        
        current = dialog;
        effects.GetTags(dialog);
        textmp.fontSize = dialog.fontSize;
        textmp.enabled = true;
        canClick = true;

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
