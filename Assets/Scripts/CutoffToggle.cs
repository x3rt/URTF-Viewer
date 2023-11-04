using TMPro;
using UnityEngine;

public class CutoffToggle : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    public bool enableCutoff = true;
    private string _lastText = string.Empty;
    public static CutoffToggle Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        SetColor();
    }

    private void SetColor()
    {
        buttonText.color = enableCutoff ? Color.green : Color.red;
    }

    public void SetLastText(string text)
    {
        _lastText = text;
    }

    public void ToggleCutoff()
    {
        enableCutoff = !enableCutoff;
        TextSync.Instance.ReadStringInput(_lastText);
    }
}