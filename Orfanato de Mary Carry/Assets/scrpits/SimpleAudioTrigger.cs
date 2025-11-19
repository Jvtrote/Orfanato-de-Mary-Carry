using UnityEngine;

// Certifique-se de que este script está anexado a um GameObject que também tenha um Audio Source e um Collider (Is Trigger).
public class SimpleAudioTrigger : MonoBehaviour
{
    private AudioSource audioSource;

    // Controla se o áudio já foi tocado
    private bool hasPlayed = false;

    private const string PlayerTag = "MainCamera";

    void Start()
    {
        // 1. Pega o componente Audio Source que deve estar neste mesmo objeto
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("O objeto '" + gameObject.name + "' precisa de um componente Audio Source!");
        }
        else if (audioSource.clip == null)
        {
            Debug.LogWarning("O Audio Source no objeto '" + gameObject.name + "' não tem um AudioClip definido.");
        }
    }

    /// <summary>
    /// Chamada quando outro Collider entra neste Trigger.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        // 1. Verifica se o objeto que entrou é o jogador E se o áudio ainda não foi tocado.
        if (other.CompareTag(PlayerTag) && !hasPlayed)
        {
            // 2. Toca o áudio se ele existir
            if (audioSource != null && audioSource.clip != null)
            {
                audioSource.Play();

                // 3. Marca como tocado para garantir que seja apenas uma vez
                hasPlayed = true;

                // Opcional: Desativa o componente Collider
                Collider col = GetComponent<Collider>();
                if (col != null)
                {
                    col.enabled = false;
                }
            }
        }
    }
}