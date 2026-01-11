using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LifeUIController : MonoBehaviour
{
    public static LifeUIController Instance;

    [Header("Life Icons (Fill)")]
    [SerializeField] private Image[] lifeIcons;
    // Kéo 3 Icon_Life vào theo thứ tự từ trên xuống

    [Header("Flash")]
    [SerializeField] private Color healColor = Color.green;
    [SerializeField] private float flashDuration = 0.1f;
    [SerializeField] private int flashCount = 3;

    private int maxHP = 3;
    private int currentHP;

    public Action OnDoneDamageUI;

    // ===== QUEUE =====
    private int pendingDamage;
    private int pendingHeal;
    private bool isProcessing;

    // ===== FAST FORWARD =====
    private bool skipAnimation;

    #region Unity
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        maxHP = lifeIcons.Length;
        currentHP = maxHP;
        InitUI();
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        ResetColors();
        isProcessing = false;
    }
    #endregion

    // =========================
    // INIT
    // =========================
    private void InitUI()
    {
        for (int i = 0; i < lifeIcons.Length; i++)
        {
            lifeIcons[i].gameObject.SetActive(i < currentHP);
            lifeIcons[i].color = Color.white;
        }
    }

    // =========================
    // PUBLIC API
    // =========================
    public void TakeDamage(int amount = 1)
    {
        if (currentHP <= 0) return;

        pendingDamage++;
        skipAnimation = true;          // fast-forward visual hiện tại
        TryProcess();
    }

    public void Heal(int amount = 1)
    {
        if (currentHP >= maxHP) return;

        pendingHeal++;
        skipAnimation = true;          // fast-forward visual hiện tại
        TryProcess();
    }

    private void TryProcess()
    {
        if (!isProcessing)
            StartCoroutine(ProcessQueue());
    }

    // =========================
    // PROCESS QUEUE (LOGIC FIRST)
    // =========================
    private IEnumerator ProcessQueue()
    {
        isProcessing = true;
        yield return null;

        while ((pendingDamage > 0 || pendingHeal > 0) && currentHP > 0)
        {
            // reset skip cho action mới
            skipAnimation = false;

            // ===== DAMAGE =====
            if (pendingDamage > 0 && currentHP > 0)
            {
                int index = currentHP - 1;

                pendingDamage--;
                currentHP--;
                if(currentHP <= 0)
                {
                    flashCount = 1;
                    flashDuration = 0;
                }

                lifeIcons[index].gameObject.SetActive(true);

                yield return Flash(lifeIcons[index], healColor);

                lifeIcons[index].gameObject.SetActive(false);
            }
            // ===== HEAL =====
            else if (pendingHeal > 0 && currentHP < maxHP)
            {
                int index = currentHP;

                pendingHeal--;
                currentHP++;

                lifeIcons[index].gameObject.SetActive(true);

                yield return Flash(lifeIcons[index], healColor);
            }

            yield return null;
        }

        isProcessing = false;
        OnDoneDamageUI?.Invoke();
    }

    // =========================
    // FLASH (FAST-FORWARDABLE)
    // =========================
    private IEnumerator Flash(Image img, Color color)
    {
        img.color = Color.white;

        for (int i = 0; i < flashCount; i++)
        {
            if (skipAnimation) break;

            img.color = color;
            yield return WaitOrSkip(flashDuration);

            img.color = Color.white;
            yield return WaitOrSkip(flashDuration);
        }

        img.color = Color.white;
    }

    private IEnumerator WaitOrSkip(float duration)
    {
        float t = 0f;
        while (t < duration)
        {
            if (skipAnimation) yield break;
            t += Time.deltaTime;
            yield return null;
        }
    }

    private void ResetColors()
    {
        foreach (var img in lifeIcons)
            img.color = Color.white;
    }
}
