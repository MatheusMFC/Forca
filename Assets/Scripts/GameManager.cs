using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    private int numTentativas;              //Armazena o número de tentativas válidas da rodada
    private int maxNumTentativas;           //Número máximo de tentativas para Forca ou Salvação
    int score = 0;
    int scorePalavras = 0;

    public GameObject letra;                //prefab da letra no Game
    public GameObject centro;               //objeto de texto que indica o centro da tela

    private string palavraOculta = "";      //palavra a ser descoberta
    //private string[] palavrasOcultas = new string[] { "carro", "elefante", "futebol" }; //array de palavras ocultas   (usada no Lab2 -parte A)

    private int tamanhoPalavraOculta;       //tamanho da palavra a ser descoberta
    char[] letrasOcultas;                   //letras da palavra a ser descoberta
    bool[] letrasDescobertas;               //indicador de quais letras foram descobertas

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centroDaTela");
        InitGame();
        InitLetras();
        numTentativas = 0;
        maxNumTentativas = 10;
        UpdateNumTentativas();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
        checkTeclado();
    }

    void InitLetras()
    {
        int numLetras = tamanhoPalavraOculta;
        for (int i = 0; i < numLetras; i++)
        {
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i - numLetras / 2.0f) * 80), centro.transform.position.y, centro.transform.position.z); ;
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra" + (i + 1);         //nomeia na hierarquia a GameObject com letra-(iésima+1), i = 1..numLetras
            l.transform.SetParent(GameObject.Find("Canvas").transform);         //posiciona-se como filho do GameObject Canvas
        }
    }

    void InitGame()
    {
        //palavraOculta = "Elefante";                                   //definição da palavra oculta   (usada no Lab1 -parte A)
        //int numeroAleatorio = Random.Range(0, palavrasOcultas.Length);  //sorteando um número de 0 ao tamanho da lista de palavras    (usada no Lab2 -parte A)
        //palavraOculta = palavrasOcultas[numeroAleatorio];               //seleciona uma palavra sorteada  (usada no Lab2 -parte A)
        palavraOculta = PegaUmaPalavraDoArquivo();
        tamanhoPalavraOculta = palavraOculta.Length;                    //determinação do número de letras da palavra oculta
        palavraOculta = palavraOculta.ToUpper();                        //transfromando a palavra oculta para letras maiúsculas
        letrasOcultas = new char[tamanhoPalavraOculta];                 //instanciando a lista de letras da palavra oculta
        letrasOcultas = palavraOculta.ToCharArray();                    //atribuindo os valores de cada letra a uma posição da lista
        letrasDescobertas = new bool[tamanhoPalavraOculta];             //instanciando a lista do indicador das letras descobertas
    }

    void checkTeclado()
    {
        if (Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);

            if (letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122)
            {
                numTentativas++;
                UpdateNumTentativas();
                if (numTentativas > maxNumTentativas)
                {
                    SceneManager.LoadScene("Lab1_forca");
                }

                for (int i = 0; i <= tamanhoPalavraOculta; i++)
                {
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = char.ToUpper(letraTeclada);
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra" + (i + 1)).GetComponent<Text>().text = letraTeclada.ToString();
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();
                            VerificaSePalavraDescoberta();
                        }
                    }
                }
            }
        }
    }

    void UpdateNumTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + " | " + maxNumTentativas;

    }

    void UpdateScore()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score " + score;
    }

    void VerificaSePalavraDescoberta()
    {
        bool condicao = true;
        for (int i = 0; i < tamanhoPalavraOculta; i++)
        {
            condicao = condicao && letrasDescobertas[i];
        }
        if (condicao)
        {
            scorePalavras = PlayerPrefs.GetInt("scorePalavras");
            scorePalavras++;
            PlayerPrefs.SetInt("scorePalavras", scorePalavras);
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("Lab1_salvo");
        }
    }

    string PegaUmaPalavraDoArquivo()
    {
        TextAsset t1 = (TextAsset)Resources.Load("palavras", typeof(TextAsset));
        string s = t1.text;
        string[] palavras = s.Split(' ');
        int palavraAleatoria = Random.Range(0, palavras.Length + 1);
        return palavras[palavraAleatoria];
    }
}