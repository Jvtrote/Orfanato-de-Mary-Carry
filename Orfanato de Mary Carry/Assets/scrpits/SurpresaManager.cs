using UnityEngine;
using UnityEngine.SceneManagement; // Necessário para carregar cenas

public class SurpresaManager : MonoBehaviour
{
    [Header("Configuração de Fim de Jogo")]
    public string gameOverSceneName = "GameOverScene"; // Nome da sua cena de Game Over
    public float delayBeforeLoad = 3.0f; // Tempo para ver a animação de ataque

    public void EndGame()
    {
        Debug.Log("Fim de Jogo acionado! Carregando cena de Game Over...");

        // Pausa o jogo (opcional)
        Time.timeScale = 0f;

        // Chama a coroutine para dar tempo para a animação do monstro
        StartCoroutine(LoadGameOverAfterDelay());
    }

    private System.Collections.IEnumerator LoadGameOverAfterDelay()
    {
        // Espera o tempo definido
        yield return new WaitForSecondsRealtime(delayBeforeLoad);

        // Retorna o tempo ao normal
        Time.timeScale = 1f;

        // Carrega a cena de Game Over
        SceneManager.LoadScene(gameOverSceneName);
    }
}