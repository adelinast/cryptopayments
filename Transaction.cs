using System.Collections.Generic;
using System.Linq;
using System.Collections.Specialized;
using System.Collections;

namespace CryptoPayments
{
    public class Transaction
    {
        public Transaction()
        {
            mKnownAdresses = new OrderedDictionary();
            FillKnownAddresses();
        }        
               
        public int Id { get; set; }
        public bool InvolvesWatchonly { get; set; }
        public string Txid { get; set; }
        public string Account { get; set; }
        public string Address { get; set; }
        public string Category { get; set; }
        public double Amount { get; set; }
        public string Label { get; set; }
        public long Confirmations { get; set; }
        public string Blockhash { get; set; }
        public long Blockindex { get; set; }
        public double Blocktime { get; set; }
        public long Vout { get; set; }
        public List<object> Walletconflicts { get; set; }
        public double Time { get; set; }
        public double Timereceived { get; set; }
        public string Bip125_replaceable { get; set; }

        public bool IsValid()
        {
            return Confirmations >= scmMinConfirmations;
        }

        static public bool IsKnownAddress(string Address)
        {
            return mKnownAdresses.Cast<DictionaryEntry>().Any(x => (x.Value != null && x.Value.Equals(Address)));
        }

        public void FillKnownAddresses()
        {
            mKnownAdresses.Clear();

            mKnownAdresses.Add("Wesley Crusher", "mvd6qFeVkqH6MNAS2Y2cLifbdaX5XUkbZJ");
            mKnownAdresses.Add("Leonard McCoy", "mmFFG4jqAtw9MoCC88hw5FNfreQWuEHADp");
            mKnownAdresses.Add("Jonathan Archer", "mzzg8fvHXydKs8j9D2a8t7KpSXpGgAnk4n");
            mKnownAdresses.Add("Jadzia Dax", "2N1SP7r92ZZJvYKG2oNtzPwYnzw62up7mTo");
            mKnownAdresses.Add("Montgomery Scott", "mutrAf4usv3HKNdpLwVD4ow2oLArL6Rez8");
            mKnownAdresses.Add("James T.Kirk", "miTHhiX3iFhVnAEecLjybxvV5g8mKYTtnM");
            mKnownAdresses.Add("Spock", "mvcyJMiAcSXKAEsQxbW9TYZ369rsMG6rVV");
        }

        static public string GetKnownAccount(string Address)
        {
            var account = (mKnownAdresses.Cast<DictionaryEntry>().First(x => (x.Value != null && x.Value.Equals(Address)))).Key;
            return account.ToString();
        }

        static public IOrderedDictionary GetKnownAccounts()
        {
            return mKnownAdresses;
        }

        static public IOrderedDictionary mKnownAdresses; //account name and address
        const int scmMinConfirmations = 6;
    }
}
