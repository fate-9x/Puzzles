using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RayDetector : MonoBehaviour
{
    [SerializeField] private TextMeshPro text;
    private RaycastHit[] results = new RaycastHit[10];
    
    void Update()
    {
        int numHits = Physics.RaycastNonAlloc(transform.position, transform.forward, results);
        if (numHits > 0)
        {
            RaycastHit hit = results[0];
            text.text = hit.collider.name;

            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red);
        }
        else
        {
            text.text = "No Hit";

            Debug.DrawRay(transform.position, transform.forward * 1000, Color.green);
        }
    }
}