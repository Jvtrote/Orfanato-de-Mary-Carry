using UnityEngine;

public class MonsterActivator : MonoBehaviour
{
    [Header("Monstro a Ativar")]
    // Arraste o GameObject do monstro aqui no Inspector
    public MonsterRunner monsterRunner; // Referência ao novo script

    private bool hasBeenActivated = false;
    private const string PlayerTag = "MainCamera";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(PlayerTag) && !hasBeenActivated)
        {
            if (monsterRunner != null)
            {
                // Chama a função StartRunning no script do monstro
                monsterRunner.StartRunning();

                hasBeenActivated = true;

                // Desativa o collider da área para que não seja acionado novamente
                GetComponent<Collider>().enabled = false;
            }
            else
            {
                Debug.LogError("Monster Runner não está atribuído no Activator!");
            }
        }
    }
}
