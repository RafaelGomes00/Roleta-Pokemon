using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleJSON;
using System.IO;
using UnityEngine.Networking;

public class Roleta : MonoBehaviour
{
    [SerializeField] private Transform forceBody;

    [SerializeField] private int forceMin;
    [SerializeField] private int forceMax;

    [SerializeField] private APIRequests request;

    [SerializeField] private TextMesh[] text;

    private Rigidbody body;
    private string result;

    private void Start()
    {
        body = GetComponent<Rigidbody>();
        StartCoroutine(GenerateNewPokemons());
    }

    public void ApplyForce()
    {
        body.AddForceAtPosition(Vector3.up * Random.Range(forceMin, forceMax), forceBody.position);
        StartCoroutine(WaitSpin());
    }

    private IEnumerator GenerateNewPokemons()
    {
        for(int i = 0; i < 12; i++)
        {
            yield return StartCoroutine(request.InitiateRequest(Random.Range(1, 808)));

            Pokemon poke = request.pokemon;
            text[i].text = poke.name;
        }

        Controller.instance.EnableButton();
    }

    private IEnumerator WaitSpin()
    {
        yield return new WaitForFixedUpdate();

        while(body.angularVelocity.x > 0)
        {
            yield return null;
        }

        StartCoroutine(Controller.instance.UpdateUI());
    }
}
