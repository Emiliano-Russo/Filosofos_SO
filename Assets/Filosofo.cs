using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class Filosofo : MonoBehaviour
{
    public struct filosofo_estados
    {
        public static  Nro_Asiento nro_asiento_ocupado; 
        public static bool estoy_en_casa;
        public static bool estoy_comiendo;
        public struct transicion
        {
            public static bool llegue_a_puerta;
            public static bool llegue_al_destino;
        }
    }

    public GameObject punto_salida;
    public GameObject punto_entrada;
    public GameObject lugar_2;
    public float velocidad_caminando;
    public bool en_casa
    {
        get { return filosofo_estados.estoy_en_casa; }
    }


    //el update creo que no va a estar, porque el filosofo solo va a recibir ordenes
    void Update()
    {       
        Salir_Casa();
        Entrar_Casa();
    }

    //creo que las variables se van a asignar afuera
    private void Start()
    {
        filosofo_estados.estoy_en_casa = true;
        filosofo_estados.nro_asiento_ocupado = Nro_Asiento.Asiento_2;
        filosofo_estados.transicion.llegue_al_destino = false;
        filosofo_estados.transicion.llegue_a_puerta = false;
    }


    public void Entrar_Casa()
    {
        if (!filosofo_estados.estoy_en_casa)
        {
            Debug.Log("entrando!");
            Ingresar_Casa();
            EmpezarAComer();
        }
    }

    public void Salir_Casa()
    {
        if (filosofo_estados.estoy_en_casa)
        {
            Debug.Log("saliendo!");
            PararDeComer();
            IrseDeCasa();
        }
    }


    //Empiezan los metodos privados 

    //hacer una animacion de comer
    private void EmpezarAComer()
    {
        //si estoy en el lugar
        if (filosofo_estados.nro_asiento_ocupado != Nro_Asiento.Ningun_Asiento)
        {
            //activar la animacion de comer
        }
    }

    private void Ingresar_Casa()
    {
        
        if (!filosofo_estados.transicion.llegue_a_puerta)
        {
            filosofo_estados.transicion.llegue_a_puerta = Vector3.Distance(punto_salida.transform.position, transform.position) < 0.5;
            transform.position = Vector3.MoveTowards(transform.position, punto_salida.transform.position, Time.deltaTime * velocidad_caminando);
        }
        else if (filosofo_estados.transicion.llegue_a_puerta && !filosofo_estados.transicion.llegue_al_destino)
        {
            filosofo_estados.transicion.llegue_al_destino = Vector3.Distance(lugar_2.transform.position, transform.position) < 0.5;
            transform.position = Vector3.MoveTowards(transform.position,lugar_2.transform.position , Time.deltaTime * velocidad_caminando);
        }
        else if (filosofo_estados.transicion.llegue_a_puerta && filosofo_estados.transicion.llegue_al_destino)
        {
            filosofo_estados.estoy_en_casa = true;
            filosofo_estados.transicion.llegue_al_destino = false;
            filosofo_estados.transicion.llegue_a_puerta = false;
        }
    }

    //hacer la ruta para marcharse de la casa
    private void IrseDeCasa()
    {
        //si no estoy en el lugar 3
        if (filosofo_estados.nro_asiento_ocupado != Nro_Asiento.Asiento_3)
        {
            //mientras no llegue a la puerta
            if(!filosofo_estados.transicion.llegue_a_puerta)
            {
                filosofo_estados.transicion.llegue_a_puerta = Vector3.Distance(punto_salida.transform.position, transform.position) < 0.5;
                transform.position = Vector3.MoveTowards(transform.position, punto_salida.transform.position, Time.deltaTime * velocidad_caminando);                
            }
            else if (filosofo_estados.transicion.llegue_a_puerta && !filosofo_estados.transicion.llegue_al_destino)
            {
                transform.position = Vector3.MoveTowards(transform.position, punto_entrada.transform.position, Time.deltaTime * velocidad_caminando);
                filosofo_estados.transicion.llegue_al_destino = Vector3.Distance(punto_entrada.transform.position, transform.position) < 0.5;
            }
            else if (filosofo_estados.transicion.llegue_a_puerta && filosofo_estados.transicion.llegue_al_destino)
            {
                filosofo_estados.estoy_en_casa = false;
                filosofo_estados.transicion.llegue_a_puerta = false;
                filosofo_estados.transicion.llegue_al_destino = false;
            }

        }
    }

    //parar de hacer la animacion de comer
    private void PararDeComer()
    {
        if (filosofo_estados.estoy_comiendo)
        {
            //detener animacion comer
        }

    }

    
 
}
