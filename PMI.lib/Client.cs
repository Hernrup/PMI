using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Chello.Core;

namespace PMI.Lib
{
    public class Client
    {
        private TrelloClient trelloClient;
        private LimeClient limeClient;

        public Client() { 
            this.trelloClient = new TrelloClient();
            this.limeClient = new LimeClient();
        }

        public void process() {
            
        }
        public void test()
        {

            trelloClient.test();
            
        }   
        
    }
}
