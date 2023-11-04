using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class TextSync : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI display;
    private int _maxCharacters = 5000;
    private string _cutOffText = "<i><color=#87cefa><u>(Click here for full content)</u></color></i>";
    private Coroutine _writeRoutine;

    public void ReadStringInput(string input)
    {
        // Using a coroutine, because the text input field sends the event for each character when text is pasted
        if (_writeRoutine != null)
            StopCoroutine(_writeRoutine);
        _writeRoutine = StartCoroutine(WriteRoutine(input));
    }

    private IEnumerator WriteRoutine(string input)
    {
        var text = string.Empty;
        // Anything over 5000 characters will be cut off
        if (input.Length <= _maxCharacters)
        {
            text = input;
        }
        else
        {
            yield return new WaitForSecondsRealtime(0.05f);
            var lines = input.Split('\n').ToList();
            foreach (var line in lines)
            {
                if (line.Length + text.Length > _maxCharacters)
                {
                    text += "...\n";
                    text += _cutOffText;
                    break;
                }

                text += line + "\n";
            }
        }

        display.text = text;
    }
}