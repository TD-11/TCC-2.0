using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Define o objeto "instance" como estático
    //Quer dizer que sempre que for preciso só basta chamar esse objeto e não criar outro
    public static GameController instance;
    
    //Variável que armazena o valor total de kiwis coletados
    public int totalScore;
    // Permite converter valores numéricos para a interface
    public TMP_Text text;
    // Define como "GameObject" o game over
    public GameObject gameOver;
    // Define como "GameObject" o victory
    public GameObject victory;
    void Start()
    {
        // Permite instanciar o mesmo objeto sempre
        instance = this;
    }

    void Update()
    {
        // Converte os valores para a interface
        text.text = totalScore.ToString();
    }
    // Função para mostrar a tela de "Game Over"
    public void ShowGameOver()
    {
        // Permite que o painel seja ativado
        gameOver.SetActive(true);
    }
    // Função para mostrar a tela de "Victory"
    public void ShowVictory()
    {
        // Permite que o painel seja ativado
        victory.SetActive(true);
    }
    // Essa função é para que o botão de "Restart" volte ao início do jogo caso seja clicado
    public void RestartGame(string lvlName)// Variável que armazena o nome do level
    {
        //Esse método irá reiniciar o level de acordo com qual seja informado
        SceneManager.LoadScene(lvlName);
    }
}
