using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class UndoRedoInput : MonoBehaviour
{
    [SerializeField] public TMP_InputField inputField;
    private static int _maxHistory = 100;
    private readonly List<string> _textHistory = new(_maxHistory);
    private int _historyIndex = -1;
    private Coroutine _saveRoutine;

    private void Start()
    {
        if (inputField != null)
        {
            _textHistory.Add(inputField.text);
        }

        _historyIndex = _textHistory.Count - 1;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Undo();
            }
            else if (Input.GetKeyDown(KeyCode.Y))
            {
                Redo();
            }
        }

        // Re-Highlight the text input field if it's not selected
        if (inputField != null && !inputField.isFocused)
        {
            inputField.ActivateInputField();
        }
    }

    public void OnInputValueChanged()
    {
        if (_saveRoutine != null)
            StopCoroutine(_saveRoutine);
        _saveRoutine = StartCoroutine(SaveRoutine());
    }

    private IEnumerator SaveRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        Save();
    }

    private void Save()
    {
        var newText = inputField.text;
        if (newText != GetCurrentText())
        {
            if (_historyIndex < _textHistory.Count - 1)
            {
                _textHistory.RemoveRange(_historyIndex + 1, _textHistory.Count - _historyIndex - 1);
            }

            if (_textHistory.Count >= _maxHistory)
            {
                _textHistory.RemoveAt(0);
                _historyIndex--;
            }

            _textHistory.Add(newText);
            _historyIndex++;
        }
    }

    public void Undo()
    {
        if (_historyIndex > 0)
        {
            _historyIndex--;
            inputField.text = _textHistory[_historyIndex];
        }
    }

    public void Redo()
    {
        if (_historyIndex < _textHistory.Count - 1)
        {
            _historyIndex++;
            inputField.text = _textHistory[_historyIndex];
        }
    }

    private string GetCurrentText()
    {
        if (_historyIndex >= 0 && _historyIndex < _textHistory.Count)
        {
            return _textHistory[_historyIndex];
        }

        return inputField.text;
    }
}