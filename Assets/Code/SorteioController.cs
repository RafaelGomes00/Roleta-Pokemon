using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorteioController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Texto"))
        {
            Controller.instance.currentChoise = other.GetComponent<TextMesh>().text;
        }
    }
}
