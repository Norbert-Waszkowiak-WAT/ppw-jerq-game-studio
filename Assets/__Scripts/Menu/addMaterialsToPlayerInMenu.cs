using UnityEngine;

public class addMaterialsToPlayerInMenu : MonoBehaviour
{
    public Material mat1;
    public Material mat2;
    public Material mat3;

    public string a = "black";
    public GameObject[] toMat1;
    public string b = "pink _ black";
    public GameObject[] toMat2;
    public string c = "simple iridescent";
    public GameObject[] toMat3;

    private void Awake()
    {
        Paint();
    }

    public void Paint()
    {
        LogWriter.WriteLog("Painting");
        foreach (GameObject obj in toMat1)
        {
            obj.GetComponent<Renderer>().material = mat1;
        }
        foreach (GameObject obj in toMat2)
        {
            obj.GetComponent<Renderer>().material = mat2;
        }
        foreach (GameObject obj in toMat3)
        {
            obj.GetComponent<Renderer>().material = mat3;
        }
    }
}
