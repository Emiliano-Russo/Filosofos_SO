using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets
{
    public class Puntos : MonoBehaviour
    {
        [SerializeField]
        private  GameObject puntoSalida;
        [SerializeField]
        private  GameObject puntoEntrada;
        [SerializeField]
        private  GameObject[] lugares;
        [SerializeField]
        private  GameObject A, B,Centro_de_la_mesa;

        private static Dictionary<int, bool> lugar_libre = new Dictionary<int, bool>();

        public Text[] asientos_txt;

        private void Update()
        {
            for (int i = 1; i <= 4; i++)
            {
                asientos_txt[i].text = i + ") libre: " + lugar_libre[i];
            }
        }

        private void Start()
        {
            for (int i = 1; i <= 4; i++)
            {
                lugar_libre.Add(i, false);
            }
        }

        

        public GameObject get_centro_de_la_mesa
        {
            get { return Centro_de_la_mesa; }
        }

        public  GameObject punto_salida
        {
            get { return puntoSalida; }
        }

        public  GameObject punto_entrada
        {
            get { return puntoEntrada; }
        }

        public  GameObject punto_a
        {
            get { return A; }
        }

        public  GameObject punto_b
        {
            get { return B; }
        }

        public  GameObject Get_Lugar(int nro_lugar)
        {
            Checkear_nro_lugar(nro_lugar);
            return lugares[nro_lugar];
        }

        public  Lugar_Libre_ClaseParametro Get_Un_Lugar_Libre(){

            Lugar_Libre_ClaseParametro lugar_libre_parametro = new Lugar_Libre_ClaseParametro();
            for (int i = 1; i <= 4; i++)
            {
                if (lugar_libre[i])
                {
                    lugar_libre_parametro.lugar_libre_nro = i;
                    lugar_libre_parametro.lugar_libre_punto = lugares[i];
                    return lugar_libre_parametro;
                }
                    
            }
            throw new Exception("NO EXISTEN LUGARES LIBRES!");
        }

        public override string ToString()
        {
            string retorno = "";
            foreach (var item in lugar_libre)
            {
                retorno += item.Key+" / " +item.Value +"  -- ";
            }
            return retorno;
        }

        public  void Set_Lugar_Libre(int nro_lugar)
        {
            Checkear_nro_lugar(nro_lugar);
            lugar_libre[nro_lugar] = true;
        }

        public  void Set_Lugar_Ocupado(int nro_lugar)
        {
            Checkear_nro_lugar(nro_lugar);
            lugar_libre[nro_lugar] = false;
        }

        private  void Checkear_nro_lugar(int nro_lugar)
        {
            if (nro_lugar < 1 || nro_lugar > 4)
                throw new Exception("el lugar: " + nro_lugar + " no existe");
        }
    }
}
