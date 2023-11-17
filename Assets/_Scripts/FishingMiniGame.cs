using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishingMiniGame : MonoBehaviour
{
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;

    [Header("Fish Movement Data")]
    [SerializeField] Transform fish;

    float fishPosition;
    float fishDestination;

    //Timer Kapan Ikan Pindah Posisi
    float fishTimer;
    [SerializeField] float timerMultiplicator = 3f;

    float fishSpeed;
    [SerializeField] float smoothMotion = 1f;


    [Header("Hook Movement Data")]
    [SerializeField] Transform hook;
    [SerializeField] SpriteRenderer hookSpriteRenderer;

    float hookPosition;
    //Size Bar Hijau
    [SerializeField] float hookSize = 0.1f;

    //Kekuatan buat Naekin Progress
    [SerializeField] float hookPower = 0.5f;
    float hookProgress;

    //Kekuatan Narik Bar Hijau
    float hookPullVelocity;
    [SerializeField] float hookPullPower = 0.01f;
    [SerializeField] float hookGravityPower = 0.005f;

    //Kekuatan buat Nurunin Progress
    [SerializeField] float hookProgressDegradationPower = 0.1f;

    [Header("Progress Bar Data")]
    [SerializeField] Transform progressBarContainer;
    bool pause = false;

    [SerializeField] float failTimer = 10f;

    private void Start()
    {
        Resize();
    }

    private void Update()
    {
        if (pause)
        {
            return;
        }

        Fish();
        Hook();
        ProgressCheck();
    }

    public void Resize()
    {
        Bounds b = hookSpriteRenderer.bounds;
        float ySize = b.size.y;
        Vector3 ls = hook.localScale;
        float distance = Vector3.Distance(topPivot.position, bottomPivot.position);
        ls.y = (distance / ySize * hookSize);
        hook.localScale = ls;
    }

    private void ProgressCheck()
    {
        Vector3 ls = progressBarContainer.localScale;
        ls.y = hookProgress;
        progressBarContainer.localScale = ls;

        float min = hookPosition - hookSize / 2;
        float max = hookPosition + hookSize / 2;

        if (min < fishPosition && fishPosition < max)
        {
            hookProgress += hookPower * Time.deltaTime;
        }
        else
        {
            hookProgress -= hookProgressDegradationPower * Time.deltaTime;

            failTimer -= Time.deltaTime;
            if (failTimer < 0f)
            {
                FailedCatch();
            }
        }

        //Ikan Didapatkan
        if (hookProgress >= 1f)
        {
            Catched();
        }

        hookProgress = Mathf.Clamp(hookProgress, 0f, 1f);
    }

    private void Catched()
    {
        pause = true;
        Debug.Log("WIN, YOU CATCH A FISH");
    }

    private void FailedCatch()
    {
        pause = true;
        Debug.Log("LOSE, FISH FLEE");
    }

    void Hook()
    {
        if (Input.GetMouseButton(0))
        {
            //Untuk Naekin Velocity Hooknya kalau ada Input
            hookPullVelocity += hookPullPower * Time.deltaTime;
        }

        //Untuk Nurunin Velocity Hooknya kalau tidak ada Input
        hookPullVelocity -= hookGravityPower * Time.deltaTime;

        hookPosition += hookPullVelocity;

        if (hookPosition - hookSize / 2 <= 0f && hookPullVelocity < 0f)
        {
            hookPullVelocity = 0f;
        }

        if (hookPosition + hookSize / 2 >= 1f && hookPullVelocity > 0f)
        {
            hookPullVelocity = 0f;
        }

        hookPosition = Mathf.Clamp(hookPosition, hookSize / 2, 1 - hookSize / 2);
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPosition); 
    }

    void Fish()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer < 0f)
        {
            fishTimer = Random.value * timerMultiplicator;

            fishDestination = Random.value;
        }

        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }
}
