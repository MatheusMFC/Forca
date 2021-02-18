using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MostraMensagemUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int scorePalavras = PlayerPrefs.GetInt("scorePalavras");
        string mensagemFinal;
        Color cor;
        switch (scorePalavras)
        {
            case 0:
                mensagemFinal = "Muito triste, não acertou nenhuma palavra :(";
                cor = Color.red;
                break;
            case 1:
                mensagemFinal = "Não merece parabéns, acertou só uma palavra :\\";
                cor = Color.yellow;
                break;
            default:
                mensagemFinal = "Parabéns, você acertou " + scorePalavras + " palavras :)";
                cor = Color.green;
                break;
        }
        gameObject.GetComponent<Text>().text = mensagemFinal;
        gameObject.GetComponent<Text>().color = cor;
    }
}