using UnityEngine;
using UnityEngine.SceneManagement; // É obrigatório para mudar de cena!

public class CreditSceneTrigger : MonoBehaviour
{
    [Header("Nome da Cena de Créditos")]
    // Digite o nome exato da cena que contém os créditos (ex: "CenaCreditos")
    public string creditSceneName = "creditos";

    private const string PlayerTag = "MainCamera"; // Garante que apenas o jogador ative

    // A função OnTriggerEnter é chamada quando o jogador entra na área do Collider
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou tem a tag correta
        if (other.CompareTag(PlayerTag))
        {
            // Opcional: Para garantir que o NavMeshAgent do monstro não cause erros ao sair da cena
            Time.timeScale = 1f;

            Debug.Log("Fim do jogo! Carregando cena: " + creditSceneName);

            // Carrega a cena de forma síncrona (a cena de Créditos)
            SceneManager.LoadScene(creditSceneName);
        }
    }
}