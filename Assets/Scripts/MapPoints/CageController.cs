using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CageController : MonoBehaviour
{
    #region Singleton

    public static CageController Instance;

    void Awake()
    {
        Instance = this;
    }

    #endregion
    public GameObject Cage;
    public float CageRange;
    public IntRange WaitTime;
    public Vector2 OrginalPos;
    Vector2 cameraPos;
    // Use this for initialization
    void Start()
    {
        //StartCoroutine(Spawn());
    }
    IEnumerator Spawn()
    {
        MakeCage();
        yield return new WaitForSeconds(WaitTime.Random);
        StartCoroutine(Spawn());
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeCage()
    {
        Vector2 t = OrginalPos + Random.insideUnitCircle * CageRange;
        CageFinder.Instance.ChangeTarget( Instantiate(Cage, t, Quaternion.identity));
    }
    
    void OnDrawGizmos()
    {

        Gizmos.DrawWireSphere(OrginalPos, CageRange);
    }
}
