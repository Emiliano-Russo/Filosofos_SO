using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manejador_Escena : MonoBehaviour
{

    public void Ir_al_inicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}
