using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public float velocidadMinima = 2f;
    public float velocidadMaxima = 5f;
    public float tamañoMinimo = 1f;
    public float tamañoMaximo = 3f;
    [SerializeField]private float vida;
    [SerializeField]private int puntaje;

    [SerializeField] AudioClip explosionClip;
    void Start()
    {
        float tamaño = Random.Range(tamañoMinimo, tamañoMaximo);
        transform.localScale = new Vector3(tamaño, tamaño, 1);

        float velocidad = Random.Range(velocidadMinima, velocidadMaxima);
        GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * velocidad;

        vida = Random.Range(tamaño, tamaño * 2);
        puntaje = Mathf.RoundToInt(tamaño * 10);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            vida--;
            if (vida < 0)
            {
                audioManager.Instance.playFX(explosionClip);
                GameManager.Instance.sumarScore(puntaje);
                Destroy(gameObject);
            }
        }
    }
}
