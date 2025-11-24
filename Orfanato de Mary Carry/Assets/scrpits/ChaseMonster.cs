using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChaserMonster : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource;
    private Transform playerTarget;
    private SkinnedMeshRenderer monsterRenderer;
    private bool isChasing = false;

    [Header("Configurações")]
    public float chaseSpeed = 6.0f;
    public string runningAnimationName = "correndo";

    [Header("Configurações de Ataque e Fim de Jogo")]
    public string attackAnimationName = "Atacando"; // NOVO: Nome da animação
    public GameObject gameManager; // NOVO: Referência ao GameObject GameManager

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        monsterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (agent == null)
        {
            Debug.LogError("ERRO FATAL: NavMeshAgent não encontrado em Awake()!");
        }
    }

    void Start()
    {
        if (agent == null)
        {
            enabled = false;
            return;
        }

        gameObject.SetActive(true);
        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = false;
        }

        agent.isStopped = true;
    }

    public void StartChasing(Transform target)
    {
        if (agent == null)
        {
            return;
        }

        playerTarget = target;
        isChasing = true;

        // 1. Inicia o som da perseguição
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.Play();
        }

        // Inicia a Coroutine que liga a visibilidade e o movimento
        if (monsterRenderer != null)
        {
            StartCoroutine(AppearAfterDelayAndStartChase());
        }

        // 3. Inicia a animação
        if (animator != null)
        {
            animator.SetBool(runningAnimationName, true);
        }
    }

    // NOVO: Método chamado quando o monstro toca no jogador
    public void AttackPlayer()
    {
        // 1. Para toda a lógica de movimento
        isChasing = false;
        if (agent != null)
        {
            agent.isStopped = true;
        }

        // 2. Toca a animação de ataque
        if (animator != null && !string.IsNullOrEmpty(attackAnimationName))
        {
            animator.SetTrigger(attackAnimationName);
        }

        // 3. Chama a função de Game Over no GameManager
        if (gameManager != null)
        {
            // Envia uma mensagem ao script GameManager para iniciar a tela de Game Over
            gameManager.SendMessage("EndGame", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            Debug.LogError("CHASER: O GameObject GameManager não foi atribuído no Inspector!");
        }

        // 4. Para o som e destrói o objeto após a animação
        if (audioSource != null)
        {
            audioSource.Stop();
        }
        Destroy(gameObject, 3.0f); // Dá 3s para a animação de ataque rodar
    }

    private IEnumerator AppearAfterDelayAndStartChase()
    {
        yield return null;

        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = true;
            Debug.Log("CHASER: Visibilidade forçada no próximo frame. Monstro visível.");
        }

        if (agent != null)
        {
            agent.Warp(transform.position);
            agent.speed = chaseSpeed;
            agent.isStopped = false;
        }
    }

    void Update()
    {
        if (isChasing && playerTarget != null && agent != null)
        {
            agent.SetDestination(playerTarget.position);
        }
    }

    public void Vanish()
    {
        isChasing = false;

        if (audioSource != null)
        {
            audioSource.Stop();
        }

        if (animator != null)
        {
            animator.SetBool(runningAnimationName, false);
        }

        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = false;
        }

        Destroy(gameObject);
    }
}