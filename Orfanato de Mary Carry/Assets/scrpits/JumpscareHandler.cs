using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class JumpscareHandler : MonoBehaviour
{
    private Animator animator;
    private SkinnedMeshRenderer monsterRenderer;

    [Header("Configurações do Susto")]
    public string scareAnimationName = "Susto";
    public float scareDuration = 1.5f;

    [Header("Configuração de Transição")]
    public GameObject chaserMonsterPrefab;
    public Transform playerTarget;

    void Start()
    {
        animator = GetComponent<Animator>();
        monsterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (monsterRenderer == null)
        {
            Debug.LogError("JumpscareHandler: Renderizador do monstro de susto não encontrado.");
            enabled = false;
            return;
        }

        // Esconde o monstro no início do jogo
        monsterRenderer.enabled = false;
    }

    public void StartScareAndVanish()
    {
        monsterRenderer.enabled = true;
        StartCoroutine(ScareSequence());
    }

    private IEnumerator ScareSequence()
    {
        // 1. Dispara a animação de susto
        if (animator != null && !string.IsNullOrEmpty(scareAnimationName))
        {
            animator.SetTrigger(scareAnimationName);
        }

        // 2. Espera o tempo definido para a animação de susto
        yield return new WaitForSeconds(scareDuration);

        // --- 3. Geração do Monstro Perseguidor (Spawn) ---
        if (chaserMonsterPrefab == null || playerTarget == null)
        {
            Debug.LogError("ERRO CRÍTICO: Prefab ou Target do Jogador não atribuído no Inspector!");
            Vanish();
            yield break;
        }

        Vector3 spawnPosition = transform.position;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(spawnPosition, out hit, 1.0f, NavMesh.AllAreas))
        {
            GameObject chaserMonster = Instantiate(chaserMonsterPrefab, hit.position, transform.rotation);

            ChaserMonster chaserScript = chaserMonster.GetComponent<ChaserMonster>();
            if (chaserScript != null)
            {
                chaserScript.StartChasing(playerTarget);
            }
            else
            {
                Debug.LogError("O Prefab atribuído não tem o script ChaserMonster.cs!");
            }
        }
        else
        {
            Debug.LogError("NavMesh.SamplePosition falhou: Não encontrou ponto válido para spawn!");
        }

        // 4. Torna o monstro de susto invisível e o destrói
        // NOVO: Desativa o GameObject pai imediatamente para resolver o problema de "fusão" visual
        gameObject.SetActive(false);

        Vanish();
    }

    private void Vanish()
    {
        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = false;
        }

        // Destrói o GameObject
        Destroy(gameObject, 0.1f);
        enabled = false;
    }
}