using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{



    [SerializeField] Text kaynak_1_Text;
    [SerializeField] Text bina_1_Text;

    private float kaynak_1;
    private int kaynak_1_donusturucu;
    private float bina_1;





    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method is start");
        Oku();

    }

    // Update is called once per frame
    void Update()
    {
        KaynakOlusturucu();
    }

    void KaynakOlusturucu()
    {
        kaynak_1 += Time.deltaTime;
        kaynak_1_donusturucu = (int)kaynak_1;
        kaynak_1_Text.text = "Kaynak_1: " + kaynak_1_donusturucu.ToString();
    }
    public void BinaOlusturucu()
    {
        bina_1 += kaynak_1_donusturucu;
        bina_1_Text.text = bina_1.ToString();
        if(kaynak_1 > 0)
        {
            kaynak_1 = 0;
        }
        Kaydet();
    }
    //data save system json format
    [System.Serializable]
    class SaveData
    {
        public float bina_1_data;
    }
    public void Kaydet()
    {
        SaveData data = new SaveData();
        data.bina_1_data = bina_1;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public void Oku()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if(File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bina_1 = data.bina_1_data;
        }
    }
    public void Resetle()
    {
        SaveData data = new SaveData();
        data.bina_1_data = 0;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Oku();
        bina_1_Text.text = bina_1.ToString();
    }











}
