using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectBolts : MonoBehaviour
{
    	// Start is called before the first frame update
   	void Start()
    	{
        
   	}

	void OnCollisionEnter(Collider other)
	{
		if(other.CompareTag("bolt"))
		{
			Destroy(other.gameObject);
		}	
	}

}
