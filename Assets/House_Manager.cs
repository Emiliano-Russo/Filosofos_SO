using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class House_Manager : MonoBehaviour
{
    private float segundos = 0;
    private Filosofo logic_filosofo_dentro;
    private Filosofo logic_filosofo_fuera;
    private bool ya_lo_hicimos;

    public GameObject[] Filosofos;


    private void Start()
    {
        logic_filosofo_fuera = Filosofos[5].GetComponent<Filosofo>();
    }

    void Update()
    {
        segundos += Time.deltaTime;
        if (segundos >= 5)
        {
            Asignar_Filosofo_Dentro_Random();
            Cambio_de_Filosofo();
        }
        else
            ya_lo_hicimos = false;


    }

    private void Asignar_Filosofo_Dentro_Random()
    {
        if (!ya_lo_hicimos)
        {
            int[] ignore = { Nro_Filosofo_Fuera() };
            Random_Number_Generator.Roll_A(1, 6, ignore);
            int nro_random = Random_Number_Generator.nro_A;
            logic_filosofo_dentro = Filosofos[nro_random].GetComponent<Filosofo>();
            ya_lo_hicimos = true;
        }

    }

    private void Cambio_de_Filosofo()
    {
        if (logic_filosofo_dentro.estoy_en_casa == true || logic_filosofo_fuera.estoy_en_casa == false)
        {
            Quitar_Filosofo_Dentro();
            Entrar_al_Filosofo_de_Afuera();
        }
        else
        {
            logic_filosofo_fuera = logic_filosofo_dentro;
            logic_filosofo_dentro = null;
            segundos = 0;
        }
    }

    private void Entrar_al_Filosofo_de_Afuera()
    {
        logic_filosofo_fuera.Entrar_Casa();
    }

    private void Quitar_Filosofo_Dentro()
    {
        logic_filosofo_dentro.Salir_Casa();
    }

    private int Nro_Filosofo_Fuera()
    {
        for (int i = 1; i <= 5; i++)
        {
            if (!Filosofos[i].GetComponent<Filosofo>().estoy_en_casa)
                return i;
        }
        throw new Exception("No hay ningun filosofo afuera");
    }
}
