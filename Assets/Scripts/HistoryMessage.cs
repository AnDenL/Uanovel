using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HistoryMessage : MonoBehaviour
{
    public void Set(Dialog dialog)
    {
        GetComponent<TextMeshProUGUI>().text = dialog.clearText;
        transform.GetChild(1).GetComponent<Image>().sprite = dialog.icon;
    }
}
