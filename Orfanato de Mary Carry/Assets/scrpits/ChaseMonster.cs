using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ChaserMonster : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private AudioSource audioSource; // NOVO: Referência para o áudio
    private Transform playerTarget;
    private SkinnedMeshRenderer monsterRenderer;
    private bool isChasing = false;

    [Header("Configurações")]
    public float chaseSpeed = 6.0f;
    public string runningAnimationName = "correndo";

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>(); // NOVO: Obtém o AudioSource
        monsterRenderer = GetComponentInChildren<SkinnedMeshRenderer>();

        if (agent == null)
        {
            Debug.LogError("ERRO FATAL: NavMeshAgent não encontrado em Awake()!");
        }
    }

    // ... (O método Start() permanece o mesmo) ...
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
            audioSource.Play(); // NOVO: Toca o áudio
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

    // ... (O resto dos métodos permanecem o mesmo) ...
    private IEnumerator AppearAfterDelayAndStartChase()
    {
        yield return null;

        if (monsterRenderer != null)
        {
            monsterRenderer.enabled = true;
            Debug.Log("CHASER: Visibilidade forçada no próximo frame. Monstro visível.");
        }

        agent.Warp(transform.position);

        agent.speed = chaseSpeed;
        agent.isStopped = false;
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
            audioSource.Stop(); // Boa prática: para o som ao desaparecer
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