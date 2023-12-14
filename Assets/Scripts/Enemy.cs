using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static audioManager;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float velocidadMinima = 2f;
    [SerializeField] private float velocidadMaxima = 5f;
    [SerializeField] private float tama�oMinimo = 1f;
    [SerializeField] private float tama�oMaximo = 3f;
    [SerializeField] private float vida;
    [SerializeField] private int puntaje;

    public float _VelocidadMinima { get => velocidadMinima; set => velocidadMinima = value; }
    public float _VelocidadMaxima { get => velocidadMaxima; set => velocidadMaxima = value; }
    public float _Tama�oMinimo { get => tama�oMinimo; set => tama�oMinimo = value; }
    public float _Tama�oMaximo { get => tama�oMaximo; set => tama�oMaximo = value; }
    public float _Vida { get => vida; set => vida = value; }
    public int _Puntaje { get => puntaje; set => puntaje = value; }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
