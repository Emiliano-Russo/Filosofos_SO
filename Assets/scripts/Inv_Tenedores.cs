using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inv_Tenedores : MonoBehaviour
{


    private GameObject[] tenedores;

 
    public void Ocupar_Tenedor(int nro)
    {
        tenedores[nro].SetActive(true);
    }

    public void Liberar_Tenedor(int nro)
    {
        tenedores[nro].SetActive(false);
    }

}
