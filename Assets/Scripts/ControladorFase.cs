using UnityEngine;
using UnityEngine.UI;

public class ControladorFase : MonoBehaviour
{
    internal float TempoRestante;
    public GameObject telaGanhou, telaPerdeuErrou, telaPerdeuTempo, telaPause;

    // Elementos gráficos presentes na barra superior do game
    public Image imagemTacaSelecionada;
    public Text textoTempoRestante, textoFaseAtual;

    // Vetores das imagens das garrafas e dos tipos de bebidas
    public Sprite[] bebidas;
    public string[] tipos;

    // Variáveis representando o objeto do personagem e da garrafa
    public GameObject personagem;
    public SpriteRenderer bebidaNaTela;

    // Variáveis de controle
    internal int faseAtual, numBebidaAtual;
    internal string nomeBebidaAtual, nomeTacaAtual;
    internal Vector3 posInicialPersonagem;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posInicialPersonagem = personagem.transform.localPosition;
        TempoRestante = 60;
        faseAtual = 1;
        nomeTacaAtual = "";
        EscolherUmaBebida();
        imagemTacaSelecionada.sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        //codigo para diminuir o tempo
        TempoRestante -= Time.deltaTime;

        //código para atualizar os textos na tela
        textoTempoRestante.text = "Tempo Restante: " + TempoRestante.ToString("00");
        textoFaseAtual.text = "Fase: " + faseAtual;

        //verifica se o tempo acabou
        if (TempoRestante <= 0)
        {
            telaPerdeuTempo.SetActive(true);
            Time.timeScale = 0;
            TempoRestante = 0;
        }
    }

    public void PegarTaca(GameObject taca)
    {
        imagemTacaSelecionada.sprite = taca.GetComponent<SpriteRenderer>().sprite;
        imagemTacaSelecionada.preserveAspect = true;
        nomeTacaAtual = taca.GetComponent<ControladorTaca>().tipo;
    }

    public void Comparar() 
    { 
        if (nomeTacaAtual == nomeBebidaAtual)
        {
            telaGanhou.SetActive(true);
            Time.timeScale = 0;
        }
        else if (nomeTacaAtual != "")
        {
            telaPerdeuErrou.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void EscolherUmaBebida()
    {
        int valorAleatorio = (int)(Random.value * bebidas.Length);

        if (numBebidaAtual == valorAleatorio)
            valorAleatorio++;

        bebidaNaTela.sprite = bebidas[valorAleatorio];
        nomeBebidaAtual = tipos[valorAleatorio];
    }

    public void Pausar()
    {
        telaPause.SetActive(true);
        Time.timeScale = 0;
    }

    public void Despausar()
    {
        telaPause.SetActive(false);
        Time.timeScale = 1;
    }

    public void AvancarFase()
    {
        //avanço para a proxima fase
        faseAtual++;
        
        personagem.transform.localPosition = posInicialPersonagem; //reposiciona o personagem
        TempoRestante += 10; //acrescenta mais 10 segundos
        //"tira" a taça do personagem e escolhe nova bebida
        nomeTacaAtual = "";
        EscolherUmaBebida();
        imagemTacaSelecionada.sprite = null;

        //desliga a tela ganhou e descongela o tempo
        telaGanhou.SetActive(false);
        Time.timeScale = 1;
    }

    public void RecomecarFase()
    {
        //volta para a fase 1
        faseAtual = 1;

        personagem.transform.localPosition = posInicialPersonagem; //reposiciona o personagem
        TempoRestante = 60;

        //"tira" a taça do personagem e escolhe nova bebida
        nomeTacaAtual = "";
        EscolherUmaBebida();
        imagemTacaSelecionada.sprite = null;

        //desliga as telas perdeu e descongela o tempo
        telaPerdeuErrou.SetActive(false);
        telaPerdeuTempo.SetActive(false);
        Time.timeScale = 1;
    }
}