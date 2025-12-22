using UnityEngine;
using UnityEngine.Formats.Alembic.Importer;
using System.Collections;

public class AlembicClothingChanger : MonoBehaviour
{
    public AlembicStreamPlayer shirt1;
    public AlembicStreamPlayer shirt2;

    private Coroutine currentAnimationCoroutine;
    private AlembicStreamPlayer currentPlayer;

    void Start()
    {
        // Initially hide both shirts
        shirt1.gameObject.SetActive(false);
        shirt2.gameObject.SetActive(false);
        currentPlayer = null;
    }

    // --- BUTTON: Shirt 1 ---
    public void ShowShirt1()
    {
        if (currentPlayer == shirt1) return; // already active

        SwitchToShirt(shirt1);
    }

    // --- BUTTON: Shirt 2 ---
    public void ShowShirt2()
    {
        if (currentPlayer == shirt2) return; // already active

        SwitchToShirt(shirt2);
    }

    // --- BUTTON: No Shirt ---
    public void RemoveAllClothing()
    {
        if (currentAnimationCoroutine != null)
            StopCoroutine(currentAnimationCoroutine);

        if (shirt1 != null) shirt1.gameObject.SetActive(false);
        if (shirt2 != null) shirt2.gameObject.SetActive(false);

        currentPlayer = null;
        Debug.Log("All clothing removed.");
    }

    // Switch helper
    private void SwitchToShirt(AlembicStreamPlayer newShirt)
    {
        if (currentAnimationCoroutine != null)
            StopCoroutine(currentAnimationCoroutine);

        // Disable both first to ensure only one shows
        shirt1.gameObject.SetActive(false);
        shirt2.gameObject.SetActive(false);

        // Enable selected one
        newShirt.gameObject.SetActive(true);
        currentPlayer = newShirt;

        PlayAnimation(newShirt);
    }

    private void PlayAnimation(AlembicStreamPlayer player)
    {
        player.CurrentTime = 0f;
        currentAnimationCoroutine = StartCoroutine(PlayAlembicAnimation(player));
    }

    IEnumerator PlayAlembicAnimation(AlembicStreamPlayer player)
    {
        while (player.CurrentTime < player.Duration)
        {
            player.CurrentTime += Time.deltaTime;
            yield return null;
        }

        player.CurrentTime = player.Duration;
    }
}
