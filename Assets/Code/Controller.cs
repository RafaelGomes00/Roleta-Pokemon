using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Controller : MonoBehaviour
{
    public static Controller instance;

    public string currentChoise;

    [SerializeField] private Button rollButton;
    
    [SerializeField] private TextMeshProUGUI nomeTXT;
    [SerializeField] private TextMeshProUGUI idTXT;
    [SerializeField] private Image image;

    [SerializeField] private Sprite mainImage;

    private string result;

    private void Start()
    {
        instance = this;
    }

    public void EnableButton()
    {
        rollButton.interactable = true;
    } 

    public void UpdateUIRoll()
    {
        nomeTXT.text = "Sorteando...";
        idTXT.text = "Sorteando...";
        image.sprite = mainImage;
    }
    public IEnumerator UpdateUI()
    {
        APIRequests request = GetComponent<APIRequests>();

        yield return request.StartCoroutine(request.InitiateRequest(currentChoise));

        Pokemon poke = request.pokemon;

        nomeTXT.text = poke.name;
        idTXT.text = poke.id.ToString();
        image.sprite = poke.image;

        EnableButton();
    }
}
