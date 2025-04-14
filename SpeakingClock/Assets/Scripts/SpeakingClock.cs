using System.Collections.Generic;
using NUnit.Framework.Internal;
using TMPro;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI finalTimeText;
    [SerializeField] public List<TextMeshProUGUI> minuteText;
    public Color32 lightOff;
    public List<ListWrapper<int>> HourLeds;
    public List<ListWrapper<int>> MinuteLeds;
    public List<int> BasicLeds;
    public List<int> MainHourLeds;
    public int MainHourPluralLed;

    private int lastMinute = -5;
    private int hour;
    private int minute;
    public List<int> finalLeds = new List<int>();
    private bool nearEndHour;

    public int TestH;
    public int TestM;

    void Start()
    {
        

    }

    void UpdateLedList() {
        ClearLeds();
        finalLeds.Clear();

        finalLeds.AddRange(BasicLeds);
        finalLeds.AddRange(HourLeds[hour].leds);
        if (hour%12 != 0) finalLeds.AddRange(MainHourLeds);
        if (hour%12 > 1) finalLeds.Add(MainHourPluralLed);
        finalLeds.AddRange(MinuteLeds[minute/5].leds);

        finalLeds.Sort();

        LightLeds();
    }

    void LightLeds() {
        int count = 0;
        for (int i = 0; i < finalTimeText.textInfo.characterCount; ++i)
            {
                if (i != finalLeds[count]) continue;
                count++;
                Color32 myColor32 = Color.white;
                int meshIndex = finalTimeText.textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = finalTimeText.textInfo.characterInfo[i].vertexIndex;
                Color32[] vertexColors = finalTimeText.textInfo.meshInfo[meshIndex].colors32;
                vertexColors[vertexIndex + 0] = myColor32;
                vertexColors[vertexIndex + 1] = myColor32;
                vertexColors[vertexIndex + 2] = myColor32;
                vertexColors[vertexIndex + 3] = myColor32;
                if (count >= finalLeds.Count) break;
            }
            finalTimeText.UpdateVertexData(TMP_VertexDataUpdateFlags.All);
    }

    void ClearLeds() {
        if (finalLeds.Count == 0) return;
        int count = 0;
        for (int i = 0; i < finalTimeText.textInfo.characterCount; ++i)
            {
                if (i != finalLeds[count]) continue;
                count++;
                Color32 myColor32 = lightOff;
                int meshIndex = finalTimeText.textInfo.characterInfo[i].materialReferenceIndex;
                int vertexIndex = finalTimeText.textInfo.characterInfo[i].vertexIndex;
                Color32[] vertexColors = finalTimeText.textInfo.meshInfo[meshIndex].colors32;
                vertexColors[vertexIndex + 0] = myColor32;
                vertexColors[vertexIndex + 1] = myColor32;
                vertexColors[vertexIndex + 2] = myColor32;
                vertexColors[vertexIndex + 3] = myColor32;
                if (count >= finalLeds.Count) break;
            }
    }

    // Update is called once per frame
    void Update()
    {
        if (finalTimeText.textInfo.characterCount == 0) return;

        minute = System.DateTime.Now.Minute - System.DateTime.Now.Minute%5;//TestM;

        if (System.DateTime.Now.Minute != lastMinute) {
            if (minute != lastMinute/5) {
                nearEndHour = minute > 30;
                hour = System.DateTime.Now.Hour + (nearEndHour? 1 : 0);//TestH + (nearEndHour? 1 : 0);
                if (hour > 12) hour %= 12;
                
                UpdateLedList();
            }
            lastMinute = System.DateTime.Now.Minute;
            switch (lastMinute%5) {
                case 0:
                    foreach (var minuteLed in minuteText) {
                        minuteLed.color = lightOff;
                    }
                break;

                case 4:
                    minuteText[3].color = Color.white;
                    goto case 3;
                case 3:
                    minuteText[2].color = Color.white;
                    goto case 2;
                case 2:
                    minuteText[1].color = Color.white;
                    goto case 1;
                case 1:
                    minuteText[0].color = Color.white;
                break;
                default:
                break;
            }
        }
    }
}
