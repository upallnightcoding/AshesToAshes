using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCntrl : MonoBehaviour
{
    private ProjectileSO projectileSO = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /**
     * SetProjectileSO() - 
     */
    public ProjectileCntrl SetProjectileSO(ProjectileSO projectileSO)
    {
        this.projectileSO = projectileSO;

        return (this);
    }

    public void CreateProjectile(Transform parent)
    {
        GameObject go = Instantiate(projectileSO.projectilePrefab, parent);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (projectileSO.impactPrefab)
        {
            GameObject go = Instantiate(projectileSO.impactPrefab, transform.position, Quaternion.identity);
            Destroy(go, 1.0f);
        }
    }
}
