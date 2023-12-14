using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float velocidad = 10f;
    public float damage = 10f; 
    void Update()
    {
        transform.Translate(Vector2.up * velocidad * Time.deltaTime);

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
}
