using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BarraDeVida : MonoBehaviour
{
    public Image barraDeHP;

    private float vidaAtual = 150;
    private float vidaTotal = 150;
    void Start()
    {
        UpdateHealth();
    }

    private void UpdateHealth()
    {
        float ratio = vidaAtual / vidaTotal;
        barraDeHP.rectTransform.localScale = new Vector3(ratio, 1, 1);
    }

    private void tomaDano(float danoDoAtaque)
    {
        vidaAtual -= danoDoAtaque;
        if(vidaAtual < 0)
        {
            vidaAtual = 0;
        }

        UpdateHealth();
    }

}
