using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryObject : MonoBehaviour 
{

    private Transform _carryPosition;

    private GameObject _carriedObject = null;
    private Transform _carriedObjectTransform;

    private List<GameObject> _portableObject;
    private SpawnerIngredients _spawnerIngredient = null;

    private void Start()
    {
        _portableObject = new List<GameObject>();
        _carryPosition = transform;
    }

    private void TakeObject(GameObject toTake)
    {
        _carriedObject = toTake;
        _carriedObjectTransform = toTake.transform;
        _carriedObject.GetComponent<PortableObject>().Take();
    }

    public void ReleaseObject()
    {
        if(_carriedObject)
        {
            _carriedObject.GetComponent<PortableObject>().Release();
            _carriedObject = null;
        }
    }

    private void Update()
    {
         if(_carriedObject)
         {
             _carriedObjectTransform.position = _carryPosition.position;
             _carriedObjectTransform.rotation = _carryPosition.rotation;
         }
    }

    public void TryTakeObject()
    {
        if (!_carriedObject)
        {
            if(_spawnerIngredient != null)
            {
                _spawnerIngredient.SpawnIngredientFor(this);
                return;
            }

            if (_portableObject.Count > 0)
            {
                float distance = Mathf.Infinity;
                GameObject go = null;
                for (int i = 0; i < _portableObject.Count; )
                {
                    if (_portableObject[i] == null)
                    {
                        _portableObject.RemoveAt(i);
                    }
                    else
                    {
                        float tempDist = Vector3.Distance(_carryPosition.position, _portableObject[i].transform.position);
                        if (tempDist < distance)
                        {
                            go = _portableObject[i];
                            distance = tempDist;
                        }

                        i++;
                    }
                }

                if (go != null)
                {
                    TakeObject(go);
                }
            }
        }
        else
        {
            ReleaseObject();
        }
    }

    public void TryTakeObject(GameObject goIngredient)
    {
        if (!_carriedObject)
        {
            TakeObject(goIngredient);
        }
        else
        {
            ReleaseObject();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PortableObject")
        {
            PortableObject po = other.GetComponent<PortableObject>();
            if (po && po.GetCanBeTaken())
            {
                _portableObject.Add(other.gameObject);
            }
        }
        else if(other.tag == "Spawner")
        {
            SpawnerIngredients si = other.GetComponent<SpawnerIngredients>();
            if(si != null)
            {
                _spawnerIngredient = si;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PortableObject")
        {
            _portableObject.Remove(other.gameObject);
        }
        else if (other.tag == "Spawner")
        {
            _spawnerIngredient = null;
        }
    }
}
