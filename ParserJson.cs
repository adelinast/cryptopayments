using System;
using System.IO;
using Newtonsoft.Json;

namespace CryptoPayments
{
    public class ParserJson
    {
        public ParserJson(string TransactionFile)
        {
            mTransactionFilename = TransactionFile;
            JsonString = "";
        }
        private void ReadJson()
        {
			try 
			{
				if (!File.Exists(mTransactionFilename)) 
				{
					//Console.WriteLine("File does not exist: {0}", mTransactionFilename);
					return;
				}

				using (StreamReader r = new StreamReader(mTransactionFilename))
				{
                    JsonString = r.ReadToEnd();
				}
			}
			catch (Exception e) 
			{
				Console.WriteLine("Process failed {0} {1}", e.ToString());
			}
        }

        public string JsonString { set; get; }
        

        public bool LoadJson(string jsonString, ref RootJsonObject jsonObj)
        {
			if (string.IsNullOrEmpty(jsonString))
            {
				return false;
			}
			
			jsonObj = JsonConvert.DeserializeObject<RootJsonObject>(jsonString);
			
			return true;
		}

        public bool LoadJson(ref RootJsonObject jsonObj)
        {
            ReadJson();

            if (string.IsNullOrEmpty(JsonString))
            {
                return false;
            }

            jsonObj = JsonConvert.DeserializeObject<RootJsonObject>(JsonString);

            return true;
        }

        private string mTransactionFilename;
    }
}
