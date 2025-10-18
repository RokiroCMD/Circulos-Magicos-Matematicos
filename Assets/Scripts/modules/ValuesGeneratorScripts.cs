using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValuesGeneratorScripts : MonoBehaviour
{

    [SerializeField] public List<int> numsCMM = new List<int>();

    public List<int> values = new List<int>();
    [SerializeField] int maxValue;

    [SerializeField] int maxPool;
    [SerializeField] int fillPorcentage = 0;

    public static ValuesGeneratorScripts Instance;
    int quantity = 0;
    public bool isValidValue(int v)
    {
        return numsCMM.Contains(v);
    }

    private void Awake()
    {
        if (Instance != null)
        {

        }
        else
        {
            Instance = this;
        }
    }


    public int getRandomValue()
    {
        if (values.Count < 1)
        {
            FillValues();
        }

        int indiceAleatorio = Random.Range(0, values.Count);

        // Obtener y mostrar el valor aleatorio
        int valorAleatorio = values[indiceAleatorio];
        values.RemoveAt(indiceAleatorio);
        return valorAleatorio;
    }

    public void removeValue(int n)
    {
        for (int i = 0; i < values.Count; i++)
        {
            if (values[i].Equals(n))
            {
               
                values.RemoveAt(i);
                quantity--;
                return;
            }
        }
    }

    public bool hasValuesLeft()
    {
        if (quantity < 1)
        {
            return false;
        }
        return true;
    }

    public void FillValues()
    {
        values = new List<int>();
        for (int i = 0; i < maxPool; i++)
        {
            values.Add(Random.Range(1, maxValue));
        }

        if (!(values.Contains(numsCMM[0]) || values.Contains(numsCMM[1]) || values.Contains(numsCMM[2]) || values.Contains(numsCMM[3])))
        {
            values.RemoveAt(0);
            values.Add(numsCMM[Random.Range(0, numsCMM.Count)]);
        }

        quantity = 0;
        for (int i = 0; i < values.Count; i++)
        {
            if (values[i] == numsCMM[0] || values[i] == numsCMM[1] || values[i] == numsCMM[2] || values[i] == numsCMM[3])
            {
                quantity++;
            }
        }
        if (fillPorcentage > 0)
        {
            for (int i = 0; i < values.Count; i++)
            {
                float actualPorcentage = ((float)quantity / ((float) values.Count)) *  100f;

                if (actualPorcentage <= fillPorcentage)
                {
                    changeValue(i);
                }
            }
        }
    }

    public void changeValue(int i)
    {
        if (!isValidValue(values[i]))
        {
            values.RemoveAt(i);
            values.Insert(i, numsCMM[Random.Range(0, numsCMM.Count)]);
            quantity++;
        }
        else
        {
            changeValue(i + 1);
        }

    }

}
