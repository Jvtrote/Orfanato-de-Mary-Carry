using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    // Atribua as 3 Mesas (MesaSlot.cs) no Inspector
    public mesaCarne mesaCarne;   // Espera CorEsperada="Vermelho"
    public mesaLegumes mesaLegumes; // Espera CorEsperada="Verde"
    public MesaSlot mesaFrutas;  // Espera CorEsperada="Amarelo"

    public GameObject Chavecozinha;

    // Chama sempre que um prato é colocado ou removido (via MesaSlot.cs)
    public void ChecarProgresso()
    {
        // Requisito: 2 pratos vermelhos na mesa de carne E
        // 2 pratos verdes na mesa de legumes E
        // 2 pratos amarelos na mesa de frutas.

        bool puzzleCompleto =
            mesaCarne.GetContagemCorreta() == 2 &&
            mesaLegumes.GetContagemCorreta() == 2 &&
            mesaFrutas.GetContagemCorreta() == 2;

        if (puzzleCompleto)
        {
            ConcluirPuzzle();
        }
        else
        {
            // Opcional: Feedback visual ou sonoro de progresso incompleto
            Debug.Log("Puzzle incompleto. Continue colocando os pratos.");
        }
    }

    private void ConcluirPuzzle()
    {
        Debug.Log("PARABÉNS! O Puzzle foi concluído!");

        if (Chavecozinha != null)
        {
            Chavecozinha.SetActive(true);
            Debug.Log("Chave liberada!");
            // Ações de conclusão:
            // * Desativar os pratos/mesas
            // * Chamar um evento de fim de jogo/abrir porta, etc.
            // * Tocar som de vitória
        }
    }
}