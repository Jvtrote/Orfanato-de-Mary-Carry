using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoScrollCredits : MonoBehaviour
{
    [Header("Configurações de Rolagem")]
    // Velocidade com que o texto subirá
    public float scrollSpeed = 50f;

    [Header("Tempo")]
    // Tempo em segundos para a cena de créditos
    public float creditDuration = 30f;
    // O nome da cena para voltar após os créditos (Ex: "MenuPrincipal")
    public string nextSceneName = "MenuPrincipal";

    private float timer;

    void Update()
    {
        // 1. Move o texto para cima (no eixo Y)
        transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);

        // 2. Controla a duração dos créditos
        timer += Time.deltaTime;

        if (timer >= creditDuration)
        {
            LoadNextScene();
        }
    }

    void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            // Se não houver próxima cena, encerra o jogo
            Application.Quit();
            Debug.Log("Fim do Jogo. Encerrando aplicação.");
        }
    }
}