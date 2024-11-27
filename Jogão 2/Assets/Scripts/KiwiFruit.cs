using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiFruit : MonoBehaviour
{
    // Permite acessar os componentes
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    // Define a variável "collected" como um game object
    public GameObject collected;
    // Variável que vai armazenar cada unidade de kiwi coletado
    public int score;
    void Start()
    {
        // Armazena os componentes nas variaveis
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        
    }
    // Essa função serve para que se o player entrar na zona do kiwi
    void OnTriggerEnter2D(Collider2D collider)
    {
        // Caso o player entre na zona...
        if (collider.gameObject.tag == "Player")
        {
            //... vai desabilitar o sprite do kiwi...
            sr.enabled = false;
            //... vai desbilitar o colisor
            circle.enabled = false;
            // E vai ativar a animação de coletado
            collected.SetActive(true);
            
            // Vai contabilizar o kiwi no score
            GameController.instance.totalScore += score;
            
            // Vai destruir a animação de coletado
            Destroy(gameObject, 0.5f);
        }
    }
}
