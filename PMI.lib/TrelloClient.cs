using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using Chello.Core;
using System.Configuration;
using System.IO;

namespace PMI.Lib
{
    //TODO extract interface
    public class TrelloClient
    {
        private ChelloClient client {get; set;}
        private string key = "";
        private string secreatKey = "";
        private string username = "";
        private string token = "";
        private string tokenFile = "";
        

        public TrelloClient() {
            this.key = ConfigurationManager.AppSettings["key"];
            this.secreatKey = ConfigurationManager.AppSettings["secreatKey"];
            this.username = ConfigurationManager.AppSettings["username"];
            this.tokenFile = ConfigurationManager.AppSettings["tokenFile"];
            this.token = getToken();

            client = new ChelloClient(key,token);
        }

        public string getToken() {
            try
            {
                using (StreamReader sr = new StreamReader(string.Format("{0}", this.tokenFile)))
                {
                    String sToken = sr.ReadToEnd();
                    return sToken;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Could not load token. "+e.ToString());
            }


        }

        public void test() {
            List<Board> boards = new List<Board>();
            Board board = this.client.Boards.Single("50e6ae765d8ce6b7680018d0");
            boards = this.client.Members.AllBoards(username).ToList();
        }

        public List<Board> getItems(ItemType type)
        {
            List<Board> items = new List<Board>();

            //items = this.client.Boards.ForUser(username).ToList();
            items = this.client.Members.AllBoards(username).ToList();
            return items;
        }

        public Board getBoard(string id)
        {
            Board board = this.client.Boards.Single("50e6ae765d8ce6b7680018d0");
            return board;
        }

    }
}
