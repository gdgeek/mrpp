using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MrPP.Game
{
    public class BoardManager : GDGeek.Singleton<BoardManager>
    {
        [SerializeField]
        private BoardWindow _window;
        private TargetContent _content;

        private void onClose(){
            close();
        }
        public void open(TargetBox tb) {
            _content = tb.target.content;
         
            _window.setContent(_content);
            _window.open();
        }
        public void close() {
            _content = null;
            _window.close();
        }
       

        // Update is called once per frame
        void Update()
        {

        }
    }
}