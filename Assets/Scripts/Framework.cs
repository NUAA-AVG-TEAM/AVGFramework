using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Framework : MonoBehaviour
{
    public GameObject oa;
    public GameObject mn;
    public GameObject gm;
    public GameObject st;
    public GameObject sl;
    public GameObject ac;
    public GameObject gp;   // gaming panel
    public GameObject lp;   // log panel
    public GameObject cp;   // choice panel
    public GameObject ldp;  // load panel
    public GameObject sp;   // save panel
    public GameObject qp;   // qload panel
    public GameObject mp;   // main panel
    public GameObject cgp;  // cg panel
    public GameObject bp;   // bgm panel
    private void Awake()
    {
        oa.SetActive(false);
        mn.SetActive(false);
        gm.SetActive(false);
        st.SetActive(false);
        sl.SetActive(false);
        ac.SetActive(false);

        gp.SetActive(false);
        lp.SetActive(false);
        cp.SetActive(false);
        ldp.SetActive(false);
        sp.SetActive(false);
        qp.SetActive(false);
        mp.SetActive(false);
        cgp.SetActive(false);
        bp.SetActive(false);
    }
}
