using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] private float velocidad = 10f;
    [SerializeField] private float damage = 10f;

    public float _Velocidad { get => velocidad; set => velocidad = value; }
    public float _Damage { get => damage; set => damage = value; }

    void Update()
    {
        transform.Translate(Vector2.up * _Velocidad * Time.deltaTime);

    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    
}
