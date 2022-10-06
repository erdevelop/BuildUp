using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class GameManager : MonoBehaviour
{

    [SerializeField] Text kaynak_1_Text;
    [SerializeField] Text bina_1_Text;
    [SerializeField] Text bina_2_Text;
    [SerializeField] Text bina_3_Text;

    private float kaynak_1;
    private int kaynak_1_donusturucu;
    private float bina_1;
    private float bina_2;
    private float bina_3;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method is start");
        Oku();
        BinaDegerGosterGuncelle();

    }
    // Update is called once per frame
    void Update()
    {
        KaynakOlusturucu();
    }
    void BinaDegerGosterGuncelle()
    {
        GameObject.FindGameObjectWithTag("Bina_1_Text").GetComponent<Text>().text = bina_1.ToString();
        GameObject.FindGameObjectWithTag("Bina_2_Text").GetComponent<Text>().text = bina_2.ToString();
        GameObject.FindGameObjectWithTag("Bina_3_Text").GetComponent<Text>().text = bina_3.ToString();
    }
    void KaynakOlusturucu()
    {
        kaynak_1 += Time.deltaTime;
        kaynak_1_donusturucu = (int)kaynak_1;
        kaynak_1_Text.text = kaynak_1_donusturucu.ToString();
    }
    public void Bina1Olusturucu()
    {
        bina_1 += kaynak_1_donusturucu;
        
        if(kaynak_1 > 0)
        {
            kaynak_1 = 0;
        }

        BinaDegerGosterGuncelle();
        Kaydet();
    }
    public void Bina2Olusturucu()
    {
        bina_2 += bina_1;
        
        if(bina_1 > 0)
        {
            bina_1 = 0;
        }
        BinaDegerGosterGuncelle();
        Kaydet();
    }
    public void Bina3Olusturucu()
    {
        bina_3 += bina_2;
        
        if(bina_2 > 0)
        {
            bina_2 = 0;
        }
        BinaDegerGosterGuncelle();
        Kaydet();
    }
    //data save system json format
    [System.Serializable]
    class SaveData
    {
        public float bina_1_data;
        public float bina_2_data;
        public float bina_3_data;
    }
    public void Kaydet()
    {
        SaveData data = new SaveData();
        data.bina_1_data = bina_1;
        data.bina_2_data = bina_2;
        data.bina_3_data = bina_3;

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
            bina_2 = data.bina_2_data;
            bina_3 = data.bina_3_data;
        }
    }
    public void Resetle()
    {
        SaveData data = new SaveData();
        data.bina_1_data = 0;
        data.bina_2_data = 0;
        data.bina_3_data = 0;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        Oku();
        BinaDegerGosterGuncelle();
    }











}
