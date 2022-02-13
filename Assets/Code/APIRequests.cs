using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIRequests : MonoBehaviour
{
    public Pokemon pokemon {get; private set;}

    private string nome;
    private int id;
    private Sprite image;

    public IEnumerator InitiateRequest(string name)
    {
        yield return StartCoroutine(RequestPokemon(name));
    }
    public IEnumerator InitiateRequest(int id)
    {
        yield return StartCoroutine(RequestPokemon(id.ToString()));
    }

    private IEnumerator RequestPokemon(string search)
    {
        UnityWebRequest request = UnityWebRequest.Get("https://pokeapi.co/api/v2/pokemon/" + search);

        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
            yield break;
        else
        {
            string result = request.downloadHandler.text;

            JSONNode pokeInfo = JSON.Parse(result);

            string pokeSpriteURL = pokeInfo["sprites"]["front_default"];
            UnityWebRequest pokeSpriteReq = UnityWebRequestTexture.GetTexture(pokeSpriteURL);
            yield return pokeSpriteReq.SendWebRequest();

            if (pokeSpriteReq.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error receiving the sprite");
                yield break;
            }

            Texture2D texture = DownloadHandlerTexture.GetContent(pokeSpriteReq);

            Sprite pokeSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            pokemon = new Pokemon(pokeInfo["name"], pokeInfo["id"], pokeSprite);
        }
    }
}
