using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class particulasAplastar : MonoBehaviour
{
  void Start()
    {
        Destruir();
    }
    public async Task Destruir(){
        await Task.Delay(1000);
        Destroy(gameObject);
        
    }
}
