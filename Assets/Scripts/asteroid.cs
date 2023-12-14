using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : MonoBehaviour
{
    public float velocidadMinima = 2f;
    public float velocidadMaxima = 5f;
    public float tama�oMinimo = 1f;
    public float tama�oMaximo = 3f;
    [SerializeField]private float vida;
    [SerializeField]private int puntaje;

    [SerializeField] AudioClip explosionClip;
    void Start()
    {
        float tama�o = Random.Range(tama�oMinimo, tama�oMaximo);
        transform.localScale = new Vector3(tama�o, tama�o, 1);

        float velocidad = Random.Range(velocidadMinima, velocidadMaxima);
        GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * velocidad;

        vida = Random.Range(tama�o, tama�o * 2);
        puntaje = Mathf.RoundToInt(tama�o * 10);
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
