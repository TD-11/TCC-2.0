using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Definição de variáveis para pegar componentes
     private Rigidbody2D rig;
     private Animator anim;
    
    // Variável de velocidade
    public float velocity;
    // Variável de força do pulo
    public float forceJump;
    // Variável para verificar se está pulando ou não
    private bool jumping;
    // Variável para verificar se está no segundo pulo
    private bool doubleJump;
    
    void Start()
    {
        //Pegando componentes
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        // Chamando função
        Jump();
    }
    void FixedUpdate()
    {
        // Chamando função
        Move();
    }    
    // função para movimentação
    void Move()
        {
            // Vai definir em que sentido o player vai se movimentar
            Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
            // Aplica a velocidade de movimentação em tempo real
            transform.position += move * Time.deltaTime * velocity;
            // Se o usuário movimentar-se horizontalmente para a direita...
            if (Input.GetAxis("Horizontal") > 0f)
            {
                // ...será exibido a animação de correr
                anim.SetBool("Running", true);
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
            }
            // Se o usuário movimentar-se horizontalmente para a esquerda...
            if (Input.GetAxis("Horizontal") < 0f)
            {
                // ...será exibido a animação de correr, só que para ooutro lado
                anim.SetBool("Running", true);
                transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
            // Caso o jogador não se movimente... 
            if (Input.GetAxis("Horizontal") == 0f)
            {
                // ... não será exibido a animação
                anim.SetBool("Running", false);
            }
        }
    // Função para o pulo do player
    void Jump()
    {
        // Caso o usuário pressione o botão de pulo...
        if (Input.GetButtonDown("Jump")) 
        {
            // Vai ser verificado se a variável "jumping" é falsa
            if (!jumping)
            {
                // Executa a animação do pulo
                anim.SetBool("Jumping", true);
                // Se for a condição for verdadeira o player vai saltar
                // Aqui vamos acessar o componente Rigid Body 2D e aplicar a direção da força vezes a força
                rig.velocity = Vector2.up * forceJump;
                // Define "doubleJump" como "true". Possibilitando o segundo pulo
                doubleJump = true;
            }
            // caso "jumping' for verdadeiro...
            else
            {
                // ... e se "doubleJump" for verdadeiro...
                if (doubleJump) 
                {
                    anim.SetBool("SecondJumping", true);
                    // Aqui vamos acessar o componente Rigid Body 2D e aplicar a direção da força vezes a força novamente
                     rig.velocity = Vector2.up * forceJump;
                     // Define "doubleJump" como "false"
                     doubleJump = false; 
                }
             } 
        } 
    }
    // Vai verificar se o player entrou em colisão com outro
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Se ele colidir com um objeto que tenha uma tag Ground...
        if (collision.gameObject.tag == "Ground")
        {
            // E a animação de pulo será desativada
            anim.SetBool("Jumping", false);
            anim.SetBool("SecondJumping", false);
            // ... a variável "jumping" vai ser falsa
            jumping = false;
        }
        // Se o player colidir em um objeto que tenha a tag Danger... 
        if (collision.gameObject.tag == "Danger")
        {
            // ... o controle do jogo irá ativar o painel de "Game Over"
            GameController.instance.ShowGameOver();
            // E o player será destruído
            Destroy(gameObject);
        }
        // Se o player colidir em um objeto que tenha a tag Victory... 
        if (collision.gameObject.tag == "Victory")
        {
             // ... o controle do jogo irá ativar o painel de "Victory"
            GameController.instance.ShowVictory();
            // E o player será destruído
            Destroy(gameObject);
        }
    }
    // Essa função verifica se o jogador parou de colidir com um objeto 
    private void OnCollisionExit2D(Collision2D collision)
    {
        // Caso ele tenha parado de colidir com um objeto com tag Ground...
        if (collision.gameObject.tag == "Ground")
        {
            // ... a variável "jumping" será verdadeira
            jumping = true;
        }
    }
}

