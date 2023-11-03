using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextSync : MonoBehaviour
{
    // TMP Text UI
    
    [SerializeField]
    TextMeshProUGUI display;
    
    public void ReadStringInput(string s)
    {
        display.text = s;
    }
    
    
}
