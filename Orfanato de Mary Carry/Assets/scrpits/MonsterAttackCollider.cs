using UnityEngine;

public class MonsterAttackCollider : MonoBehaviour
{
    private ChaserMonster chaserMonster;
    private bool hasAttacked = false;

    void Start()
    {
        // Pega a referência para o script principal do monstro
        chaserMonster = GetComponent<ChaserMonster>();
        if (chaserMonster == null)
        {
            Debug.LogError("MonsterAttackCollider requer o script ChaserMonster no mesmo objeto!");
            enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 1. Verifica se o monstro ainda não atacou (evita múltiplos ataques)
        if (hasAttacked)
            return;

        // 2. Verifica se o objeto que colidiu é o jogador
        // (Assumindo que seu jogador tem a tag "Player")
        if (other.gameObject.CompareTag("MainCamera"))
        {
            hasAttacked = true;
            Debug.Log("Jogador detectado! Iniciando ataque...");

            // Chama o método no script principal
            chaserMonster.AttackPlayer();

            // Desativa este script para garantir que não haja mais triggers
            enabled = false;
        }
    }
}