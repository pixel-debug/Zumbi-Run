using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zumbi : MonoBehaviour
{
    public float velocidade = 5;
    public float velocidadePulo;
    private Animator animacao;
    public float aux;
    public CharacterController player;
    private Vector3 movimento;
    private bool morreu;

    public int pontos = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        animacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // corre sem parar
        //   transform.Translate(Input.GetAxis("Horizontal")*aux*Time.deltaTime, 0,1 *velocidade );

        movimento.x = Input.GetAxis("Horizontal") * Time.deltaTime * aux;
        movimento.z = 1 * aux * Time.deltaTime;

        if (player.isGrounded)
        {
            movimento.y = 0;
        }
        else
        {
            movimento.y -= 8.45f * Time.deltaTime;      // para ele cair
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {

            animacao.SetTrigger("pular");

            movimento.y = 1 * velocidadePulo * Time.deltaTime;

        }


        if (Input.GetKey(KeyCode.DownArrow))              // testar
        {

            animacao.SetTrigger("baixar");

            //movimento.y = 1 * velocidadePulo * Time.deltaTime;

        }


        player.Move(movimento);
    }

    void Default()
    {
        player.center = new Vector3(0, player.center.y, player.center.z);
    }

    void OnControllerColliderHit(ControllerColliderHit bateu)    //arvore
    {
        if (bateu.transform.tag == "Obstaculo" && !morreu)
        {

            morreu = true;
            animacao.SetTrigger("morte");
            //morte do avatar
        }
        if (bateu.transform.tag == "ObstaculoLeve")
        {
            animacao.SetTrigger("lento");           //mudar o trigger
            // deixa ele lento ?????
        }


    }
    public void Lentidao()
    {
        movimento.z = 0.5f * aux * Time.deltaTime;
    }
    public void Restart()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Score : " + pontos);
    }
}
