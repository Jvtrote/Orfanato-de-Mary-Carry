using UnityEngine;

public class DoorTriggerArea : MonoBehaviour
{
    [Header("Porta a Controlar")]
    // Arraste o GameObject da porta aqui no Inspector
    public GameObject doorObject;

    [Header("Configurações da Animação")]
    // O nome do parâmetro 'Trigger' no seu Animator Controller (ex: "AbrirPorta")
    public string animationTriggerName = "AbrirPorta";

    private Animator doorAnimator;
    private bool hasBeenOpened = false;
    private const string PlayerTag = "MainCamera"; // Tag do seu jogador

    void Start()
    {
        // 1. Verifica se a porta foi definida no Inspector
        if (doorObject == null)
        {
            Debug.LogError("O Door Object não foi atribuído no Inspector do " + gameObject.name);
            enabled = false;
            return;
        }

        // 2. Pega o componente Animator da porta
        doorAnimator = doorObject.GetComponent<Animator>();
        if (doorAnimator == null)
        {
            Debug.LogError("A porta ('" + doorObject.name + "') não tem um componente Animator!");
            enabled = false;
        }
    }

    // --- Lógica de Detecção do Jogador ---

    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou tem a tag "MainCamera" E se a porta ainda não foi aberta.
        if (other.CompareTag(PlayerTag) && !hasBeenOpened)
        {
            if (doorAnimator != null)
            {
                // Dispara o parâmetro 'Trigger' da animação
                doorAnimator.SetTrigger(animationTriggerName);
                hasBeenOpened = true; // Marca como aberta para não disparar novamente

                Debug.Log("Animação de porta disparada: " + animationTriggerName);

                // Opcional: Desativa o collider da área para economizar recursos após o evento
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    // O OnTriggerExit foi removido para que a porta não feche.
}