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
    [SerializeField]
    private Image[] tenedores_img;

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
            tenedores_img[(int)correspondencia.tenedor_A].color = Color_Lugar(nro_asiento_solicitado);
        }
        if (tenedor_ocupadoPor[correspondencia.tenedor_B] == Nro_Asiento.Ningun_Asiento)
        {
            tenedor_ocupadoPor[correspondencia.tenedor_B] = nro_asiento_solicitado;
            this.tenedores[(int)correspondencia.tenedor_B].SetActive(false);
            tenedores_img[(int)correspondencia.tenedor_B].color = Color_Lugar(nro_asiento_solicitado);
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
            tenedores_img[(int)correspondencia.tenedor_A].color = Color.black;
        }
        if (tenedor_ocupadoPor[correspondencia.tenedor_B] == nro_asiento)
        {
            tenedor_ocupadoPor[correspondencia.tenedor_B] = Nro_Asiento.Ningun_Asiento;
            this.tenedores[(int)correspondencia.tenedor_B].SetActive(true);
            tenedores_img[(int)correspondencia.tenedor_B].color = Color.black;
        }
    }

    private Color Color_Lugar(Nro_Asiento nro_asiento_solicitado)
    {
        int nroAsiento = (int)nro_asiento_solicitado;

        switch (nroAsiento)
        {
            case 1:
                return Color.green;
                break;
            case 2:
                return Color.blue;
                break;
            case 3:
                return Color.yellow;
                break;
            case 4:
                return Color.red;
                break;
            default:
                throw new Exception("Nro Lugar no detectado: " + nroAsiento);
        }
    }
}
