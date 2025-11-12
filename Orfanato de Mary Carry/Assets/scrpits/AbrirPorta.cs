using UnityEngine;

public class AbrirPorta : MonoBehaviour
{
    private Animator animator;
    private bool playerIsNear = false; // Variável para rastrear a proximidade do jogador
    public KeyCode interactionKey = KeyCode.F; // Tecla que acionará a interação

    void Start()
    {
        // Certifica-se de que o Animator está no mesmo objeto ou em um filho
        animator = GetComponent<Animator>();

        // Segurança: verifique se o Animator foi encontrado
       
    }

    void Update()
    {
        // 1. Verifica se o jogador está perto.
        // 2. Verifica se a tecla de interação (F) foi pressionada neste frame.
        if (playerIsNear && Input.GetKeyDown(interactionKey))
        {
            if (animator == null)
            {
                Debug.LogError("Animator não encontrado no objeto da porta. Certifique-se de que ele está anexado.");
            }
            // Aciona o parâmetro "abrir" no Animator, iniciando a animação
            // Você pode adicionar uma verificação aqui para não abrir se já estiver aberta.
            animator.enabled = true;
            animator.SetTrigger("abrir");
            Debug.Log("Porta acionada com a tecla F!");
        }
    }

    // --- Lógica do Gatilho (Trigger) ---

    // Quando algo entra no gatilho
    void OnTriggerEnter(Collider other)
    {
        // **IMPORTANTE:** Mude a tag de verificação para "Player",
        // pois geralmente é o objeto pai do personagem, não a "MainCamera".
        // Se a sua MainCamera for o objeto com o collider/tag que entra no gatilho,
        // mantenha "MainCamera", caso contrário, use "Player".
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            playerIsNear = true;
            Debug.Log("Jogador perto! Pressione F para abrir.");
        }
    }

    // Quando algo sai do gatilho
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("MainCamera"))
        {
            playerIsNear = false;
            Debug.Log("Jogador longe.");

            // Se quiser que a porta feche automaticamente ao sair, descomente a linha abaixo:
            // animator.SetTrigger("fechar"); 
        }
    }
}