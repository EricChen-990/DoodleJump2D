using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabPool : MonoBehaviour
{
    private List<GameObject> poolObjects = new List<GameObject>();
    private List<GameObject> checkboxPool = new List<GameObject>();
    private List<GameObject> trapLeftPool = new List<GameObject>();
    private List<GameObject> trapRightPool = new List<GameObject>();
    private int amountToPool = 20;
    private int smallAmoountToPool = 5;
    [SerializeField] private GameObject platfromPrefab;
    [SerializeField] private GameObject springPrefab;
    [SerializeField] private GameObject trapPrefab;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject checkBoxPrefab;
    [SerializeField] private GameObject trapLeftPrefab;
    [SerializeField] private GameObject trapRightPrefab;
    
    // Start is called before the first frame update
    void Start(){
        for (int i = 0; i < amountToPool ; i++){
            int j = UnityEngine.Random.Range(0, 10);
            switch(i){
                case 0:
                    GameObject gobj =  Instantiate(monsterPrefab);
                    gobj.SetActive(false);
                    poolObjects.Add(gobj);
                    break;
                case 1:
                case 2:
                    GameObject gobj1 =  Instantiate(springPrefab);
                    gobj1.SetActive(false);
                    poolObjects.Add(gobj1);
                    break;
                case 3:
                case 4:
                    GameObject gobj2 =  Instantiate(trapPrefab);
                    gobj2.SetActive(false);
                    poolObjects.Add(gobj2);
                    break;
                
                default:
                    GameObject gobj3 =  Instantiate(platfromPrefab);
                    gobj3.SetActive(false);
                    poolObjects.Add(gobj3);
                    break;
                    
            }
        }
    }

    for(int i = 0 ; i < smallAmoountToPool ; i++){
        GameObject gobja =  Instantiate(checkBoxPrefab);
        gobja.SetActive(false);
        poolObjects.Add(gobja);

        GameObject gobjb =  Instantiate(trapLeftPrefab);
        gobjb.SetActive(false);
        poolObjects.Add(gobjb);

        GameObject gobjc =  Instantiate(trapRightPrefab);
        gobjc.SetActive(false);
        poolObjects.Add(gobjc);
    }

    public GameObject GetObject(){
        int i = UnityEngine.Random.Range(0, poolObjects.Count);
        GameObject goobj = poolObjects[i];
        poolObjects.RemoveAt(i);
        return goobj;
    }

    public void Recycle(GameObject gobj){
        gobj.SetActive(false);
        poolObjects.Add(gobj);
    }

    public GameObject GetChickBoxObject(){
        GameObject gobj = checkboxPool[0];
        checkboxPool.RemoveAt(0);
        return gobj;
    }

    public void RecycleCheckBox(GameObject gobj){
        gobj.SetActive(false);
        checkboxPool.Add(gobj);
    }

    public Gameobject GetTrapLeftobject(){
        GameObject gobj = trapLeftPool[0];
        trapLeftPool.RemoveAt (0);
        return gobj;
    }

    public void RecycleTrapLeft(GameObject gobj){
        gobj.setActive(false);
        trapLeftPool.Add(gobj);
    }

    public GameObject GetTrapRightobject(){
        //int i = UnityEngine. Random. Range (0, poolobjects .Count);
        GameObject gobj = traprightPool[0];
        trapRightPool.RemoveAt (0);
        return gobj;
    }
2個參考
    public void RecycleTrapRight(GameObject gobj){
        gobj.SetActive(false); 
        trapRightPool.Add(gobj);
    }

    void Update(){
        
    }
}
    

