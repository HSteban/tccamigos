using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform cameraTransform;

    private float velocidadeMovimento;
    private float velocidadeNormal;
    public float velocidadeRapida;
    public float tempoMovimento;
    public float quantidadeRotacao;
    public Vector3 quantidadeZoom;

    public Vector3 novaPosicao;
    public Quaternion novaRotacao;
    public Vector3 novoZoom;

    public Vector3 dragPosInicial;
    public Vector3 dragPosAtual;
    public Vector3 rotPosInicial;
    public Vector3 rotPosAtual;

    

    // Start is called before the first frame update
    void Start()
    {
        novaPosicao = transform.position;
        novaRotacao = transform.rotation;
        novoZoom = cameraTransform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseInput();
        HandleMovementInput();
    }
    void HandleMouseInput()
    {

        if(Input.mouseScrollDelta.y != 0)
        {
            novoZoom += Input.mouseScrollDelta.y * quantidadeZoom;
        }
        if(Input.GetMouseButtonDown(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entrada;

            if(plane.Raycast(ray,out entrada))
            {
                dragPosInicial = ray.GetPoint(entrada);
            }
        }
        if (Input.GetMouseButton(0))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float entrada;

            if (plane.Raycast(ray, out entrada))
            {
                dragPosAtual = ray.GetPoint(entrada);

                novaPosicao = transform.position + dragPosInicial - dragPosAtual;
            }
        }

        if(Input.GetMouseButtonDown(2))
        {
            rotPosInicial = Input.mousePosition;
        }
        if(Input.GetMouseButton(2))
        {
            rotPosAtual = Input.mousePosition;

            Vector3 dif = rotPosInicial - rotPosAtual;

            rotPosInicial = rotPosAtual;

            novaRotacao *= Quaternion.Euler(Vector3.up * (-dif.x / 5f));
        }
    }
    void HandleMovementInput()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidadeMovimento = velocidadeRapida;
        }
        else
        {
            velocidadeMovimento = velocidadeNormal;
        }
        if(Input.GetKey(KeyCode.W))
        {
            novaPosicao += (transform.forward * velocidadeMovimento);
        }
        if (Input.GetKey(KeyCode.S))
        {
            novaPosicao += (transform.forward * -velocidadeMovimento);
        }
        if (Input.GetKey(KeyCode.D))
        {
            novaPosicao += (transform.right * velocidadeMovimento);
        }
        if (Input.GetKey(KeyCode.A))
        {
            novaPosicao += (transform.right * -velocidadeMovimento);
        }

        if(Input.GetKey(KeyCode.Q))
        {
            novaRotacao *= Quaternion.Euler(Vector3.up * quantidadeRotacao);
        }
        if(Input.GetKey(KeyCode.E))
        {
            novaRotacao *= Quaternion.Euler(Vector3.up * -quantidadeRotacao);
        }
        if(Input.GetKey(KeyCode.R))
        {
            novoZoom += quantidadeZoom;
        }
        if (Input.GetKey(KeyCode.F))
        {
            novoZoom -= quantidadeZoom;
        }

        transform.position = Vector3.Lerp(transform.position, novaPosicao, Time.deltaTime * tempoMovimento);
        transform.rotation = Quaternion.Lerp(transform.rotation, novaRotacao, Time.deltaTime * tempoMovimento);
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, novoZoom, Time.deltaTime * tempoMovimento);
    }
}
