using System;
using System.IO.Ports;
using UnityEngine;

public class SerialCommunication : MonoBehaviour
{
    private SerialPort serialPort;
    private const string PortName = "COM3";
    private const int BaudRate = 115200;
    private const int DataLength = 28;
    private const int FadeIncrement = 1;
    private const int FadeDecrement = 3;
    private const int MaxIntensity = 250;
    private const int MinIntensity = 0;

    private byte[] receivedData = new byte[DataLength];
    private byte[] sendData = new byte[DataLength];
    private int[] intData = new int[DataLength];
    private int[] intensity = new int[DataLength];
    private int[] ranintensity = new int[DataLength];
    private int[] touchDuration = new int[DataLength];

    int[] ranData = new int[DataLength];
    long l = 0;

    private float sigma = 0.5f;
    private int dataIndex = 0;

    // ランダム生成器（インスタンスを使い回す）
    private System.Random random = new System.Random();

    void Start()
    {
        InitializeSerialPort();
    }

    void Update()
    {
        if (IsSerialPortOpen())
        {
            ReceiveSerialData();
            UpdateIntensity();
            SendSerialData();
        }
    }

    private void InitializeSerialPort()
    {
        try
        {
            serialPort = new SerialPort(PortName, BaudRate)
            {
                ReadTimeout = 1000
            };
            serialPort.Open();
            Debug.Log($"シリアルポートを開きました: {PortName}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"シリアルポート接続エラー: {ex.Message}");
        }
    }

    private bool IsSerialPortOpen()
    {
        return serialPort != null && serialPort.IsOpen;
    }

    private void ReceiveSerialData()
    {
        try
        {
            while (serialPort.BytesToRead > 0)
            {
                byte receivedByte = (byte)serialPort.ReadByte();

                if (receivedByte == '\n')
                {
                    if (dataIndex == DataLength)
                    {
                        ConvertBytesToIntArray();
                        ApplyRandomActivation(0.3f);
                    }
                    dataIndex = 0;
                }
                else if (dataIndex < DataLength)
                {
                    receivedData[dataIndex++] = receivedByte;
                }
            }
        }
        catch (TimeoutException)
        {
            Debug.LogWarning("データ受信タイムアウト");
        }
        catch (Exception ex)
        {
            Debug.LogError($"受信エラー: {ex.Message}");
        }
    }

    private void ConvertBytesToIntArray()
    {
        for (int i = 0; i < DataLength; i++)
        {
            intData[i] = receivedData[i];
        }
    }

    // 点滅をランダムに適用（偏りを防ぐよう修正）
    private void ApplyRandomActivation(float probability)
    {
        if (l % 800 == 0)
        {
            int[] indices = new int[DataLength];
            for (int i = 0; i < DataLength; i++) indices[i] = i;
            Shuffle(indices);

            for (int n = 0; n < DataLength; n++)
            {
                int i = indices[n];
                if (intData[i] == 0)
                {
                    ranData[i] = (random.NextDouble() < probability) ? 1 : 0;
                }
                else
                {
                    ranData[i] = 1;
                }
            }
        }
        else
        {
            for (int i = 0; i < DataLength; i++)
            {
                if (intData[i] == 1)
                {
                    ranData[i] = 1;
                }
                else if (intData[i] == 0 && ranData[i] == 0)
                {
                    ranData[i] = 0;
                }
            }
        }

        l++;
        if (l > 5000) l = 0;
    }

    // 配列をシャッフル（Fisher?Yatesアルゴリズム）
    private void Shuffle(int[] array)
    {
        for (int i = array.Length - 1; i > 0; i--)
        {
            int j = random.Next(i + 1);
            int temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    private void UpdateIntensity()
    {
        float gaussianScale = 1.0f / (1.0f * Mathf.Sqrt(2 * Mathf.PI * sigma));

        Array.Fill(intensity, MinIntensity);

        for (int i = 0; i < DataLength; i++)
        {
            if (intData[i] == 1)
            {
                touchDuration[i] = Mathf.Min(touchDuration[i] + FadeIncrement, MaxIntensity);
            }
            else
            {
                touchDuration[i] = Mathf.Max(touchDuration[i] - FadeDecrement, MinIntensity);
            }
            if (ranData[i] == 1)
            {
                ranintensity[i] = Mathf.Min(ranintensity[i] + FadeIncrement, 50);
            }
            else
            {
                ranintensity[i] = Mathf.Max(ranintensity[i] - FadeDecrement, MinIntensity);
            }

            // ?? タッチなら最大250、ランダム点滅なら最大125
            //int localMax = (intData[i] == 1) ? MaxIntensity : 10;
            float baseIntensity = Mathf.Min(MaxIntensity, touchDuration[i]);

            intensity[i] = Mathf.Clamp(intensity[i] + ranintensity[i], MinIntensity, MaxIntensity);

            for (int j = 0; j < DataLength; j++)
            {
                float distance = Mathf.Abs(j - i);
                float gaussianIntensity = gaussianScale * Mathf.Exp(-0.5f * Mathf.Pow(distance, 2));
                intensity[j] = Mathf.Clamp(intensity[j] + Mathf.RoundToInt(2.5f * baseIntensity * gaussianIntensity), MinIntensity, MaxIntensity);
            }
        }

        //Debug.Log("Current Data: " + string.Join(", ", ranData));

        for (int i = 0; i < DataLength; i++)
        {
            sendData[i] = (byte)intensity[i];
        }
    }


    private void SendSerialData()
    {
        try
        {
            byte[] dataWithNewline = new byte[sendData.Length + 1];
            Array.Copy(sendData, dataWithNewline, sendData.Length);
            dataWithNewline[sendData.Length] = (byte)'\n';

            serialPort.Write(dataWithNewline, 0, dataWithNewline.Length);
        }
        catch (Exception ex)
        {
            Debug.LogError($"送信エラー: {ex.Message}");
        }
    }

    public int Getinfo(int i)
    {
        return intData[i];
    }

    void OnApplicationQuit()
    {
        if (serialPort != null && serialPort.IsOpen)
        {
            serialPort.Close();
            Debug.Log("シリアルポートを閉じました");
        }
    }
}
