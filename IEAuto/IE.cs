using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEAuto
{
    class IE
    {
        SHDocVw.InternetExplorer ie;
        public MSHTML.HTMLDocument Document
        {
            get
            {
                return ie.Document;
            }
        }
        public string LastErrorMsg { get; set; }
        public IE()
        {
            try
            {
                ie = new SHDocVw.InternetExplorer();
            }
            catch (Exception e)
            {
                LastErrorMsg = e.Message;
            }
        }
        void Wait()
        {
            while(ie.Busy || ie.ReadyState != SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
            {
                System.Threading.Thread.Sleep(100);
            }
        }
        public int Browse(string url)
        {
            object ourl = url;
            try
            {
                ie.Navigate2(ref ourl);
                Wait();
            }
            catch ( Exception e )
            {
                LastErrorMsg = e.Message;
                return -1;
            }
            return 0;
        }
        public int Close()
        {
            try
            {
                ie.Quit();
            }
            catch ( Exception e)
            {
                LastErrorMsg = e.Message;
                return -1;
            }
            ie = null;
            return 0;
        }
        
    }
}
