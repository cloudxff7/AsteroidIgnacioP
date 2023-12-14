using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float rotationSpeed = 180f;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] PlayerInput playerInput;
    [SerializeField]GameObject proyectilPrefab;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerInput = GetComponent<PlayerInput>();
      
    }

    void Update()
    {
        PlayerMove();
        PlayerShoot();
        VerificarLimitesPantalla();
    }

    void PlayerMove() 
    {
        Vector2 MovementInput = playerInput.actions["Move"].ReadValue<Vector2>();

        rb.velocity = transform.up * MovementInput.y * speed;
        float rotation = -MovementInput.x * rotationSpeed;
        rb.MoveRotation(rb.rotation + rotation);
    }

    void VerificarLimitesPantalla()
    {
        Vector3 posicion = transform.position;

        float anchoPantalla = Camera.main.orthographicSize * Screen.width / Screen.height;
        float alturaPantalla = Camera.main.orthographicSize;

        if (posicion.x > anchoPantalla)
        {
            posicion.x = -anchoPantalla;
        }
        else if (posicion.x < -anchoPantalla)
        {
            posicion.x = anchoPantalla;
        }

        if (posicion.y > alturaPantalla)
        {
            posicion.y = -alturaPantalla;
        }
        else if (posicion.y < -alturaPantalla)
        {
            posicion.y = alturaPantalla;
        }

        transform.position = posicion;
    }
    void PlayerShoot()
    {
        if (playerInput.actions["Shoot"].WasPressedThisFrame())
        {
            GameObject nuevoProyectil = Instantiate(proyectilPrefab, transform.position, transform.rotation);
            nuevoProyectil.GetComponent<Rigidbody2D>().velocity = transform.up * nuevoProyectil.GetComponent<bullet>().velocidad;
        }
    }
}
