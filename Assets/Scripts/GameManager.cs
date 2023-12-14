using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField] protected GameObject asteroidePrefab;
    [SerializeField] protected GameObject MainMenu;
    [SerializeField] protected GameObject Player;
    [SerializeField] protected TextMeshProUGUI scoreTxt;
    [SerializeField] protected TextMeshProUGUI highScoreTxt;
    [SerializeField] protected TextMeshProUGUI livesTxt;

    [SerializeField] protected AudioClip ScoreFx;

    [SerializeField] private int cantidadAsteroides = 5;
    [SerializeField] private float tiempoEspera = 2f;
    [SerializeField] private float tiempoEsperaDificultad = 5f;
   
    [SerializeField] private bool pause = false;
    [SerializeField] private int vidas;
    [SerializeField] private int score;
    [SerializeField] private int highScore;

    public static GameManager Instance { get; private set; }
    public int _CantidadAsteroides { get => cantidadAsteroides; set => cantidadAsteroides = value; }
    public float _TiempoEspera { get => tiempoEspera; set => tiempoEspera = value; }
    public float _TiempoEsperaDificultad { get => tiempoEsperaDificultad; set => tiempoEsperaDificultad = value; }
    public bool _Pause { get => pause; set => pause = value; }
    public int _Vidas { get => vidas; set => vidas = value; }
    public int _Score { get => score; set => score = value; }
    public int _HighScore { get => highScore; set => highScore = value; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {
       StartCoroutine(GenerarAsteroidesInfinitos());
    }

    public void gameStart()
    {
        StartCoroutine(initGame());
    }

    public void gamePause()
    {
        _Pause = !_Pause;

        if (_Pause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void gameOver(GameObject player)
    {
        _Vidas--;
        if (_Vidas <= 0)
        {
            comprobarHighScore();
            Destroy(player);
            StopCoroutine(GenerarAsteroidesInfinitos());
            MainMenu.SetActive(true);
        }
       
    }
    
    public void CloseGame() 
    {
     Application.Quit();
    }


    IEnumerator GenerarAsteroidesInfinitos()
    {
        while (true)
        {
            for (int i = 0; i < _CantidadAsteroides; i++)
            {
                // Calcular posición aleatoria en un borde de la pantalla
                Vector2 posicion = ObtenerPosicionAleatoriaEnBorde();

                // Instanciar asteroide en la posición calculada
                Instantiate(asteroidePrefab, posicion, Quaternion.identity);
            }

            // Esperar antes de generar más asteroides
            yield return new WaitForSeconds(_TiempoEspera);
        }
    }

    Vector2 ObtenerPosicionAleatoriaEnBorde()
    {
        float anchoPantalla = Camera.main.orthographicSize * Screen.width / Screen.height;
        float alturaPantalla = Camera.main.orthographicSize;

        float bordeAleatorio = Random.Range(0, 4);  // 0: arriba, 1: abajo, 2: izquierda, 3: derecha

        switch (bordeAleatorio)
        {
            case 0: // Arriba
                return new Vector2(Random.Range(-anchoPantalla, anchoPantalla), alturaPantalla);
            case 1: // Abajo
                return new Vector2(Random.Range(-anchoPantalla, anchoPantalla), -alturaPantalla);
            case 2: // Izquierda
                return new Vector2(-anchoPantalla, Random.Range(-alturaPantalla, alturaPantalla));
            case 3: // Derecha
                return new Vector2(anchoPantalla, Random.Range(-alturaPantalla, alturaPantalla));
            default:
                return Vector2.zero;
        }
    }

    IEnumerator initGame()
    {
        _Vidas = 3;
        _CantidadAsteroides = 5;
        _TiempoEspera = 2f;
        StopCoroutine(GenerarAsteroidesInfinitos());

        GameObject[] gos = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject go in gos)
        {
            Destroy(go);
        }

        GameObject[] gos2 = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject go2 in gos2)
        {
            Destroy(go2);
        }

        Instantiate(Player, new Vector3(0, 0, 1), Quaternion.identity);
        yield return new WaitForSeconds(3f);
        StartCoroutine(GenerarAsteroidesInfinitos());
        StartCoroutine(aumentarDificultad());
    }


    void Update()
    {
       scoreTxt.text = _Score.ToString();
       highScoreTxt.text = _HighScore.ToString();
       livesTxt.text = "lives: " + _Vidas.ToString();
    }

    public void sumarScore(int scorer) 
    {
        _Score += scorer;
    }

    public void comprobarHighScore()
    {
        if (_HighScore < _Score)
        {
            _HighScore = _Score;
            audioManager.Instance.playFX(ScoreFx);
        }
    }

    IEnumerator aumentarDificultad() 
    {
        yield return new WaitForSeconds(_TiempoEsperaDificultad);
        _CantidadAsteroides++;
        if (_TiempoEspera >= .1f)
        {
            _TiempoEspera -= .1f;
        }
    }
}

