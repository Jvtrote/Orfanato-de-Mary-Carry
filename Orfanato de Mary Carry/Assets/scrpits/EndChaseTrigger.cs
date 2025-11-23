using UnityEngine;

public class EndChaseTrigger : MonoBehaviour
{
    // Este Trigger detecta APENAS o monstro perseguidor
    private void OnTriggerEnter(Collider other)
    {
        // Tenta pegar o script ChaserMonster no objeto que entrou
        ChaserMonster chaser = other.GetComponent<ChaserMonster>();

        if (chaser != null)
        {
            // Se for o monstro perseguidor, chama a função Vanish
            chaser.Vanish();

            Debug.Log("Monstro sumiu ao entrar na área de fim de perseguição.");

            // Destrói este Trigger para que ele não seja ativado novamente
            Destroy(gameObject);
        }
    }
}