using UnityEngine;

public class mesaLegumes : MonoBehaviour
{
    // Variável que você define no Inspector (ex: "Vermelho", "Verde", "Amarelo")
    public string Verde;

    // Variável para armazenar a contagem de pratos corretos nesta mesa
    private int pratosCorretosNaMesa = 2;

    // Referência ao script que gerencia o estado geral do puzzle
    public PuzzleManager puzzleManager;

    // Chama quando um Collider entra na área de Trigger da mesa
    private void OnTriggerEnter(Collider other)
    {
        // Verifica se o objeto que entrou tem a tag da cor esperada
        if (other.CompareTag(Verde))
        {
            pratosCorretosNaMesa++;
            Debug.Log(Verde + " correto na mesa " + gameObject.name + ". Total: " + pratosCorretosNaMesa);

            // Notifica o PuzzleManager sobre a mudança
            puzzleManager.ChecarProgresso();
        }
    }

    // Chama quando um Collider sai da área de Trigger da mesa
    private void OnTriggerExit(Collider other)
    {
        // Verifica se o objeto que saiu tem a tag da cor esperada
        if (other.CompareTag(Verde))
        {
            pratosCorretosNaMesa--;
            Debug.Log(Verde + " removido da mesa " + gameObject.name + ". Total: " + pratosCorretosNaMesa);

            // Notifica o PuzzleManager sobre a mudança
            puzzleManager.ChecarProgresso();
        }
    }

    // Método para ser chamado pelo PuzzleManager ou por você, se necessário
    public int GetContagemCorreta()
    {
        return pratosCorretosNaMesa;
    }
}
