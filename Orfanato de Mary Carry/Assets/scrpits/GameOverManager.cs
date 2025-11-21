using UnityEngine;
using UnityEngine.SceneManagement; // É obrigatório para gerenciar cenas!

public class GameManager : MonoBehaviour
{
    // Função pública que será chamada pelo botão no Inspector
    // O nome (string) da cena deve ser EXATAMENTE como está no seu projeto ("orfanato")
    public void LoadOrfanatoScene()
    {
        Debug.Log("Carregando cena: orfanato...");

        // Carrega a cena pelo nome
        SceneManager.LoadScene("orfanato");
    }
}