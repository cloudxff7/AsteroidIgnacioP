using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    [SerializeField]GameObject asteroidePrefab;
    [SerializeField] GameObject MainMenu;
    public int cantidadAsteroides = 5;
    public float tiempoEspera = 2f;
    public float tiempoEsperaDificultad = 5f;
    [SerializeField] GameObject Player;
    [SerializeField] bool pause= false;
    [SerializeField] int vidas;
    [SerializeField] int score;
    [SerializeField] int HighScore;

    [SerializeField]TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI highScoreTxt;
    [SerializeField] TextMeshProUGUI livesTxt;

    [SerializeField] AudioClip ScoreFx;
    public static GameManager Instance { get; private set; }
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
        pause = !pause;

        if (pause) Time.timeScale = 0;
        else Time.timeScale = 1;
    }

    public void gameOver(GameObject player)
    {
        vidas--;
        if (vidas <= 0)
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
            for (int i = 0; i < cantidadAsteroides; i++)
            {
                // Calcular posición aleatoria en un borde de la pantalla
                Vector2 posicion = ObtenerPosicionAleatoriaEnBorde();

                // Instanciar asteroide en la posición calculada
                Instantiate(asteroidePrefab, posicion, Quaternion.identity);
            }

            // Esperar antes de generar más asteroides
            yield return new WaitForSeconds(tiempoEspera);
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
        vidas = 3;
        cantidadAsteroides = 5;
        tiempoEspera = 2f;
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
       scoreTxt.text = score.ToString();
       highScoreTxt.text = HighScore.ToString();
       livesTxt.text = "lives: " + vidas.ToString();
    }

    public void sumarScore(int scorer) 
    {
        score += scorer;
    }

    public void comprobarHighScore()
    {
        if (HighScore < score)
        {
            HighScore = score;
            audioManager.Instance.playFX(ScoreFx);
        }
    }

    IEnumerator aumentarDificultad() 
    {
        yield return new WaitForSeconds(tiempoEsperaDificultad);
        cantidadAsteroides++;
        if (tiempoEspera >= .1f)
        {
            tiempoEspera -= .1f;
        }
    }
}

