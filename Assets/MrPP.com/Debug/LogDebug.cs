using System;
using System.Collections;
using System.Collections.Generic;
using MrPP.Helper;
using UnityEngine;
using UnityEngine.UI;
namespace MrPP{
	public class LogDebug : GDGeek.Singleton<LogDebug>
	{

	   
		private struct Pack
		{
			public Color32 color;
			public string text;
			public string message;
		};
		private Queue<LogDebugItem.Item> texts_ = new Queue<LogDebugItem.Item>();
	    public Text _text;

		[SerializeField]
		private LogDebugItem[] _items;
		// Use this for initialization
		void Start () {
	        for (int i = 0; i < list_.Count; ++i) {
	            log(list_[i]);
	        }
		}
		
		
	    private int n = 0;

	    private void refresh() {
	        if (texts_.Count > 30) {
	            texts_.Dequeue();
	        }
			//texts_ [1];
			LogDebugItem.Item[] texts = texts_.ToArray();
			//int m = );
	       //string log = "";
	        for (int i = Mathf.Min(texts_.Count, _items.Length )- 1; i >=0 ; --i) {
				_items [i].show (texts [texts.Length - 1-i]);
	        }
	        _text.text = "";
	    }
        private bool pause_ = false;
        internal void pause(string text, string message, Color32 color)
        {


            _text.text = message;
            log(text, message, color);
            pause_ = true;
        }

        public void print(string message) {

            _text.text = message;
        }
        public SimpleTagalong _tagalong;
        internal void locked()
        {
            _tagalong.enabled = false;
        }

        private static List<Pack> list_ = new List<Pack>();
	    public static void Log(string text)
	    {
	        Log(text, null,  Color.blue);
	    }


		public static void Log(string text,  Color32 color)
		{
			Log(text, null,  color);
		}

		public static void Log(string text,  string message)
		{
			Log(text, message,  Color.blue);
		}
		public static void Log(string text, string message, Color32 color)
		{
			if (LogDebug.Instance != null)
			{
				LogDebug.Instance.log(text, message, color);
			}
			else {
				list_.Add(CreatePack (text, message, color));
			}
		}




		private static Pack CreatePack(string text, string message, Color32 color){

			Pack pack = new Pack ();
			pack.color = color;
			pack.text = text;
			pack.message = message;
			return pack;
		}
		private LogDebugItem.Item createItem(string text, string message, Color32 color){
		
			LogDebugItem.Item item = new LogDebugItem.Item();
			item.color = color;
			item.text = text;
			item.message = message;
			item.no = n++;
			return item;
		}

		public void log(string text, string message, Color32 color)
		{
            if (!pause_) {

                log (createItem (text, message, color));
            }
        }
		private void log(Pack pack)
		{
			log (createItem (pack.text, pack.message, pack.color));
		}
		private void log(LogDebugItem.Item item)
		{
			texts_.Enqueue(item);
			refresh();
		}
	}
}