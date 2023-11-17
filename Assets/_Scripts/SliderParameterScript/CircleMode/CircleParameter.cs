using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleParameter : MonoBehaviour
{
    public Image _bar;
    public RectTransform button;
    public List<ParameterHolder> parameterHolder;
    public ParameterHolder selectedParam;

    public float _barValue = 0;

    private float timePassed = 0.0f;
    public float speed = 1.0f;
    public float maxValue = 100.0f; // Maksimum nilai barValue

    private bool isIncreasing = true; // Menandakan apakah sedang bergerak maju atau mundur

    public bool isTicking = true;

    private void Start()
    {
        RandomingParameter();
    }

    public void RandomingParameter()
    {
        for (int i = 0; i < parameterHolder.Count; i++)
        {
            parameterHolder[i].gameObject.SetActive(false);
        }

        int randomParam = UnityEngine.Random.Range(0, parameterHolder.Count);
        selectedParam = parameterHolder[randomParam];
        selectedParam.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTicking)
        {
            // Perhitungan nilai barValue berdasarkan waktu
            timePassed += Time.deltaTime * speed;
            if (timePassed > 2.0f) // Ganti arah setiap 2 detik
            {
                isIncreasing = !isIncreasing;
                timePassed = 0.0f;
            }

            if (isIncreasing)
            {
                _barValue = Mathf.Lerp(0, maxValue, timePassed / 2.0f);
            }
            else
            {
                _barValue = Mathf.Lerp(maxValue, 0, timePassed / 2.0f);
            }

            BarValueChanged(_barValue);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            isTicking = false;
            CheckingParameterCondition();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            isTicking = true;
            RandomingParameter();
        }
    }

    private void BarValueChanged(float barValue)
    {
        float amount = (barValue / maxValue) * 180.0f / 360;
        _bar.fillAmount = amount;
        float buttonAngle = amount * 360;
        button.localEulerAngles = new Vector3(0, 0, -buttonAngle);
    }

    public void CheckingParameterCondition()
    {
        if (_barValue <= selectedParam.maxValue && _barValue >= selectedParam.minValue)
        {
            Debug.Log("WIN");
        }
        else
        {
            Debug.Log("LOSE");
        }
    }
}
