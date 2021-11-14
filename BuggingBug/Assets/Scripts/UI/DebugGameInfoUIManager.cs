using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugGameInfoUIManager : MonoBehaviour
{
    HorizontalLayoutGroup[] info;

    Dictionary<string, Text> content = new Dictionary<string, Text>();
    private void Awake()
    {
        info = GetComponentsInChildren<HorizontalLayoutGroup>();
        for (int i = 0; i < info.Length; i++)
        {
            Text[] labelVal = info[i].GetComponentsInChildren<Text>();
            Text val = labelVal[1];
            val.text = "";
            content.Add(info[i].gameObject.name, val);
        }
    }

    private void Start()
    {
        UIEvents.uiEvents.OnUpdateDebugUIElement_Float += UpdateDebugUIElement;
        UIEvents.uiEvents.OnUpdateDebugUIElement_String += UpdateDebugUIElement_String;
        UIEvents.uiEvents.OnUpdateDebugUIElement_PerFrame += UpdateDebugUIElement_PerFrame;
    }

    private void UpdateDebugUIElement_PerFrame(string id, float val)
    {
        content[id].text = val.ToString("F1");
    }

    public void UpdateDebugUIElement(string id, float val)
    {
        content[id].text = val.ToString("F1");
    }

    public void UpdateDebugUIElement_String(string id, string val)
    {
        content[id].text = val;
    }
}
