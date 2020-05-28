using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House_Manager : MonoBehaviour
{
    private float contador = 0;
    private bool quitando_filosofo = false;
    private Filosofo logic_filosofo_dentro;
    private Filosofo logic_filosofo_fuera;

    public GameObject[] Filosofo;

    // Update is called once per frame
    void Update()
    {
        contador += Time.deltaTime;
        if (contador >= 5)
        {
            Quitar_Aleatorio_un_Filosofo();
        }
        
    }

    private void Quitar_Aleatorio_un_Filosofo()
    {

        if (!quitando_filosofo)
        {
            quitando_filosofo = true;
            int nro_random = 2;//hacemos un random de los que estan dentro
            logic_filosofo_dentro = Filosofo[nro_random].GetComponent<Filosofo>();
        }

        if (logic_filosofo_dentro.en_casa)
        {
            logic_filosofo_dentro.Salir_Casa();
        }
        else
        {
            contador = 0;
            quitando_filosofo = false;
            logic_filosofo_dentro = null;
        }
    }

    

    private void Resetear_Contador()
    {

    }
}
