using UnityEngine;

public class EndChaseTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Tenta pegar o script ChaserMonster no objeto que entrou
        ChaserMonster chaser = other.GetComponent<ChaserMonster>();

        if (chaser != null)
        {
            Debug.Log("CHASER: O monstro foi detectado e desaparecerá.");

            // Se for o monstro perseguidor, chama a função Vanish
            chaser.Vanish();

            // Destrói este Trigger
            Destroy(gameObject);
        }
    }
}