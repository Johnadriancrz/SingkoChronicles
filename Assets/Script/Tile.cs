using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    [System.Serializable]
    public class TileState
    {
        public Color fillColor;
        public Color outlineColor;
    }

    public TileState State { get; private set; }
    public char letter { get; private set; }

    private TextMeshProUGUI text;
    private Image fill;
    private Outline outline;

    private void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        fill = GetComponent<Image>();
        outline = GetComponent<Outline>();
    }

    public void SetLetter(char letter)
    {
        this.letter = letter;
        text.text = letter.ToString();
    }

    public void SetState(TileState state)
    {
        this.State = state;
        fill.color = state.fillColor;
        outline.effectColor = state.outlineColor;
    }
}
