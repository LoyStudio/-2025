using UnityEngine;

public class LoadCharacters : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;

    private void Awake()
    {
        switch (Shop.currnetCharacter) 
        {
            case 0:
                characters[0].SetActive(true);
                break;
            case 1:
                characters[1].SetActive(true);
                break;
            case 2:
                characters[2].SetActive(true);
                break;
        }
    }
}
