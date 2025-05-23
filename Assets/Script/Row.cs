using UnityEngine;

public class Row : MonoBehaviour
{
    
    public Tile[] tiles {  get; private set; }

    public string Word
    {
        get
        {
            string result = "";
            for (int i = 0; i < tiles.Length; i++)
            {
                result += tiles[i].letter;
            }

            return result;
        }
    }

    private void Awake()
    {
        tiles = GetComponentsInChildren<Tile>();
    }

}
