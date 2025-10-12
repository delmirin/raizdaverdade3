using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class Pergunta
    {
        public string texto;
        public string[] respostas;
        public int indiceCorreto; // 0, 1 ou 2
    }

    public TextMeshProUGUI perguntaText;
    public Button[] botoes;
    public GameObject feedbackPanel;
    public TextMeshProUGUI feedbackText;

    public Pergunta[] perguntas;

    private int indicePergunta = 0;

    void Start()
    {
        MostrarPergunta();
    }

    void MostrarPergunta()
    {
        feedbackPanel.SetActive(false);

        Pergunta p = perguntas[indicePergunta];
        perguntaText.text = p.texto;

        for (int i = 0; i < botoes.Length; i++)
        {
            botoes[i].GetComponentInChildren<TextMeshProUGUI>().text = p.respostas[i];
            int copia = i;
            botoes[i].onClick.RemoveAllListeners();
            botoes[i].onClick.AddListener(() => VerificarResposta(copia));
        }
    }

    void VerificarResposta(int indiceEscolhido)
    {
        Pergunta p = perguntas[indicePergunta];
        bool acertou = indiceEscolhido == p.indiceCorreto;

        feedbackPanel.SetActive(true);
        feedbackText.text = acertou ? "✅ Certo!" : "❌ Errado!";
        feedbackPanel.GetComponent<Image>().color = acertou ? Color.green : Color.red;

        Invoke(nameof(ProximaPergunta), 1.5f); // espera 1.5 segundos
    }

    void ProximaPergunta()
    {
        indicePergunta++;
        if (indicePergunta < perguntas.Length)
            MostrarPergunta();
        else
            perguntaText.text = "Fim do quiz!";
    }
}
