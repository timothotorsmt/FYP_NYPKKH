using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ChatSys
{
    // This class reads different CSV files
    public class CSVReader : MonoBehaviour
    {
        // TODO: make this more modular and extendible
        [SerializeField] private string _chatLocation;
        [SerializeField] private ChatList _chatListContainer;

        // Start is called before the first frame update
        void Awake()
        {
            _chatListContainer.ChatNodeList.Clear(); 
            ReadCSV();
        }

        // Clear log after runtime because then its gna be fuckeddddddd 
        void OnApplicationQuit() {
            _chatListContainer.ChatNodeList.Clear(); 
        }

        // Function to read CSV files
        void ReadCSV() 
        {
            string filePath;

            filePath = _chatLocation;
            // Read and write files from the filepath
            StreamReader reader = new StreamReader(filePath, true);
            bool EOF = false;

            while (!EOF) {
                string data_string = reader.ReadLine();
                if (data_string == null) {
                    EOF = true;
                    break;
                }

                // store in a chat node
                ChatNode temp_chatNode = new ChatNode();
                string[] dataValues = data_string.Split(",");

                if (dataValues[1] == null || dataValues[2] == null || dataValues[3] == null) {
                    Debug.LogWarning("Incorrect formatting of CSV files, certain fields missing. Check your formatting of the file.");
                    continue;
                }


                if (dataValues[1] != null) {
                    // Get ID
                    // Check if there is a proper ID (starting with pound sign)
                    if (dataValues[1][0] == '#') {
                        temp_chatNode.ID = dataValues[1];

                    }
                }

                if (dataValues[2] != null) {
                    // Get order
                    temp_chatNode.Order = uint.Parse(dataValues[2]);
                }

                if (dataValues[3] != null) {
                    // Get speaker name
                    temp_chatNode.Speaker = dataValues[3];
                }

                if (dataValues[4] != null) {
                    // Get sprite mood
                    temp_chatNode.Mood = (uint.Parse(dataValues[4]));
                }

                if (dataValues[5] != null) {
                    // Get main body
                    dataValues[5] = dataValues[5].Replace("//", ",");
                    temp_chatNode.BodyText = dataValues[5];
                }

                if (dataValues.Length > 6) {
                    temp_chatNode.Questions = new List<List<string>>();
                    // Questions and answers
                    for (int i = 6; i < dataValues.Length - 1; i+=2) {
                        if (dataValues[i] != "" && dataValues[i+1] != "") {
                            List<string> x = new List<string>();
                            dataValues[i] = dataValues[i].Replace("//", ",");
                            x.Add(dataValues[i]);
                            x.Add(dataValues[i+1]);
                            temp_chatNode.Questions.Add(x);
                        }
                    }
                }
                
                if (temp_chatNode != null) {
                    _chatListContainer.ChatNodeList.Add(temp_chatNode);
                }
            }
        }
    }

}
