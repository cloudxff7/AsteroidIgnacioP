using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, IPlaySound
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject proyectilPrefab;
    [SerializeField] private AudioClip ShootClip;
    [SerializeField] private AudioClip lostClip;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float rotationSpeed = 180f;

    private AudioClip currentClip;
    public float _Speed { get => speed; set => speed = value; }
    public float _RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }

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

        rb.velocity = transform.up * MovementInput.y * _Speed;
        float rotation = -MovementInput.x * _RotationSpeed;
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
            nuevoProyectil.GetComponent<Rigidbody2D>().velocity = transform.up * nuevoProyectil.GetComponent<bullet>()._Velocidad;
            currentClip = ShootClip;
            PlayFx();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            GameManager.Instance.gameOver(gameObject);
            transform.position = new Vector3(0, 0, 1);
            currentClip = lostClip;
            PlayFx();
        }
    }

    public void PlayFx()
    {
        audioManager.Instance.playFX(currentClip);
    }
}

