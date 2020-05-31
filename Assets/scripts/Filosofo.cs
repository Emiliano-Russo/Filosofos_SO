using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using System;
using UnityEngine.UI;
using Assets.scripts;

public class Filosofo : MonoBehaviour
{


    private bool llegue_a_puerta;
    private bool llegue_al_destino;
    private float distancia_min_para_llegar = 0.05f;
    private float velocidad_caminando = 1;
    private Puntos puntos;
    private Inv_Tenedores inv_tenedores;

    public Nro_Asiento nro_asiento_ocupado;
    public bool estoy_en_casa;
    public bool estoy_comiendo;
    public Text lugar_ocupado_txt;
    public Animator anim;




    private void Start()
    {
        inv_tenedores = GameObject.Find("Tenedores_Manager").GetComponent<Inv_Tenedores>();
        anim = GetComponent<Animator>();
        puntos = GameObject.Find("Puntos").GetComponent<Puntos>();
        estoy_comiendo = false;
    }


    private void Update()
    {
        lugar_ocupado_txt.text = "lugar ocupado (" + transform.name + "): " + nro_asiento_ocupado;

        if (estoy_en_casa && !estoy_comiendo && nro_asiento_ocupado != Nro_Asiento.Ningun_Asiento)
        {
            Intentar_Comer();
        }

    }

    private void Intentar_Comer()
    {
        if (inv_tenedores.PuedeComer(nro_asiento_ocupado))
            anim.SetBool("Comer", true);
        else
            inv_tenedores.Solicitar_Tenedores_Semejantes(nro_asiento_ocupado);
    }

    public void Entrar_Casa()
    {
        if (!estoy_en_casa)
        {
            Ingresar_Casa();
        }
    }

    public void Salir_Casa()
    {
        if (estoy_en_casa)
        {
            IrseDeCasa();
        }
    }




    //hacer la ruta para ingresar a casa
    private void Ingresar_Casa()
    {
        anim.SetBool("Caminar", true);
        if (!llegue_a_puerta)
        {
            Ir_Hasta_La_Puerta();
        }
        else if (llegue_a_puerta && !llegue_al_destino)
        {
            Ir_Hasta_Un_Lugar_Disponible();
        }
        else if (llegue_a_puerta && llegue_al_destino)
        {
            anim.SetBool("Caminar", false);
            Situar_Variables_Modo_Dentro_de_Casa();
        }
    }

    private void Ir_Hasta_La_Puerta()
    {

        llegue_a_puerta = Vector3.Distance(puntos.punto_salida.transform.position, transform.position) < distancia_min_para_llegar;
        transform.LookAt(puntos.punto_salida.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, puntos.punto_salida.transform.position, Time.deltaTime * velocidad_caminando);
    }

    bool get_lugarlibre_devuelto = false;
    Lugar_Libre_ClaseParametro lugar_libre = null;
    private void Ir_Hasta_Un_Lugar_Disponible()
    {
        Aux_GetLugarLibre();
        int asiento_a_ocupar = lugar_libre.lugar_libre_nro;
        GameObject lugar_punto = lugar_libre.lugar_libre_punto;
        Moverse_al_Lugar(lugar_punto, asiento_a_ocupar);
    }

    private void Aux_GetLugarLibre()
    {
        if (!get_lugarlibre_devuelto)
        {
            get_lugarlibre_devuelto = true;
            lugar_libre = puntos.Get_Un_Lugar_Libre();
        }
    }

    private void Moverse_al_Lugar(GameObject lugar_punto, int asiento_a_ocupar)
    {
        llegue_al_destino = Vector3.Distance(lugar_punto.transform.position, transform.position) < distancia_min_para_llegar;
        transform.LookAt(lugar_punto.transform.position);
        if (llegue_al_destino)
            puntos.Set_Lugar_Ocupado(asiento_a_ocupar);
        transform.position = Vector3.MoveTowards(transform.position, lugar_punto.transform.position, Time.deltaTime * velocidad_caminando);
    }

    private void Situar_Variables_Modo_Dentro_de_Casa()
    {
        nro_asiento_ocupado = (Nro_Asiento)lugar_libre.lugar_libre_nro;
        transform.LookAt(puntos.get_centro_de_la_mesa.transform.position);
        estoy_en_casa = true;
        llegue_al_destino = false;
        llegue_a_puerta = false;
        lugar_libre = null;
        get_lugarlibre_devuelto = false;
    }



    bool lugar_libre_marcado = false;
    //hacer la ruta para marcharse de la casa
    private void IrseDeCasa()
    {
        print("-------alguien se fue -----------");
        anim.SetBool("Comer", false);
        Dejar_Tenedores();
        anim.SetBool("Caminar", true);
        Dejar_Lugar_Libre();
        //si no estoy en el lugar 3
        //if (nro_asiento_ocupado != Nro_Asiento.Asiento_3)
        //{
        //mientras no llegue a la puerta
        if (!llegue_a_puerta)
        {
            llegue_a_puerta = Vector3.Distance(puntos.punto_salida.transform.position, transform.position) < distancia_min_para_llegar;
            transform.LookAt(puntos.punto_salida.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, puntos.punto_salida.transform.position, Time.deltaTime * velocidad_caminando);
        }
        else if (llegue_a_puerta && !llegue_al_destino)
        {
            llegue_al_destino = Vector3.Distance(puntos.punto_entrada.transform.position, transform.position) < distancia_min_para_llegar;
            transform.LookAt(puntos.punto_entrada.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, puntos.punto_entrada.transform.position, Time.deltaTime * velocidad_caminando);
        }
        else if (llegue_a_puerta && llegue_al_destino)
        {
            anim.SetBool("Caminar", false);
            estoy_en_casa = false;
            llegue_a_puerta = false;
            llegue_al_destino = false;
            lugar_libre_marcado = false;
        }

        //}
    }

    private void Dejar_Tenedores()
    {
        inv_tenedores.Desocupar_tenedores(nro_asiento_ocupado);
    }

    private void Dejar_Lugar_Libre()
    {
        if (!lugar_libre_marcado)
        {
            lugar_libre_marcado = true;
            puntos.Set_Lugar_Libre((int)nro_asiento_ocupado);
            nro_asiento_ocupado = Nro_Asiento.Ningun_Asiento;
        }
    }

}
