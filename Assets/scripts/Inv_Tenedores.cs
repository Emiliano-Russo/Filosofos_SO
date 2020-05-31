using Assets;
using Assets.scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inv_Tenedores : MonoBehaviour
{

    [SerializeField]
    private GameObject[] tenedores;

    private Dictionary<Tenedor, Nro_Asiento> tenedor_ocupadoPor = new Dictionary<Tenedor, Nro_Asiento>(); // inventario de tenedores disponibles
    private Dictionary<Nro_Asiento, Par_Tenedores> nroAsiento_parTenedores = new Dictionary<Nro_Asiento, Par_Tenedores>(); // correspondencia inmodificable
    private void Start()
    {
        for (int i = 1; i <= 5; i++)
        {
            tenedor_ocupadoPor.Add((Tenedor)i, Nro_Asiento.Ningun_Asiento);
        }
        nroAsiento_parTenedores.Add(Nro_Asiento.Asiento_1, new Par_Tenedores(Tenedor.Tenedor_1, Tenedor.Tenedor_2));
        nroAsiento_parTenedores.Add(Nro_Asiento.Asiento_2, new Par_Tenedores(Tenedor.Tenedor_5, Tenedor.Tenedor_3));
        nroAsiento_parTenedores.Add(Nro_Asiento.Asiento_3, new Par_Tenedores(Tenedor.Tenedor_3, Tenedor.Tenedor_4));
        nroAsiento_parTenedores.Add(Nro_Asiento.Asiento_4, new Par_Tenedores(Tenedor.Tenedor_4, Tenedor.Tenedor_2));
    }

    private void Update()
    {
        foreach (KeyValuePair<Tenedor, Nro_Asiento> item in tenedor_ocupadoPor)
        {
            print(item.Key + " ) " + item.Value);
        }
    }

    internal bool PuedeComer(Nro_Asiento nro_asiento_consultado)
    {
        Par_Tenedores correspondencia = nroAsiento_parTenedores[nro_asiento_consultado];
        bool a = tenedor_ocupadoPor[correspondencia.tenedor_A] == nro_asiento_consultado;
        bool b = tenedor_ocupadoPor[correspondencia.tenedor_B] == nro_asiento_consultado;
        return a && b;      
    }

    internal void Solicitar_Tenedores_Semejantes(Nro_Asiento nro_asiento_solicitado)
    {
        Par_Tenedores correspondencia = nroAsiento_parTenedores[nro_asiento_solicitado];
        if (tenedor_ocupadoPor[correspondencia.tenedor_A] == Nro_Asiento.Ningun_Asiento)
        {
            tenedor_ocupadoPor[correspondencia.tenedor_A] = nro_asiento_solicitado;
            this.tenedores[(int)correspondencia.tenedor_A].SetActive(false);
        }
        if (tenedor_ocupadoPor[correspondencia.tenedor_B] == Nro_Asiento.Ningun_Asiento)
        {
            tenedor_ocupadoPor[correspondencia.tenedor_B] = nro_asiento_solicitado;
            this.tenedores[(int)correspondencia.tenedor_B].SetActive(false);
        }


    }

    internal void Desocupar_tenedores(Nro_Asiento nro_asiento)
    {
        if (nro_asiento == Nro_Asiento.Ningun_Asiento)
            return;

        Par_Tenedores correspondencia = nroAsiento_parTenedores[nro_asiento];
        if (tenedor_ocupadoPor[correspondencia.tenedor_A] == nro_asiento)
        {
            tenedor_ocupadoPor[correspondencia.tenedor_A] = Nro_Asiento.Ningun_Asiento;
            this.tenedores[(int)correspondencia.tenedor_A].SetActive(true);
        }
        if (tenedor_ocupadoPor[correspondencia.tenedor_B] == nro_asiento)
        {
            tenedor_ocupadoPor[correspondencia.tenedor_B] = Nro_Asiento.Ningun_Asiento;
            this.tenedores[(int)correspondencia.tenedor_B].SetActive(true);
        }
    }
}
