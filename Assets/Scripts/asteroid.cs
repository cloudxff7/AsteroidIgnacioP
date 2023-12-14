using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroid : Enemy, IPlaySound
{
    [SerializeField] AudioClip explosionClip;
    void Start()
    {
        float tamaño = Random.Range(_TamañoMinimo , _TamañoMaximo);
        transform.localScale = new Vector3(tamaño, tamaño, 1);

        float velocidad = Random.Range(_VelocidadMinima, _VelocidadMaxima);
        GetComponent<Rigidbody2D>().velocity = Random.onUnitSphere * velocidad;

        _Vida = Random.Range(tamaño, tamaño * 2);
        _Puntaje = Mathf.RoundToInt(tamaño * 10);
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
            _Vida--;
            if (_Vida < 0)
            {
                PlayFx();
                GameManager.Instance.sumarScore(_Puntaje);
                Destroy(gameObject);
            }
        }
    }

    public void PlayFx()
    {
        audioManager.Instance.playFX(explosionClip);
    }
}
