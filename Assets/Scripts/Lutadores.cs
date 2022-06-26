using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lutadores : MonoBehaviour
{
    //animações
    private Animator anim;
    private string estadoAtual;

    const string soco = "Punch";
    const string socoGancho = "HookPunch";
    const string parado = "IdlePose";

    private bool atacando = false;
    [SerializeField] float delayDeAtaque = 0.3f;

    public bool humano;

    private CharacterController controle;
    private Vector3 movementon;
    private float powerOfJump;
    float str = 0;

    public SphereCollider[] hitBoxesDeAtaque;
 
    void Start()
    {
        controle = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (humano)
        {
            if (!atacando) 
            {
                if (Input.GetKeyDown(KeyCode.G)) //Ativa o ataque 1
                {
                    atacando = true;
                    atacar(hitBoxesDeAtaque[0]);
                    mudaAnimacao(soco);
                    Invoke(nameof(estaAtacando), delayDeAtaque);
                }
                if (Input.GetKeyDown(KeyCode.H)) //Ativa o ataque 2
                {
                    atacando = true;
                    atacar(hitBoxesDeAtaque[1]);
                    mudaAnimacao(socoGancho);
                    Invoke(nameof(estaAtacando), delayDeAtaque);
                }
            }

            if (movementon.x == 0 && !atacando)
            {
                mudaAnimacao(parado);
            }

            if (controle.isGrounded && !atacando) // Verifica se o personagem está no chão
            {
                powerOfJump = -5;

                if (Input.GetKeyDown(KeyCode.W)) // Assiona o pulo
                {
                    powerOfJump = 10;
                }
            }
            else // Realiza o pulo
            {
                powerOfJump -= 14 * Time.deltaTime;
            }

            //Controla a movimentação (44 a 48)
            if (!atacando) 
            { 
                movementon = Vector3.zero;
                movementon.x = Input.GetAxis("Horizontal") * 5;
                movementon.y = powerOfJump;

                controle.Move(movementon * Time.deltaTime);
            }
        }
        else
        {

        }
    }

    void estaAtacando()
    {
        atacando = false;
    }

    private void atacar(SphereCollider col)
    {
        Collider[] cols = Physics.OverlapSphere(col.bounds.center, col.radius, LayerMask.GetMask("HurtBox"));
        foreach (Collider c in cols)
        {
            if (c.transform.parent.parent == transform)
                continue;

            float dano = 0;
            switch (c.name)
            {
                case "HurtCabeça":
                    dano = str;
                    break;
                case "HurtTorax":
                    dano = str + 3;
                    break;
                default:
                    Debug.Log("algo errado aconteceu");
                    break;
            }
            c.SendMessageUpwards("tomaDano", dano);
        }
    }
    void mudaAnimacao(string novoEstado)
    {
        if (estadoAtual == novoEstado) return;

        anim.Play(novoEstado);

        estadoAtual = novoEstado;

    }
}
